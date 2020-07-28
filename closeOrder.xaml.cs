using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for closeOrder.xaml
    /// </summary>
    public partial class closeOrder : Window
    {
        IBL myBL = FactoryBl.GetBl();
        ObservableCollection<Order> orders;
        List<BE.HostingUnit> hostingUnits;
        Order order;
        StatusOrder s;
        public closeOrder(Host host)
        {
            InitializeComponent();
            order = new Order();
            orders = new ObservableCollection<Order>();
            hostingUnits = myBL.getHostingUnits(item => item.Owner.HostId == host.HostId);       
            refreshOrder(ref orders);
            closeComboBox.ItemsSource = orders;
            gridCloseOrder.DataContext = orders;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void closeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (closeComboBox.SelectedIndex == -1)
                return;
            order = orders[closeComboBox.SelectedIndex];
            radio1.Visibility = Visibility.Visible;
            radio2.Visibility = Visibility.Visible;
            gridCloseOrder.DataContext = order;
        }

        private void radio1_Checked(object sender, RoutedEventArgs e)
        {
            s = StatusOrder.Closes_with_customer_response;
            closush.Visibility = Visibility.Visible;
        }

        private void radio2_Checked(object sender, RoutedEventArgs e)
        {
            s = StatusOrder.Irrelevant;
            closush.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                myBL.updateOrder(order, s);
                orders[closeComboBox.SelectedIndex].Status = s; //i change also here to the refresh function, i dont want accept all from begining from Data source 
                MessageBox.Show("The order was successfully closed:)", "close", MessageBoxButton.OK, MessageBoxImage.Information);
                closeComboBox.SelectedIndex = -1;
                refreshOrder(ref orders);
                radio1.Visibility = Visibility.Collapsed;
                radio2.Visibility = Visibility.Collapsed;
                radio1.IsChecked = false;
                radio2.IsChecked = false;
                closush.Visibility = Visibility.Collapsed;
            }
            catch (HostingException h)
            {
                MessageBox.Show(h.Message, "close", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void refreshOrder(ref ObservableCollection<Order> o)
        {
            o.Clear();
            List<Order> allOrders = myBL.getOrders();
            foreach (var item in hostingUnits)
            {
                foreach (var item1 in allOrders)
                {
                    if (item.HostingUnitKey == item1.HostingUnitKey && item1.Status == StatusOrder.Mail_sent)
                        o.Add(item1);
                }
            }
        }
    }
}
