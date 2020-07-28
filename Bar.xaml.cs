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
using System.Windows.Navigation;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Controls.DataVisualization.Charting;
using System.IO;
using System.Diagnostics;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Bar.xaml
    /// </summary>
    public partial class Bar : Window
    {
        IBL myBL = FactoryBl.GetBl();
        public Bar()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void Bar_Loaded(object sender, RoutedEventArgs e)
        {
            List<GuestRequest> guests = myBL.getGuestRequests();

            ((PieSeries)wifi.Series[0]).ItemsSource = new KeyValuePair<string, int>[] {
                        new KeyValuePair<string, int>("want wifi", guests.Count(x=>x.Allguestrequestdetails.IsWifi == true)),
                        new KeyValuePair<string, int>("don't want wifi", guests.Count(x=>x.Allguestrequestdetails.IsWifi == false)) };


            ((PieSeries)pool.Series[0]).ItemsSource = new KeyValuePair<string, int>[] {
                        new KeyValuePair<string, int>("want pool", guests.Count(x=>x.Allguestrequestdetails.Pool == Option.Necessary)),
                        new KeyValuePair<string, int>("don't care", guests.Count(x=>x.Allguestrequestdetails.Pool == Option.possible)),
                        new KeyValuePair<string, int>("don't want pool", guests.Count(x=>x.Allguestrequestdetails.Pool == Option.uninterested))
            };

            ((PieSeries)jacuzzi.Series[0]).ItemsSource = new KeyValuePair<string, int>[] {
                        new KeyValuePair<string, int>("want jacuzzi", guests.Count(x=>x.Allguestrequestdetails.Jacuzzi == Option.Necessary)),
                        new KeyValuePair<string, int>("don't care", guests.Count(x=>x.Allguestrequestdetails.Jacuzzi == Option.possible)),
                        new KeyValuePair<string, int>("don't want jacuzzi", guests.Count(x=>x.Allguestrequestdetails.Jacuzzi == Option.uninterested))
            };


            ((PieSeries)airConditioner.Series[0]).ItemsSource = new KeyValuePair<string, int>[] {
                        new KeyValuePair<string, int>("want airConditioner", guests.Count(x=>x.Allguestrequestdetails.IsAirConditioner == true)),
                        new KeyValuePair<string, int>("don't want airConditioner", guests.Count(x=>x.Allguestrequestdetails.IsAirConditioner == false)) };
        }

        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

