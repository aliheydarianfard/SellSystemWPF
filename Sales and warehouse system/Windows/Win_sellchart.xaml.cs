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
using ModelLayer;
namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_sellchart.xaml
    /// </summary>
    public partial class Win_sellchart : Window
    {
        public Win_sellchart()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            List<KeyValuePair<string, long>> ChartValue = new List<KeyValuePair<string, long>>();
            
            if (rdb_Sell_day.IsChecked == true) {
                var query = database.sp_sell_Chart();
                var result = query.ToList();
                for (int i = 0; i < result.Count; i++) {
                    ChartValue.Add(new KeyValuePair<string, long>(result[i].InvoiceDate, Convert.ToInt64(result[i].TotalPrice)));

                }
                Chart_sell.DataContext = ChartValue;
                    }
            if (rdb_sell_coustpmer.IsChecked == true)
            {
                var query = database.sp_CoustomersellChart();
                var result = query.ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    ChartValue.Add(new KeyValuePair<string, long>(result[i].CoustomerName, Convert.ToInt64(result[i].totalprice)));

                }
                Chart_sell.DataContext = ChartValue;
            }
            if (rdb_sell_Proudact.IsChecked == true)
            {
                var query = database.sp_prouductsellpricechart();
                var result = query.ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    ChartValue.Add(new KeyValuePair<string, long>(result[i].ProductName, Convert.ToInt64(result[i].totalprice)));

                }
                Chart_sell.DataContext = ChartValue;
            }
            if (rdb_sell_Proudact_count.IsChecked == true)
            {
                var query = database.sp_prouductsellcountchart();
                var result = query.ToList();
                for (int i = 0; i < result.Count; i++)
                {
                    ChartValue.Add(new KeyValuePair<string, long>(result[i].ProductName, Convert.ToInt64(result[i].totalcount)));

                }
                Chart_sell.DataContext = ChartValue;
            }
         
        }

        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
