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
    /// Interaction logic for Guest.xaml
    /// </summary>
    public partial class Guest : Window
    {
        ObservableCollection<BE.GuestRequest> guestRequests;
        GuestRequest guest = new GuestRequest();
        IBL myBL = FactoryBl.GetBl();
        public Guest()
        {
            InitializeComponent();
            guestRequests = new ObservableCollection<GuestRequest>();
            GuestRequestList.DataContext = guestRequests;
            guest = new GuestRequest();
            userControlush userControlush = new userControlush(guest);
            UserGrid.Children.Add(userControlush);
            Grid.SetRow(userControlush, 1);
            userControlush.CostumerChange += Controlush_CostumerChange;
        }

        private void Controlush_CostumerChange(object sender, EventArgs e)
        {
            ValueUserControl v = e as ValueUserControl;
            if (v.host_guest == HostOrGuest.guest)
            {
                LogOutButton.Visibility = Visibility.Visible;
                messageLog.Visibility = Visibility.Collapsed;
                UserGrid.Children.RemoveRange(0, 1);
                GuestRequestList.Visibility = Visibility.Visible;
                MoreDetails.IsEnabled = true;
                Refresh(guestRequests, myBL.getGuestRequests(item => item.GuestId == guest.GuestId));
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (guest.GuestId != null)
                new addGuestRequest(guest).ShowDialog();
            else
                new addGuestRequest().ShowDialog();
            Refresh(guestRequests, myBL.getGuestRequests(item => item.GuestId == guest.GuestId));
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Refresh(ObservableCollection<BE.GuestRequest> ob, List<BE.GuestRequest> l)
        {
            ob.Clear();
            foreach (var item in l)
            {
                ob.Add(item);
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            guest.GuestId = "_________";
            guest.Private_Name = "";
            guest.Family_Name = "";
            guest.MailAddress = "";
            userControlush userControlush = new userControlush(guest);
            UserGrid.Children.Add(userControlush);
            Grid.SetRow(userControlush, 1);
            LogOutButton.Visibility = Visibility.Collapsed;
            messageLog.Visibility = Visibility.Visible;
            GuestRequestList.Visibility = Visibility.Collapsed;
            MoreDetails.IsEnabled = false;
            userControlush.CostumerChange += Controlush_CostumerChange;
        }

        private void MoreDetails_Click(object sender, RoutedEventArgs e)
        {
            if (guestRequests.Count() == 0)
                MessageBox.Show("no more, you dont have guest requests yet", "faild show more", MessageBoxButton.OK, MessageBoxImage.Information);
            else
               new ListGuestRequest(guestRequests).ShowDialog();  //this button enable just if guest != null
        }
    }
}
