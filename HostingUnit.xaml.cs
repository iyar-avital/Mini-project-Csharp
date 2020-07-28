using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnit : Window
    {
        ObservableCollection<BE.HostingUnit> Hostings;
        ObservableCollection<Order> Orders;
        BE.Host host;
        IBL myBL = FactoryBl.GetBl();
        string OrdersOrHostingUnit = null;
        public HostingUnit()
        {
            InitializeComponent();
            host = new Host();
            host.BankBranchDetails = new BankBranch();
            Hostings = new ObservableCollection<BE.HostingUnit>();
            Orders = new ObservableCollection<Order>();
            OrdersOrHostingUnit = "hostingunit";
            HostingUnitList.DataContext = Hostings;
            OrderList.DataContext = Orders;
            userControlush userControlush = new userControlush(host);
            UserGrid.Children.Add(userControlush);
            Grid.SetRow(userControlush, 1);
            userControlush.CostumerChange += Controlush_CostumerChange;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
               new addHostingUnit_HostingUnit("add", host).ShowDialog();
            Refresh(Hostings, myBL.getHostingUnits(item => item.Owner.HostId == host.HostId));
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
                new addHostingUnit_HostingUnit("update", host).ShowDialog();
            Refresh(Hostings, myBL.getHostingUnits(item => item.Owner.HostId == host.HostId));
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
                new DeleteHostingUnit(host).ShowDialog();
            Refresh(Hostings, myBL.getHostingUnits(item => item.Owner.HostId == host.HostId));
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (myBL.isDownloadFinished() == false)
                MessageBox.Show("The system is loading data. please try again later. Thanks.", "wait please", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                new AddHost(/*host*/).ShowDialog();
        }

        private void Button_AddOrder(object sender, RoutedEventArgs e)
        {
            new addOrders_order(host).ShowDialog();
            Refresh(Orders);
        }

        private void closeOrder_Click(object sender, RoutedEventArgs e)
        {
            new closeOrder(host).ShowDialog();
            Refresh(Orders);
        }

        private void MoreDetails_Click(object sender, RoutedEventArgs e)
        {
            if (Hostings.Count() == 0)
                MessageBox.Show("no more, you dont have hosting unit yet", "faild show more", MessageBoxButton.OK, MessageBoxImage.Information);
            else
               new ListHostingUnit(Hostings).ShowDialog();
        }

        private void Controlush_CostumerChange(object sender, EventArgs e)
        {
            ValueUserControl v = e as ValueUserControl;
            if (v.host_guest == HostOrGuest.host)
            {
                SignUpButton.Visibility = Visibility.Collapsed;
                LogOutButton.Visibility = Visibility.Visible;
                AddHostingButton.IsEnabled = true;
                DeleteHostingButton.IsEnabled = true;
                UpdateHostingButton.IsEnabled = true;
                AddOrderButton.IsEnabled = true;
                closeOrder.IsEnabled = true;
                MoreDetails.IsEnabled = true;
                UserGrid.Children.RemoveRange(0, 1);
                if (OrdersOrHostingUnit == "order")
                {
                    messageLogOrder.Visibility = Visibility.Collapsed;
                    OrderList.Visibility = Visibility.Visible;
                    Refresh(Orders);
                }
                else
                {
                    messageLog.Visibility = Visibility.Collapsed;
                    HostingUnitList.Visibility = Visibility.Visible;
                    Refresh(Hostings, myBL.getHostingUnits(item => item.Owner.HostId == host.HostId));
                }
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            host.HostId = "";
            host.PrivateName = "";
            host.FamilyName = "";
            host.PhoneNumber = "";
            host.MailAdress = "";
            host.BankAccountNumber = 0;
            host.CollectionClearance = false;
            host.IsInPlace = false;
            host.Languages = "";
            host.BankBranchDetails = new BankBranch();
            UserGrid.Children.RemoveRange(0, 1);
            userControlush userControlush = new userControlush(host);
            UserGrid.Children.Add(userControlush);
            Grid.SetRow(userControlush, 1);
            LogOutButton.Visibility = Visibility.Collapsed;
            SignUpButton.Visibility = Visibility.Visible;
            AddHostingButton.IsEnabled = false;
            DeleteHostingButton.IsEnabled = false;
            UpdateHostingButton.IsEnabled = false;
            AddOrderButton.IsEnabled = false;
            closeOrder.IsEnabled = false;
            MoreDetails.IsEnabled = false;
            if (OrdersOrHostingUnit == "order")
            {
                messageLogOrder.Visibility = Visibility.Visible;
                OrderList.Visibility = Visibility.Collapsed;
            }
            else
            {
                messageLog.Visibility = Visibility.Visible;
                HostingUnitList.Visibility = Visibility.Collapsed;
            }
            userControlush.CostumerChange += Controlush_CostumerChange;
            Hostings.Clear();
        }

        private void Refresh(ObservableCollection<BE.HostingUnit> ob, List<BE.HostingUnit> l)
        {
            ob.Clear();
            foreach (var item in l)
            {
                ob.Add(item);
            }
        }

        private void Refresh(ObservableCollection<BE.Order> ob)
        {
            ob.Clear();
            foreach (var item in myBL.getHostingUnits(item => item.Owner.HostId == host.HostId))
            {
                foreach (var item1 in myBL.getOrders())
                {
                    if (item.HostingUnitKey == item1.HostingUnitKey)
                        ob.Add(item1);
                }
            }
        }

        private void HostingUnitClick(object sender, RoutedEventArgs e)
        {
            OrdersOrHostingUnit = "hostingunit";
            MoreDetails.Visibility = Visibility.Visible;
            OrderTitle.Visibility = Visibility.Collapsed;
            HostingUnitTitle.Visibility = Visibility.Visible;
            if (messageLogOrder.Visibility == Visibility.Visible)
            {
                messageLog.Visibility = Visibility.Visible;
                messageLogOrder.Visibility = Visibility.Collapsed;
            }
            if (OrderList.Visibility == Visibility.Visible)
            {
                OrderList.Visibility = Visibility.Collapsed;
                Refresh(Hostings, myBL.getHostingUnits(item => item.Owner.HostId == host.HostId));
                HostingUnitList.Visibility = Visibility.Visible;
            }
        }

        private void OrderClick(object sender, RoutedEventArgs e)
        {
            OrdersOrHostingUnit = "order";
            MoreDetails.Visibility = Visibility.Hidden;
            HostingUnitTitle.Visibility = Visibility.Collapsed;
            OrderTitle.Visibility = Visibility.Visible;
            if (messageLog.Visibility == Visibility.Visible)
            {
                messageLog.Visibility = Visibility.Collapsed;
                messageLogOrder.Visibility = Visibility.Visible;
            }
            if (HostingUnitList.Visibility == Visibility.Visible)
            {
                Refresh(Orders);
                OrderList.Visibility = Visibility.Visible;
                HostingUnitList.Visibility = Visibility.Collapsed;
            }
        }
    }
}
