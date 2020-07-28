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
    /// Interaction logic for ListHostingUnit.xaml
    /// </summary>
    public partial class ListHostingUnit : Window
    {
        IBL myBL = FactoryBl.GetBl();
        List<AreaINCountry> AreaList;
        List<TypeOfUnit> TypeList;
        List<int> NumPeopleList;
        List<int> ClosedOrderList;
        ObservableCollection<BE.HostingUnit> allHostings;
        ObservableCollection<BE.HostingUnit> hostingUnits;
        int indexArea = -1;
        int indexType = -1;
        int indexPeople = -1;
        int indexOrder = -1;
        AreaINCountry SelectArea = 0;
        TypeOfUnit SelectType = 0;
        int SelectPeople = 0;
        int SelectClose = 0;
        public ListHostingUnit(ObservableCollection<BE.HostingUnit> H)
        {
            InitializeComponent();
            allHostings = new ObservableCollection<BE.HostingUnit>();
            hostingUnits = new ObservableCollection<BE.HostingUnit>();
            foreach (var item in H)
            {
                allHostings.Add(item);
            }
            Refresh(hostingUnits);
            HostingUnitGrid.DataContext = hostingUnits;
            AreaList = myBL.GroupByAreaOfUnit(item => item.Owner.HostId == H[0].Owner.HostId).AllKeysInGroup();
            TypeList = myBL.GroupByTypeOfUnits(item => item.Owner.HostId == H[0].Owner.HostId).AllKeysInGroup();
            NumPeopleList = myBL.GroupHostingUnitByNumOfPeople(item => item.Owner.HostId == H[0].Owner.HostId).AllKeysInGroup();
            ClosedOrderList = myBL.GroupByNumOfClosedOrders(item => item.Owner.HostId == H[0].Owner.HostId).AllKeysInGroup();
            AreaCombobox.ItemsSource = AreaList;
            TypeCombobox.ItemsSource = TypeList;
            NumPepoleCombobox.ItemsSource = NumPeopleList;
            CloseOrderCombobox.ItemsSource = ClosedOrderList;
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
            List<BE.HostingUnit> afterFilter = myBL.GroupByAreaOfUnit().AllElementsForKey(item);//.Where(item => item.Owner.HostId == hostingUnits[0].Owner.HostId).Select(x => x).ToList();
            indexArea = index;
            SelectArea = AreaList[indexArea];
            Refresh(hostingUnits, afterFilter);
        }

        private void TypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeCombobox.SelectedItem == null)
            {
                indexType = -1;
                TypeClear.Visibility = Visibility.Hidden;
                return;
            }
            TypeOfUnit typeOf = (TypeOfUnit)TypeCombobox.SelectedItem;
            TypeClear.Visibility = Visibility.Visible;
            TypeCombobox.IsEnabled = false;
            helpTypeCombobox_SelectionChanged(typeOf, TypeCombobox.SelectedIndex);
        }

        private void helpTypeCombobox_SelectionChanged(TypeOfUnit item, int index)
        {
            List<BE.HostingUnit> afterFilter = myBL.GroupByTypeOfUnits().AllElementsForKey(item);
            indexType = index;
            SelectType = TypeList[indexType];
            Refresh(hostingUnits, afterFilter);
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

        private void helpToNumPepoleCombobox_SelectionChanged(int item,int index)
        {         
            List<BE.HostingUnit> afterFilter = myBL.GroupHostingUnitByNumOfPeople().AllElementsForKey(item);
            indexPeople = index;
            SelectPeople = NumPeopleList[indexPeople];
            Refresh(hostingUnits, afterFilter);
        }

        private void CloseOrderCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CloseOrderCombobox.SelectedItem == null)
            {
                indexOrder = -1;
                CloseOrderClear.Visibility = Visibility.Hidden;
                return;
            }
            int CloOrdere = (int)CloseOrderCombobox.SelectedItem;       
            CloseOrderCombobox.IsEnabled = false;
            CloseOrderClear.Visibility = Visibility.Visible;
            helpCloseOrderCombobox_SelectionChanged(CloOrdere, CloseOrderCombobox.SelectedIndex);
        }

        private void helpCloseOrderCombobox_SelectionChanged(int item, int index)
        {
            List<BE.HostingUnit> afterFilter = myBL.GroupByNumOfClosedOrders().AllElementsForKey(item);
            indexOrder = index;
            SelectClose = ClosedOrderList[indexOrder];
            Refresh(hostingUnits, afterFilter);
        }

        private void Refresh(ObservableCollection<BE.HostingUnit> hu, List<BE.HostingUnit> afterfilter = null)//null for the first time, in the constructor
        {
            if (allHostings == null) return;
            if (hu.Count() == 0)
            {
                foreach (var item in allHostings)
                {
                    hu.Add(item);
                }
            }
            if (afterfilter != null)
            {
                for (int i = 0; i < hu.Count(); i++)
                {
                    bool flag = true;
                    for (int j = 0; j < afterfilter.Count(); j++)
                    {
                        if (hu[i].HostingUnitKey == afterfilter[j].HostingUnitKey)
                        {
                            flag = false;
                            j = afterfilter.Count();   //stop second for, break stop just the if and continue in for
                        }
                    }
                    if (flag == true)
                    {
                        hu.Remove(hu[i]);
                        i--;    //when item removed, count of list going down one, and we are "spend" the iteration of deleted item, so we lose the last item!!, because of this we does i--
                    }
                }
            }
        }

        private void AreaClear_Click(object sender, RoutedEventArgs e)
        {
            hostingUnits.Clear();

            if (indexType != -1)
                helpTypeCombobox_SelectionChanged(SelectType, indexType);
            if (indexPeople != -1)
                helpToNumPepoleCombobox_SelectionChanged(SelectPeople, indexPeople);
            if (indexOrder != -1)
                helpCloseOrderCombobox_SelectionChanged(SelectClose, indexOrder);
            AreaCombobox.SelectedItem = null;
            AreaCombobox.IsEnabled = true;
            Refresh(hostingUnits);  //if all the combobox were empty, the refresh dend happened because all the index == -1, and we done clear, but we need all!
        }

        private void TypeClear_Click(object sender, RoutedEventArgs e)
        {
            hostingUnits.Clear();

            if (indexArea != -1)
                helpAreaCombobox_SelectionChanged(SelectArea, indexArea);
            if (indexPeople != -1)
                helpToNumPepoleCombobox_SelectionChanged(SelectPeople, indexPeople);
            if (indexOrder != -1)
                helpCloseOrderCombobox_SelectionChanged(SelectClose, indexOrder);
            TypeCombobox.SelectedItem = null;
            TypeCombobox.IsEnabled = true;
            Refresh(hostingUnits);
        }

        private void NumPepoleClear_Click(object sender, RoutedEventArgs e)
        {
            hostingUnits.Clear();

            if (indexArea != -1)
                helpAreaCombobox_SelectionChanged(SelectArea, indexArea);
            if (indexType != -1)
                helpTypeCombobox_SelectionChanged(SelectType, indexType);
            if (indexOrder != -1)
                helpCloseOrderCombobox_SelectionChanged(SelectClose, indexOrder);
            NumPepoleCombobox.SelectedItem = null;
            NumPepoleCombobox.IsEnabled = true;
            Refresh(hostingUnits);
        }

        private void CloseOrderClear_Click(object sender, RoutedEventArgs e)
        {
            hostingUnits.Clear();

            if (indexArea != -1)
                helpAreaCombobox_SelectionChanged(SelectArea, indexArea);
            if (indexType != -1)
                helpTypeCombobox_SelectionChanged(SelectType, indexType);
            if (indexPeople != -1)
                helpToNumPepoleCombobox_SelectionChanged(SelectPeople, indexPeople);
            CloseOrderCombobox.SelectedItem = null;
            CloseOrderCombobox.IsEnabled = true;
            Refresh(hostingUnits);
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void allDatails_Click(object sender, RoutedEventArgs e)
        {
            BE.HostingUnit val = HostingUnitGrid.SelectedItem as BE.HostingUnit;
            if (val != null)
                MessageBox.Show($"Datails of Hosting Unit: \n{val}", "all Datails", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void calanderButton_Click(object sender, RoutedEventArgs e)
        {
            BE.HostingUnit val = HostingUnitGrid.SelectedItem as BE.HostingUnit;
            new Calander(val).Show();
        }
    }
}
