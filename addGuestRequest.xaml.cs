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
    /// Interaction logic for addGuestRequest.xaml
    /// </summary>
    public partial class addGuestRequest : Window
    {
        GuestRequest guestRequest = new GuestRequest();
        IBL myBL = FactoryBl.GetBl();
        public addGuestRequest(GuestRequest g = null)
        {
            InitializeComponent();
            guestRequest.Allguestrequestdetails = new Details();
            addGuestRequestGrid.DataContext = guestRequest;
            grTypeComboBox.ItemsSource = Enum.GetValues(typeof(TypeOfUnit));
            grAreaComboBox.ItemsSource = Enum.GetValues(typeof(AreaINCountry));
            grPollComboBox.ItemsSource = Enum.GetValues(typeof(Option));
            grJacuzziComboBox.ItemsSource = Enum.GetValues(typeof(Option));
            grGardenComboBox.ItemsSource = Enum.GetValues(typeof(Option));
            grChildrenAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(Option));
            if (g != null && g.GuestId != "_________")
            {
                guestRequest.GuestId = g.GuestId;
                guestRequest.Private_Name = g.Private_Name;
                guestRequest.Family_Name = g.Family_Name;
                guestRequest.MailAddress = g.MailAddress;
                guestIdMask.IsReadOnly = true;
                private_NameMask.IsReadOnly = true;
                family_NameMask.IsReadOnly = true;
                guestIdMask.BorderBrush = Brushes.LightGreen;
                private_NameMask.BorderBrush = Brushes.LightGreen;
                family_NameMask.BorderBrush = Brushes.LightGreen;
                mailAddressMask.BorderBrush = Brushes.LightGreen;
                guestIdMask.Tag = "true";
                private_NameMask.Tag = "true";
                family_NameMask.Tag = "true";
                mailAddressMask.Tag = "true";
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource guestRequestViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guestRequestViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guestRequestViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource detailsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("detailsViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // detailsViewSource.Source = [generic data source]
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (guestIdMask.Tag.Equals("false") || private_NameMask.Tag.Equals("false") || family_NameMask.Tag.Equals("false") ||
                     mailAddressMask.Tag.Equals("false") || grsubAreaMask.Tag.Equals("false") || grAdultsUpDown.Tag.Equals("false") || 
                     entryDateDatePicker.Tag.Equals("false") || releaseDateDatePicker.Tag.Equals("false"))
                    MessageBox.Show("please check you filled all the details correctly", "failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    myBL.addGuestRequest(guestRequest);
                    MessageBox.Show("The guest request was successfully added:)", "add", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (guestIdMask.IsReadOnly == false)
                    {
                        guestRequest.GuestId = "";
                        guestRequest.Private_Name = "";
                        guestRequest.Family_Name = "";
                        guestRequest.MailAddress = "";
                        guestIdMask.BorderBrush = Brushes.Black;
                        private_NameMask.BorderBrush = Brushes.Black;
                        family_NameMask.BorderBrush = Brushes.Black;
                        mailAddressMask.BorderBrush = Brushes.Black;
                        guestIdMask.Tag = "false";
                        private_NameMask.Tag = "false";
                        family_NameMask.Tag = "false";
                        mailAddressMask.Tag = "false";
                    }
                    guestRequest.Allguestrequestdetails.Adults = 0;
                    guestRequest.Allguestrequestdetails.Children = 0;
                    guestRequest.Allguestrequestdetails.Area = 0;
                    guestRequest.Allguestrequestdetails.ChildrensAttractions = 0;
                    guestRequest.Allguestrequestdetails.Garden = 0;
                    guestRequest.Allguestrequestdetails.Jacuzzi = 0;
                    guestRequest.Allguestrequestdetails.Pool = 0;
                    guestRequest.Allguestrequestdetails.Type = 0;
                    guestRequest.Allguestrequestdetails.SubArea = "";
                    guestRequest.Allguestrequestdetails.IsAirConditioner = false;
                    guestRequest.Allguestrequestdetails.IsWifi = false;
                    guestRequest.ReleaseDate = new DateTime();
                    guestRequest.EntryDate = new DateTime();
                    grsubAreaMask.BorderBrush = Brushes.Black;
                    grAdultsUpDown.BorderBrush = Brushes.Black;
                    ChildrenUpDownn.BorderBrush = Brushes.Black;
                    releaseDateDatePicker.BorderBrush = Brushes.Black;
                    entryDateDatePicker.BorderBrush = Brushes.Black;
                    grsubAreaMask.Tag = "false";
                    grAdultsUpDown.Tag = "false";
                    entryDateDatePicker.Tag = "false";
                    releaseDateDatePicker.Tag = "false";
                }
            }
            catch (HostingException h)
            {
                MessageBox.Show(h.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void guestIdMask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (guestIdMask.Text == "_________" || !BE.Validation.IsId(guestIdMask.Text) || guestIdMask.Text.Contains("_"))
            {
                guestIdMask.BorderBrush = Brushes.Red;
                GuestIdError.Visibility = Visibility.Visible;
                guestIdMask.Tag = "false";
            }
            else
            {
                guestIdMask.BorderBrush = Brushes.LightGreen;
                GuestIdError.Visibility = Visibility.Collapsed;
                guestIdMask.Tag = "true";
            }
        }

        private void private_NameMask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (private_NameMask.Text == "" || !BE.Validation.IsName(private_NameMask.Text))
            {
                private_NameMask.BorderBrush = Brushes.Red;
                grPrivateNameError.Visibility = Visibility.Visible;
                private_NameMask.Tag = "false";
            }
            else
            {
                private_NameMask.BorderBrush = Brushes.LightGreen;
                grPrivateNameError.Visibility = Visibility.Collapsed;
                private_NameMask.Tag = "true";
            }
        }

        private void family_NameMask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (family_NameMask.Text == "" || !BE.Validation.IsName(family_NameMask.Text))
            {
                family_NameMask.BorderBrush = Brushes.Red;
                grFamilyNameError.Visibility = Visibility.Visible;
                family_NameMask.Tag = "false";
            }
            else
            {
                family_NameMask.BorderBrush = Brushes.LightGreen;
                grFamilyNameError.Visibility = Visibility.Collapsed;
                family_NameMask.Tag = "true";
            }
        }

        private void mailAddressMask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (mailAddressMask.Text == "" || !BE.Validation.IsEmail(mailAddressMask.Text))
            {
                mailAddressMask.BorderBrush = Brushes.Red;
                grMailAddressError.Visibility = Visibility.Visible;
                mailAddressMask.Tag = "false";
            }
            else
            {
                mailAddressMask.BorderBrush = Brushes.LightGreen;
                grMailAddressError.Visibility = Visibility.Collapsed;
                mailAddressMask.Tag = "true";
            }
        }

        private void grsubAreaMask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (grsubAreaMask.Text == "" || !BE.Validation.IsName(grsubAreaMask.Text))
            {
                grsubAreaMask.BorderBrush = Brushes.Red;
                grSubAreaError.Visibility = Visibility.Visible;
                grsubAreaMask.Tag = "false";
            }
            else
            {
                grsubAreaMask.BorderBrush = Brushes.LightGreen;
                grSubAreaError.Visibility = Visibility.Collapsed;
                grsubAreaMask.Tag = "true";
            }
        }

        private void grAdultsUpDown_LostFocus(object sender, RoutedEventArgs e)
        {
            if (grAdultsUpDown.Text == "0")
            {
                grAdultsUpDown.BorderBrush = Brushes.Red;
                grAdultsError.Visibility = Visibility.Visible;
                grAdultsUpDown.Tag = "false";
            }
            else
            {
                grAdultsUpDown.BorderBrush = Brushes.LightGreen;
                grAdultsError.Visibility = Visibility.Collapsed;
                grAdultsUpDown.Tag = "true";
            }
        }

        private void ChildrenUpDownn_LostFocus(object sender, RoutedEventArgs e)
        {
            ChildrenUpDownn.BorderBrush = Brushes.LightGreen;
            ChildrenUpDownn.Tag = "true";
        }

        private void entryDateDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (entryDateDatePicker.SelectedDate < DateTime.Now)
            {
                entryDateDatePicker.BorderBrush = Brushes.Red;
                entryDateDatePicker.Tag = "false";
            }
            else
            {
                entryDateDatePicker.BorderBrush = Brushes.Green;
                entryDateDatePicker.Tag = "true";
            }
        }

        private void releaseDateDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (releaseDateDatePicker.SelectedDate <= entryDateDatePicker.SelectedDate)
            {
                releaseDateDatePicker.BorderBrush = Brushes.Red;
                releaseDateDatePicker.Tag = "false";
            }
            else
            {
                releaseDateDatePicker.BorderBrush = Brushes.Green;
                releaseDateDatePicker.Tag = "true";
            }
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
