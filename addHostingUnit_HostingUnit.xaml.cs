using Microsoft.Win32;
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
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for addHostingUnit_HostingUnit.xaml
    /// </summary>
    public partial class addHostingUnit_HostingUnit : Window
    {
        BE.HostingUnit hostingUnit;
        List<BE.HostingUnit> hostingUnitKeys;
        IBL myBL = FactoryBl.GetBl();
        int IndexOfPictures = 0;
        string myaddOrUpdate;
        public addHostingUnit_HostingUnit(string addOrupadate, Host host)
        {
            InitializeComponent();
            hostingUnit = new BE.HostingUnit();
            hostingUnit.AllhostingUnitdetails = new Details();
            hostingUnit.Owner = host;
            hostingUnit.Diary = new bool[12, 31];
            myaddOrUpdate = addOrupadate;
            hTypeComboBox.ItemsSource = Enum.GetValues(typeof(TypeOfUnit));
            hAreaComboBox.ItemsSource = Enum.GetValues(typeof(AreaINCountry));
            hPollComboBox.ItemsSource = Enum.GetValues(typeof(YesOrNot));
            hJacuzziComboBox.ItemsSource = Enum.GetValues(typeof(YesOrNot));
            hGardenComboBox.ItemsSource = Enum.GetValues(typeof(YesOrNot));
            hChildrenAttractionsComboBox.ItemsSource = Enum.GetValues(typeof(YesOrNot));
            grid1.DataContext = hostingUnit;
            grid2.DataContext = hostingUnit.AllhostingUnitdetails;
            if (addOrupadate == "update")
            {
                addHostingUnit_HostingUnitGrid.Visibility = Visibility.Collapsed;
                hostingUnitKeys = myBL.getHostingUnits(item => item.Owner.HostId == hostingUnit.Owner.HostId);
                UpdateHostingUnitGrid.DataContext = hostingUnitKeys;
                UpdateHostingUnitGrid.Visibility = Visibility.Visible;
            }
        }

        private void KeycomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hostingUnit = hostingUnitKeys[KeycomboBox.SelectedIndex];
            UpdateHostingUnitGrid.Visibility = Visibility.Collapsed;
            grid1.DataContext = hostingUnit;
            grid2.DataContext = hostingUnit.AllhostingUnitdetails;
            chooseItemButton.Visibility = Visibility.Collapsed;
            Rectangale.Visibility = Visibility.Collapsed;
            grid2.Margin = new Thickness(73,242,106,648);
            AddOrUpdateButton.Margin = new Thickness(408, 460, 0, 0);
            grid2.Height = 400;
            grid2.Width = 600;
            AddOrUpdateButton.Content = "update Hosting unit";
            ParkingPriceUpDown.Tag = "true";
            SubAreaMask.Tag = "true";
            AdultsUpDown.Tag = "true";
            ChildrenUpDown.Tag = "true";
            HostingUnitNameMask.Tag = "true";
            addHostingUnit_HostingUnitGrid.Visibility = Visibility.Visible;
        }            

        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SubAreaMask.Tag.Equals("false") ||
                    AdultsUpDown.Tag.Equals("false") || HostingUnitNameMask.Tag.Equals("false"))
                    MessageBox.Show("please check you filled all the details correctly", "failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (myaddOrUpdate == "add")
                    {
                        myBL.addHostingUnit(hostingUnit);
                        MessageBox.Show("The Unit was successfully added:)", "add", MessageBoxButton.OK, MessageBoxImage.Information);
                        hostingUnit.HostingUnitName = "";
                        hostingUnit.HostingUnitKey = 0;
                        hostingUnit.ParkingPrice = 0;
                        hostingUnit.Pictures = null;
                        hostingUnit.AllhostingUnitdetails.Adults = 0;
                        hostingUnit.AllhostingUnitdetails.Children = 0;
                        hostingUnit.AllhostingUnitdetails.Area = 0;
                        hostingUnit.AllhostingUnitdetails.ChildrensAttractions = 0;
                        hostingUnit.AllhostingUnitdetails.Garden = 0;
                        hostingUnit.AllhostingUnitdetails.Jacuzzi = 0;
                        hostingUnit.AllhostingUnitdetails.Pool = 0;
                        hostingUnit.AllhostingUnitdetails.Type = 0;
                        hostingUnit.AllhostingUnitdetails.SubArea = "";
                        hostingUnit.AllhostingUnitdetails.IsAirConditioner = false;
                        hostingUnit.AllhostingUnitdetails.IsWifi = false;
                        hostingUnit.Pictures = new List<Uri>();
                        ImagesGrid.Source = new BitmapImage();
                        ParkingPriceUpDown.Tag = "false";
                        ParkingPriceUpDown.BorderBrush = Brushes.Black;
                        SubAreaMask.Tag = "false";
                        SubAreaMask.BorderBrush = Brushes.Black;
                        AdultsUpDown.Tag = "false";
                        AdultsUpDown.BorderBrush = Brushes.Black;
                        HostingUnitNameMask.Tag = "false";
                        HostingUnitNameMask.BorderBrush = Brushes.Black;
                        ChildrenUpDown.Tag = "false";
                        ChildrenUpDown.BorderBrush = Brushes.Black;
                    }
                    else
                    {
                        myBL.updateHostingUnit(hostingUnit);
                        MessageBox.Show("The Unit was successfully update:)", "update", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (HostingException h)
            {
                MessageBox.Show(h.Message + " " + h.nameObject, "faild", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void hostingUnitNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (HostingUnitNameMask.Text == "" || !BE.Validation.IsName(HostingUnitNameMask.Text))
            {
                HostingUnitNameMask.BorderBrush = Brushes.Red;
                hostingUnitNameError.Visibility = Visibility.Visible;
                HostingUnitNameMask.Tag = "false";
            }
            else
            {
                HostingUnitNameMask.BorderBrush = Brushes.LightGreen;
                hostingUnitNameError.Visibility = Visibility.Collapsed;
                HostingUnitNameMask.Tag = "true";
            }
        }

        private void parkingPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ParkingPriceUpDown.BorderBrush = Brushes.LightGreen;
            ParkingPriceUpDown.Tag = "true";
        }

        private void hsubAreaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SubAreaMask.Text == "" || !BE.Validation.IsName(SubAreaMask.Text))
            {
                SubAreaMask.BorderBrush = Brushes.Red;
                subAreaError.Visibility = Visibility.Visible;
                SubAreaMask.Tag = "false";
            }
            else
            {
                SubAreaMask.BorderBrush = Brushes.LightGreen;
                subAreaError.Visibility = Visibility.Collapsed;
                SubAreaMask.Tag = "true";
            }
        }

        private void hadultsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AdultsUpDown.Text == "0")
            {
                AdultsUpDown.BorderBrush = Brushes.Red;
                adultsError.Visibility = Visibility.Visible;
                AdultsUpDown.Tag = "false";
            }
            else
            {
                AdultsUpDown.BorderBrush = Brushes.LightGreen;
                adultsError.Visibility = Visibility.Collapsed;
                AdultsUpDown.Tag = "true";
            }
        }

        private void childrenTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ChildrenUpDown.BorderBrush = Brushes.LightGreen;
            ChildrenUpDown.Tag = "true";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChooseItemClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                List<Uri> uris = new List<Uri>();
                openFileDialog.FileNames.ToList().ForEach(item => uris.Add(new Uri(item)));
                if (hostingUnit.Pictures == null)
                    hostingUnit.Pictures = new List<Uri>();
                hostingUnit.Pictures.AddRange(uris);
                ImagesGrid.Source = new BitmapImage(hostingUnit.Pictures[0]);
                if (hostingUnit.Pictures.Count == 1)
                    allowbutton.Visibility = Visibility.Collapsed;
            }
        }

        private void ImageClick(object sender, RoutedEventArgs e)
        {
            if (IndexOfPictures == hostingUnit.Pictures.Count() - 1)
                IndexOfPictures = 0;
            ImagesGrid.Source = new BitmapImage(hostingUnit.Pictures[++IndexOfPictures]);
        }

    }
}
