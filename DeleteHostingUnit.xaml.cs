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
    /// Interaction logic for DeleteHostingUnit.xaml
    /// </summary>
    public partial class DeleteHostingUnit : Window
    {
        List<BE.HostingUnit> hostingUnits;
        IBL myBL = FactoryBl.GetBl();
        BE.HostingUnit unit;
        public DeleteHostingUnit(Host host)
        {
            InitializeComponent();
            hostingUnits = myBL.getHostingUnits(item => item.Owner.HostId == host.HostId);
            deleteHostingUnitGrid.DataContext = hostingUnits;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void KeycomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unit = hostingUnits[KeycomboBox.SelectedIndex];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you sure want to delete hosting unit "
                                                       + "name: " + unit.HostingUnitName 
                                                        , "delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    myBL.deleteHostingUnit(unit);
                    MessageBoxResult messageBox = MessageBox.Show("The Unit was successfully deleted:)", "delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.OK)
                        Close();
                }
                catch (HostingException h)
                {
                    MessageBox.Show(h.Message, "faild", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
