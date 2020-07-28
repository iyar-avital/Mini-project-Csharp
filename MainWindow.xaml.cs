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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBL = FactoryBl.GetBl();
        public MainWindow()
        {
            InitializeComponent();
            myBL.MonthThread();
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            new Guest().ShowDialog();
        }

        private void hosting_unit_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnit().ShowDialog();
        }

        private void site_Owner_Click(object sender, RoutedEventArgs e)
        {
            new SiteOwner().ShowDialog();
        }
 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Bar().ShowDialog();
        }

        private void MoreDetails_Click(object sender, RoutedEventArgs e)
        {
            new MoreInformationForEveryOne().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
