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
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Profit.xaml
    /// </summary>
    public partial class Profit : Window
    {
        IBL myBL = FactoryBl.GetBl();
        public Profit()
        {
            InitializeComponent();
            int sum = 0;
            foreach (var item in myBL.getOrders())
            {
                sum += item.BookingFee;
            }
            profit.DataContext = sum;
            motarot.Visibility = Visibility.Visible;
            profit.Content = sum.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int commi;
            if (int.TryParse(newComission.Text, out commi) == true)
            {
                myBL.updateCommission(Convert.ToInt32(newComission.Text));
               MessageBoxResult m = MessageBox.Show("commission successfully changrd", "(:", MessageBoxButton.OK, MessageBoxImage.Information);
                if (m == MessageBoxResult.OK)
                    newComission.Text = "";
            }
            else
                MessageBox.Show("please enter numbers only", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void newComission_GotFocus(object sender, RoutedEventArgs e)
        {
            newComission.Text = "";
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
