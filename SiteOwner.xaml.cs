//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using BE;
//using BL;

//namespace PLWPF
//{
//    /// <summary>
//    /// Interaction logic for SiteOwner.xaml
//    /// </summary>
//    public partial class SiteOwner : Window
//    {
//        IBL myBL = FactoryBl.GetBl();
//        public SiteOwner()
//        {
//            InitializeComponent();
//        }

//        private void HostingUnit_Click(object sender, RoutedEventArgs e)
//        {
//            new siteOwner_hostingUnits("hosting_unit").ShowDialog();
//        }

//        private void Orders_Click_1(object sender, RoutedEventArgs e)
//        {
//            new siteOwner_hostingUnits("order").ShowDialog();
//        }

//        private void GuestRequest_Click_2(object sender, RoutedEventArgs e)
//        {
//            new siteOwner_hostingUnits("guest").ShowDialog();
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            new siteOwner_hostingUnits("host").ShowDialog();
//        }

//        private void Button_Click_3(object sender, RoutedEventArgs e)
//        {
//            List<BE.GuestRequest> list = new List<BE.GuestRequest>();
//            list = myBL.groupByArea().groupToList();
//            groupGuestRequest.Visibility = Visibility.Visible;
//            GroupHostingUnit.Visibility = Visibility.Collapsed;
//            groupHost.Visibility = Visibility.Collapsed;
//            groupGuestRequest.DataContext = list;
//        }

//        private void Button_Click_4(object sender, RoutedEventArgs e)
//        {
//            List<BE.GuestRequest> list = new List<BE.GuestRequest>();
//            list = myBL.groupByNumOfPeople().groupToList();
//            groupGuestRequest.Visibility = Visibility.Visible;
//            GroupHostingUnit.Visibility = Visibility.Collapsed;
//            groupHost.Visibility = Visibility.Collapsed;
//            groupGuestRequest.DataContext = list;
//        }

//        private void groupByUnitArea_Click(object sender, RoutedEventArgs e)
//        {
//            List<BE.HostingUnit> list = new List<BE.HostingUnit>();
//            list = myBL.GroupByAreaOfUnit().groupToList();
//            GroupHostingUnit.Visibility = Visibility.Visible;
//            groupGuestRequest.Visibility = Visibility.Collapsed;
//            groupHost.Visibility = Visibility.Collapsed;
//            GroupHostingUnit.DataContext = list;
//        }

//        private void groupByNumOfUnit_Click(object sender, RoutedEventArgs e)
//        {
//            List<BE.Host> list = new List<BE.Host>();
//            list = myBL.GroupByNumOfUnit().groupToList();
//            groupHost.Visibility = Visibility.Visible;
//            groupGuestRequest.Visibility = Visibility.Collapsed;
//            GroupHostingUnit.Visibility = Visibility.Collapsed;
//            groupHost.DataContext = list;
//        }

//        private void Button_Click_5(object sender, RoutedEventArgs e)
//        {
//            new Profit().ShowDialog();
//        }
//    }
//}

using System;
using System.Collections.Generic;
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
    /// Interaction logic for SiteOwner.xaml
    /// </summary>
    
    public partial class SiteOwner : Window
    {
        IBL myBL = FactoryBl.GetBl();
        List<Button> buttons = null;
        public SiteOwner()
        {
            InitializeComponent();
            buttons = new List<Button>
            {
                buttonOrders,buttonhunits,buttonhosts,buttongRequest
            };
        }

        private void GuestRequest_Click_2(object sender, RoutedEventArgs e)
        {
            List<BE.GuestRequest> list = new List<BE.GuestRequest>();
            list = myBL.getGuestRequests();
            groupGuestRequest.Visibility = Visibility.Visible;
            GroupHostingUnit.Visibility = Visibility.Collapsed;
            groupHost.Visibility = Visibility.Collapsed;
            GroupOrder.Visibility = Visibility.Collapsed;
            groupGuestRequest.DataContext = list;
        }

        private void Orders_Click_1(object sender, RoutedEventArgs e)
        {
            List<BE.Order> list = new List<BE.Order>();
            list = myBL.getOrders();
            GroupOrder.Visibility = Visibility.Visible;
            GroupHostingUnit.Visibility = Visibility.Collapsed;
            groupHost.Visibility = Visibility.Collapsed;
            groupGuestRequest.Visibility = Visibility.Collapsed;
            GroupOrder.DataContext = list;
        }

        private void HostingUnit_Click(object sender, RoutedEventArgs e)
        {
            List<BE.HostingUnit> list = new List<BE.HostingUnit>();
            list = myBL.getHostingUnits();
            GroupHostingUnit.Visibility = Visibility.Visible;
            groupGuestRequest.Visibility = Visibility.Collapsed;
            groupHost.Visibility = Visibility.Collapsed;
            GroupOrder.Visibility = Visibility.Collapsed;
            GroupHostingUnit.DataContext = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<BE.Host> list = new List<BE.Host>();
            list = myBL.getHosts();
            groupHost.Visibility = Visibility.Visible;
            groupGuestRequest.Visibility = Visibility.Collapsed;
            GroupHostingUnit.Visibility = Visibility.Collapsed;
            GroupOrder.Visibility = Visibility.Collapsed;
            groupHost.DataContext = list;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new Profit().ShowDialog();
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            CloseGrid.Background = new SolidColorBrush((Color)backGroundColor.SelectedColor);
            StyleGrid.Background = new SolidColorBrush((Color)backGroundColor.SelectedColor);
            AllButtonGrid.Background = new SolidColorBrush((Color)backGroundColor.SelectedColor);
        }

        private void ColorPicker_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            foreach(var item in buttons)
            {
                item.Background = new SolidColorBrush((Color)buttonColor.SelectedColor);
            }
        }

        private void fontColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            foreach (var item in buttons)
            {
                item.Foreground = new SolidColorBrush((Color)fontColor.SelectedColor);
            }
        }
    }
    }

