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
    /// Interaction logic for Win_ReportList.xaml
    /// </summary>
    public partial class Win_ReportList : Window
    {
        public Win_ReportList()
        {
            InitializeComponent();
        }
       
        sellEntities database = new sellEntities();
        public string ReportName;
        Win_ShowReport wn;
        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private string ReportFormula() {
            string formull = "";
            switch(ReportName)
            {
                case "CustomerList.rpt" : {
                        if (rdb_all_coustoert.IsChecked == true)
                        {
                            formull = "";
                        }
                        else if (rdb_Active_Coustomer.IsChecked == true)
                        {
                            formull = "{VW_Coustomer.CoustomerActive} = 1";
                        }
                        else if (rdb_dactive_customer.IsChecked == true) {

                            formull = "{VW_Coustomer.CoustomerActive} = 2";
                        }
          
                        return formull;
                        break; 
  
                    }
                case "UserList.rpt": {
                        if (rdb_all_User.IsChecked == true)
                        {
                            formull = "";
                        }
                        else if (rdb_Active_User.IsChecked == true)
                        {
                            formull = "{VW_Users.UserActive}=1";
                        }
                        else if (rdb_dactive_User.IsChecked == true)
                        {

                            formull = "{VW_Users.UserActive}=2";
                        }

                        return formull;
                      
                        break;
                    }
                case "SellList.rpt": {
                     
                        wn.Params[0] = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_az.Text));
                        wn.Params[1] = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text));
                        formull = "{ VW_InvoiceShow.InvoiceDate} in '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_az.Text)) + "' to '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
                        if (rdb_all_invoice.IsChecked == true)
                        {
                            formull += "";
                        }
                        else if (rdb_sell_injvoice.IsChecked == true)
                        {
                            formull += " and {VW_InvoiceShow.InvoiceType} = 1";
                        }
                        else if (rdb_reject_invoice.IsChecked == true) {
                            formull += " and {VW_InvoiceShow.InvoiceType} = 2";
                        }
                        if (cmb_sell.SelectedValue != null) {
                            formull += " and {VW_InvoiceShow.CoustoemrId} =" + cmb_sell.SelectedValue;
                        }
                        
                        return formull;
                        break;
                    }
                case "Sell_Coustomer.rpt":
                    {

                        wn.Params[0] = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_az.Text));
                        wn.Params[1] = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text));

                        formull = "{ VW_InvoiceShow.InvoiceDate} in '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_az.Text)) + "' to '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
                        if (rdb_all_invoice.IsChecked == true)
                        {
                            formull += "";
                        }
                        else if (rdb_sell_injvoice.IsChecked == true)
                        {
                            formull += " and {VW_InvoiceShow.InvoiceType} = 1";
                        }
                        else if (rdb_reject_invoice.IsChecked == true)
                        {
                            formull += " and {VW_InvoiceShow.InvoiceType} = 2";
                        }
                        if (cmb_sell.SelectedValue != null)
                        {
                            formull += " and {VW_Coustomer.CoutomerID} =" + cmb_sell.SelectedValue;
                        }

                        return formull;
                        break;
                    }

            }
            return "";
        }
        private void listView_Report_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listView_Report.SelectedIndex == 1)
            {
                groupBox_filter_Sell.Visibility = Visibility.Hidden;
                groupBox_User.Visibility = Visibility.Hidden;
                groupBox_filter_Coustoemr.Visibility = Visibility.Visible;
                ReportName = "CustomerList.rpt";
            }
            else if (listView_Report.SelectedIndex == 2)
            {
                groupBox_filter_Sell.Visibility = Visibility.Hidden;
                groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
                groupBox_User.Visibility = Visibility.Visible;
                ReportName = "UserList.rpt";

            }
            else if (listView_Report.SelectedIndex == 6) {
                groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
                groupBox_User.Visibility = Visibility.Hidden;
                groupBox_filter_Sell.Visibility = Visibility.Visible;


                cmb_sell.ItemsSource = database.Coustomers.ToList();
                cmb_sell.DisplayMemberPath = "CoustomerName";
                cmb_sell.SelectedValuePath = "CoutomerID";
                ReportName = "SellList.rpt";
            }
            else if (listView_Report.SelectedIndex == 7)
            {
                groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
                groupBox_User.Visibility = Visibility.Hidden;
                groupBox_filter_Sell.Visibility = Visibility.Visible;


                cmb_sell.ItemsSource = database.Coustomers.ToList();
                cmb_sell.DisplayMemberPath = "CoustomerName";
                cmb_sell.SelectedValuePath = "CoutomerID";
                ReportName = "Sell_Coustomer.rpt";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (listView_Report.SelectedIndex == -1)
            {
                
                MessageBox.Show("لطفا موردی را از لیست انتخاب کنید");
                return;
            }

            wn = new Win_ShowReport();
            wn.ReportName = ReportName;
            wn.ShowFormoula = ReportFormula();
            wn.ShowDialog();
     
        }
        private void index1(object sender, MouseButtonEventArgs e)
        {
            groupBox_filter_Sell.Visibility = Visibility.Hidden;
            groupBox_User.Visibility = Visibility.Hidden;
            groupBox_filter_Coustoemr.Visibility = Visibility.Visible;
            ReportName = "CustomerList.rpt";
        }
        private void index2(object sender, MouseButtonEventArgs e)
        {
            groupBox_filter_Sell.Visibility = Visibility.Hidden;
            groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
            groupBox_User.Visibility = Visibility.Visible;
            ReportName = "UserList.rpt";

        }
        private void index6(object sender, MouseButtonEventArgs e)
        {
            groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
            groupBox_User.Visibility = Visibility.Hidden;
            groupBox_filter_Sell.Visibility = Visibility.Visible;


            cmb_sell.ItemsSource = database.Coustomers.ToList();
            cmb_sell.DisplayMemberPath = "CoustomerName";
            cmb_sell.SelectedValuePath = "CoutomerID";
            ReportName = "SellList.rpt";

        }
        private void index7(object sender, MouseButtonEventArgs e)
        {
            groupBox_filter_Coustoemr.Visibility = Visibility.Hidden;
            groupBox_User.Visibility = Visibility.Hidden;
            groupBox_filter_Sell.Visibility = Visibility.Visible;


            cmb_sell.ItemsSource = database.Coustomers.ToList();
            cmb_sell.DisplayMemberPath = "CoustomerName";
            cmb_sell.SelectedValuePath = "CoutomerID";
            ReportName = "Sell_Coustomer.rpt";

        }
    }
}
