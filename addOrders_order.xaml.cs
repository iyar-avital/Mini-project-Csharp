using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for addOrders_order.xaml
    /// </summary>
    public partial class addOrders_order : Window
    {
        BackgroundWorker worker;
        bool threadush = false;
        BE.HostingUnit hostingUnit;
        BE.GuestRequest guestRequest;
        Order order;
        List<BE.HostingUnit> hostingUnitKeys;
        List<GuestRequest> guestRequestKeys;
        IBL myBL = FactoryBl.GetBl();
        public addOrders_order(Host host)
        {
            InitializeComponent();
            order = new Order();
            hostingUnitKeys = myBL.getHostingUnits(item => item.Owner.HostId == host.HostId);
            gridAddOrder.DataContext = hostingUnitKeys;
            guestRequestKeys = myBL.getGuestRequests(item => item.Status == StatusCostumer.Active);
            hComboBox.ItemsSource = hostingUnitKeys;
            gComboBox.ItemsSource = guestRequestKeys;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void gComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gComboBox.SelectedIndex == -1) return;
            guestRequest = guestRequestKeys[gComboBox.SelectedIndex];
            order.GuestRequestKey = guestRequest.GuestRequestKey;
        }

        private void hComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hComboBox.SelectedIndex == -1) return;
            hostingUnit = hostingUnitKeys[hComboBox.SelectedIndex];
            order.HostingUnitKey = hostingUnit.HostingUnitKey;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num = myBL.numOfDaysBetween(guestRequest.EntryDate, guestRequest.ReleaseDate);
            if (!(guestRequest.Allguestrequestdetails.Type == hostingUnit.AllhostingUnitdetails.Type &&
                guestRequest.Allguestrequestdetails.Area == hostingUnit.AllhostingUnitdetails.Area &&
                guestRequest.Allguestrequestdetails.SubArea == hostingUnit.AllhostingUnitdetails.SubArea &&
                guestRequest.Allguestrequestdetails.Adults == hostingUnit.AllhostingUnitdetails.Adults &&
                guestRequest.Allguestrequestdetails.Children == hostingUnit.AllhostingUnitdetails.Children &&
                guestRequest.Allguestrequestdetails.Pool == hostingUnit.AllhostingUnitdetails.Pool &&
                guestRequest.Allguestrequestdetails.Jacuzzi == hostingUnit.AllhostingUnitdetails.Jacuzzi &&
                guestRequest.Allguestrequestdetails.Garden == hostingUnit.AllhostingUnitdetails.Garden &&
                guestRequest.Allguestrequestdetails.ChildrensAttractions == hostingUnit.AllhostingUnitdetails.ChildrensAttractions &&
                guestRequest.Allguestrequestdetails.IsWifi == hostingUnit.AllhostingUnitdetails.IsWifi &&
                guestRequest.Allguestrequestdetails.IsAirConditioner == hostingUnit.AllhostingUnitdetails.IsAirConditioner))
            {
                MessageBoxResult m = MessageBox.Show("Guest request details doesn't match the selected unit, are you sure you want to add order?", "addOrder", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (m == MessageBoxResult.No)
                {
                    return;
                }
            }
            resultProgressBar.Visibility = Visibility.Visible;
            resultLabel.Visibility = Visibility.Visible;
            worker.RunWorkerAsync(30);
            myBL.addOrder(order);
            for (DateTime d = guestRequest.EntryDate; d < guestRequest.EntryDate.AddDays(num); d = d.AddDays(num))
            {
                MessageBoxResult n = MessageBoxResult.Cancel;
                if (hostingUnit[d] == true)
                    n = MessageBox.Show("The unit is busy at these dates", "addOrder", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (n == MessageBoxResult.OK)
                   return;
            }
                    try
                    {
                        List<Order> v = myBL.getOrders(item => item.HostingUnitKey == order.HostingUnitKey && item.GuestRequestKey == order.GuestRequestKey);
                        // to know what the Order Key- for the update!
                        order.OrderKey = v[v.Count() - 1].OrderKey;
                        myBL.updateOrder_privateCase_sentMail(order);
                        threadush = true; 
                    }
                    catch (HostingException h)
                    {
                        MessageBox.Show(h.Message);
                    }
                    catch (Exception ee)
                    {
                    worker.CancelAsync(); // Cancel the asynchronous operation.
                    MessageBox.Show(ee.ToString());
                    }
        }

       private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region ThreadMail
    
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int length = (int)e.Argument;
            int n = 500;
            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                if (threadush == true)
                    n = 50;
                    // Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(n);
                worker.ReportProgress(i * 100 / length);
            }  
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            resultLabel.Content = (progress + "%");
            resultProgressBar.Value = progress;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                resultLabel.Content = "Error!";
                resultProgressBar.Visibility = Visibility.Collapsed;
                resultLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                resultLabel.Content = "Done:) ";
                MessageBoxResult m = MessageBox.Show("The order is successfuly added", "addOrder", MessageBoxButton.OK, MessageBoxImage.Information);
                if (m == MessageBoxResult.OK)
                {
                    resultProgressBar.Visibility = Visibility.Collapsed;
                    resultLabel.Visibility = Visibility.Collapsed;
                }
            }
            gComboBox.SelectedItem = null;
            hComboBox.SelectedItem = null;
            threadush = false;
        }
        #endregion
    }
}
