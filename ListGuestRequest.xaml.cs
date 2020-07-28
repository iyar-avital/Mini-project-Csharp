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
    /// Interaction logic for ListGuestRequest.xaml
    /// </summary>
    public partial class ListGuestRequest : Window
    {
        IBL myBL = FactoryBl.GetBl();
        List<AreaINCountry> AreaList;
        List<int> NumPeopleList;
        List<StatusCostumer> StatusList;
        ObservableCollection<GuestRequest> allGuestRequests;
        ObservableCollection<GuestRequest> guestRequests;
        int indexArea = -1;
        int indexPeople = -1;
        int indexStatus = -1;
        AreaINCountry SelectArea = 0;
        int SelectPeople = 0;
        StatusCostumer SelectStatus = 0;
        public ListGuestRequest(ObservableCollection<GuestRequest> G)
        {
            InitializeComponent();
            allGuestRequests = new ObservableCollection<GuestRequest>();
            guestRequests = new ObservableCollection<GuestRequest>();
            foreach (var item in G)
            {
                allGuestRequests.Add(item);
            }
                Refresh(guestRequests);
                GuestRequestGrid.DataContext = guestRequests;
                AreaList = myBL.groupByArea(item => item.GuestId == G[0].GuestId).AllKeysInGroup();
                NumPeopleList = myBL.groupByNumOfPeople(item => item.GuestId == G[0].GuestId).AllKeysInGroup();
                StatusList = myBL.groupByStatus(item => item.GuestId == G[0].GuestId).AllKeysInGroup();
                AreaCombobox.ItemsSource = AreaList;
                NumPepoleCombobox.ItemsSource = NumPeopleList;
                StatusCombobox.ItemsSource = StatusList;
        }

        private void AreaCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AreaCombobox.SelectedItem == null)
            {
                indexArea = -1;
                AreaClear.Visibility = Visibility.Hidden;
                return;
            }
            AreaINCountry areaIN = (AreaINCountry)AreaCombobox.SelectedItem;
            AreaClear.Visibility = Visibility.Visible;
            AreaCombobox.IsEnabled = false;
            helpAreaCombobox_SelectionChanged(areaIN, AreaCombobox.SelectedIndex);
        }

        private void helpAreaCombobox_SelectionChanged(AreaINCountry item, int index)
        {
            List<GuestRequest> afterFilter = myBL.groupByArea().AllElementsForKey(item);
            indexArea = index;
            SelectArea = AreaList[indexArea];
            Refresh(guestRequests, afterFilter);
        }

        private void NumPepoleCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NumPepoleCombobox.SelectedItem == null)
            {
                indexPeople = -1;
                NumPepoleClear.Visibility = Visibility.Hidden;
                return;
            }
            int NumPeople = (int)NumPepoleCombobox.SelectedItem;
            NumPepoleCombobox.IsEnabled = false;
            NumPepoleClear.Visibility = Visibility.Visible;
            helpToNumPepoleCombobox_SelectionChanged(NumPeople, NumPepoleCombobox.SelectedIndex);
        }

        private void helpToNumPepoleCombobox_SelectionChanged(int item, int index)
        {
            List<GuestRequest> afterFilter = myBL.groupByNumOfPeople().AllElementsForKey(item);
            indexPeople = index;
            SelectPeople = NumPeopleList[indexPeople];
            Refresh(guestRequests, afterFilter);
        }

        private void StatusCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusCombobox.SelectedItem == null)
            {
                indexStatus = -1;
                StatusClear.Visibility = Visibility.Hidden;
                return;
            }
            StatusCostumer Status = (StatusCostumer)StatusCombobox.SelectedItem;
            StatusCombobox.IsEnabled = false;
            StatusClear.Visibility = Visibility.Visible;
            helpStatusCombobox_SelectionChanged(Status, StatusCombobox.SelectedIndex);
        }

        private void helpStatusCombobox_SelectionChanged(StatusCostumer item, int index)
        {
            List<GuestRequest> afterFilter = myBL.groupByStatus().AllElementsForKey(item);
            indexStatus = index;
            SelectStatus = StatusList[indexStatus];
            Refresh(guestRequests, afterFilter);
        }

        private void Refresh(ObservableCollection<GuestRequest> gu, List<GuestRequest> afterfilter = null)//null for the first time, in the constructor
        {
            if (allGuestRequests == null) return;
            if (gu.Count() == 0)
            {
                foreach (var item in allGuestRequests)
                {
                    gu.Add(item);
                }
            }
            if (afterfilter != null)
            {
                for (int i = 0; i < gu.Count(); i++)
                {
                    bool flag = true;
                    for (int j = 0; j < afterfilter.Count(); j++)
                    {
                        if (gu[i].GuestRequestKey == afterfilter[j].GuestRequestKey)
                        {
                            flag = false;
                            j = afterfilter.Count();   //stop second for, break stop just the if and continue in for
                        }
                    }
                    if (flag == true)
                    {
                        gu.Remove(gu[i]);
                        i--;    //when item removed, count of list going down one, and we are "spend" the iteration of deleted item, so we lose the last item!!, because of this we does i--
                    }
                }
            }
        }

        private void AreaClear_Click(object sender, RoutedEventArgs e)
        {
            guestRequests.Clear();

            if (indexPeople != -1)
                helpToNumPepoleCombobox_SelectionChanged(SelectPeople, indexPeople);
            if (indexStatus != -1)
                helpStatusCombobox_SelectionChanged(SelectStatus, indexStatus);
            AreaCombobox.SelectedItem = null;
            AreaCombobox.IsEnabled = true;
            Refresh(guestRequests);  //if all the combobox were empty, the refresh dend happened because all the index == -1, and we done clear, but we need all!
        }

        private void NumPepoleClear_Click(object sender, RoutedEventArgs e)
        {
            guestRequests.Clear();

            if (indexArea != -1)
                helpAreaCombobox_SelectionChanged(SelectArea, indexArea);
            if (indexStatus != -1)
                helpStatusCombobox_SelectionChanged(SelectStatus, indexStatus);
            NumPepoleCombobox.SelectedItem = null;
            NumPepoleCombobox.IsEnabled = true;
            Refresh(guestRequests);
        }

        private void StatusClear_Click(object sender, RoutedEventArgs e)
        {
            guestRequests.Clear();

            if (indexArea != -1)
                helpAreaCombobox_SelectionChanged(SelectArea, indexArea);
            if (indexPeople != -1)
                helpToNumPepoleCombobox_SelectionChanged(SelectPeople, indexPeople);
            StatusCombobox.SelectedItem = null;
            StatusCombobox.IsEnabled = true;
            Refresh(guestRequests);
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void allDatails_Click(object sender, RoutedEventArgs e)
        {
            GuestRequest val = GuestRequestGrid.SelectedItem as GuestRequest;
            if (val != null)
                MessageBox.Show($"Datails of Guest Request: \n{val}", "all Datails", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}



      

       



       

      
    
