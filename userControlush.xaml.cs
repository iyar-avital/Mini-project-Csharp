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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for userControlush.xaml
    /// </summary>
    public partial class userControlush : UserControl 
    {
        IBL myBL = FactoryBl.GetBl();
        GuestRequest guestUser;
        Host HostUser;
        public event EventHandler CostumerChange;
        ValueUserControl ee;

        public userControlush(Object obj)
        {
            InitializeComponent();
            guestUser = obj as GuestRequest;
            HostUser = obj as Host;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordMask.Password == "")
            {
                MessageBox.Show("enter your password", "empty password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (HostUser != null)
                {
                    List<BE.Host> hosts = myBL.getHosts(item => passwordMask.Password == item.HostId.Substring(5, 4));
                    if (hosts.Count() == 0)
                    {
                        MessageBox.Show("please sign up", "Connection failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        passwordMask.Password = "";
                    }
                    else
                    {
                        //העתקה עמוקה
                        HostUser.HostId = hosts[0].HostId;
                        HostUser.PrivateName = hosts[0].PrivateName;
                        HostUser.FamilyName = hosts[0].FamilyName;
                        HostUser.PhoneNumber = hosts[0].PhoneNumber;
                        HostUser.MailAdress = hosts[0].MailAdress;
                        HostUser.BankAccountNumber = hosts[0].BankAccountNumber;
                        HostUser.CollectionClearance = hosts[0].CollectionClearance;
                        HostUser.IsInPlace = hosts[0].IsInPlace;
                        HostUser.Languages = hosts[0].Languages;
                        HostUser.BankBranchDetails = hosts[0].BankBranchDetails;
                        passwordMask.Password = "";
                        ee = new ValueUserControl(HostOrGuest.host);
                        if (CostumerChange != null)
                            CostumerChange(this, ee);
                    }
                }
                if (guestUser != null)
                {
                    List<GuestRequest> guestRequests = myBL.getGuestRequests(item => passwordMask.Password == item.GuestId.Substring(5, 4));
                    if (guestRequests.Count() == 0)
                    {
                        MessageBox.Show("you dont have guest request", "Connection failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        passwordMask.Password = "";
                    }
                    else
                    {
                        guestUser.GuestId = guestRequests[0].GuestId;
                        guestUser.Family_Name = guestRequests[0].Family_Name;
                        guestUser.Private_Name = guestRequests[0].Private_Name;
                        guestUser.MailAddress = guestRequests[0].MailAddress;
                        passwordMask.Password = "";
                        ee = new ValueUserControl(HostOrGuest.guest);
                        if (CostumerChange != null)
                            CostumerChange(this, ee);
                    }
                }
            }
        }
    }
}
