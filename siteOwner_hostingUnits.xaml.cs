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
    /// Interaction logic for siteOwner_hostingUnits.xaml
    /// </summary>
    public partial class siteOwner_hostingUnits : Window
    {
        Host host = new Host();
        List<BE.HostingUnit> Hostings = new List<BE.HostingUnit>();
        List<Order> orders;
        List<BE.GuestRequest> guestRequests = new List<BE.GuestRequest>();
        List<Details> gdetails;
        IBL myBL = FactoryBl.GetBl();
        List<Details> details = new List<Details>();
        public siteOwner_hostingUnits(string type)
        {
            InitializeComponent();
            host = new Host();
            host.BankBranchDetails = new BankBranch();
            Hostings = myBL.getHostingUnits();
            foreach (var item in Hostings)
            {
                details.Add(item.AllhostingUnitdetails);
            }
            guestRequests = myBL.getGuestRequests();
            gdetails = new List<Details>();
            foreach (var item in guestRequests)
            {
                gdetails.Add(item.Allguestrequestdetails);
            }
            GuestList.DataContext = guestRequests;
            orders = myBL.getOrders();
            OrderList.DataContext = orders;
            HostingUnitList.DataContext = Hostings;
            if (type == "hosting_unit")
            {
                StackLists.Visibility = Visibility.Visible;
                HostingUnitList.Visibility = Visibility.Visible;
            }
            if (type == "order")
            {
                StackLists.Visibility = Visibility.Visible;
                OrderList.Visibility = Visibility.Visible;
            }
            if (type == "guest")
            {
                StackLists.Visibility = Visibility.Visible;
                OrderList.Visibility = Visibility.Visible;
            }

        }

    }
}
