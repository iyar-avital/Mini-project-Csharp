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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Calander.xaml
    /// </summary>
    public partial class Calander : Window
    {
        public Calander(BE.HostingUnit h)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Calendar myCalander = new Calendar();
            DateTime d = new DateTime(2020, 01, 01);
            while (d.Year != 2021)
            {
                if (h[d] == true)
                {
                    myCalander.BlackoutDates.Add(new CalendarDateRange(d));
                }
                d = d.AddDays(1);
            }
            CalanderBox.Child = myCalander;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
