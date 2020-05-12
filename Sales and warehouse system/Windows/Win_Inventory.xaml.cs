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
    /// Interaction logic for Win_Customer.xaml
    /// </summary>
    public partial class Win_Inventory : Window
    {
        public Win_Inventory()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        public string ProducteName;
        public int ProductID;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowCustomerINfo(SearchforProductPrice);
            Showlbl();
            Showcmb();
            

        }

        private void Showcmb()
        {
           comboBox.Items.Add("همه تراکنش ها");
            comboBox.Items.Add("اضافه به انبار");
            comboBox.Items.Add("کسر از انبار");
            comboBox.SelectedIndex = 0;
        }

        private void Showlbl()
        {
            lbl_Prodauct.Content = ProducteName;
        }

        private void ShowCustomerINfo(Func<string> SearchforUser)
        {
            var query = database.Database.SqlQuery<VW_Inventory>("select * from VW_Inventory where 1=1 and ProductID=" + ProductID +' '+ SearchforProductPrice());

            var U = query.ToList();
            dataGrid_Product.ItemsSource = U.ToList();
        }
        private string SearchforProductPrice()
        {
            string SearchString = "and InventoryDate Between'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calender_az.Text)) + "' And '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
            if (!string.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                SearchString += "and FullName Like N'%" + txt_name.Text.Trim() + "%'";
            }
            if (comboBox.SelectedIndex == 1)
            {

                SearchString += "and InventoryCount>=1";

            }
            else
                if (comboBox.SelectedIndex == 2) {

                SearchString += "and InventoryCount<0";
            }
           




            return SearchString;
        }

        private void imagesearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowCustomerINfo(SearchforProductPrice);
        }



        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Win_AddNewTrans wn = new Win_AddNewTrans();
            wn.ProductName = this.ProducteName;
            wn.ProductID = this.ProductID;
            wn.ShowDialog();
            ShowCustomerINfo(SearchforProductPrice);

        }
    }
}
