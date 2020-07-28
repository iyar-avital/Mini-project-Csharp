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
using System.Windows.Controls.DataVisualization.Charting;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for graph.xaml
    /// </summary>
    public partial class graph : Window
    {
        IBL myBL = FactoryBl.GetBl();
        public graph()
        {
            InitializeComponent();
            IBL myBL = FactoryBl.GetBl();
        }

        private void graph_Loaded(object sender, RoutedEventArgs e)
        {

            Dictionary<string, int> data = new Dictionary<string, int>();
            foreach (var item in myBL.GroupHostingUnitByNumOfPeople().AllKeysInGroup())
                data.Add(item.ToString(), myBL.GroupHostingUnitByNumOfPeople().AllElementsForKey(item).Count());
            ((ColumnSeries)num.Series[0]).ItemsSource = data;

            Dictionary<string, int> data1 = new Dictionary<string, int>();
            foreach (var item in myBL.GroupByAreaOfUnit().AllKeysInGroup())
                data1.Add(item.ToString(), myBL.GroupByAreaOfUnit().AllElementsForKey(item).Count());
            ((ColumnSeries)area.Series[0]).ItemsSource = data1;

            Dictionary<string, int> data2 = new Dictionary<string, int>();
            foreach (var item in myBL.GroupByTypeOfUnits().AllKeysInGroup())
                data2.Add(item.ToString(), myBL.GroupByTypeOfUnits().AllElementsForKey(item).Count());
            ((ColumnSeries)type.Series[0]).ItemsSource = data2;

            Dictionary<string, int> data3 = new Dictionary<string, int>();
            foreach (var item in myBL.GroupByNumOfClosedOrders().AllKeysInGroup())
                data3.Add(item.ToString(), myBL.GroupByNumOfClosedOrders().AllElementsForKey(item).Count());
            ((ColumnSeries)closed.Series[0]).ItemsSource = data3;

        }
        private void buttn_x_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}



