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
    public partial class Win_Customer : Window
    {
        public Win_Customer()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowCustomerINfo(SearchStatement);
        }

        private void ShowCustomerINfo(Func<string> SearchforUser)
        {
            var query = database.Database.SqlQuery<VW_Coustomer>("select * from VW_Coustomer Where 1=1" + SearchforUser());
            var U = query.ToList();
            dataGrid_Coustomer.ItemsSource = U.ToList();
        }
        private string SearchStatement()
        {
            string SearchString = "and SetDate Between'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calender_az.Text)) + "' And '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
            if (!string.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                SearchString += "and CoustomerName Like N'%" + txt_name.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_tell.Text.Trim()))
            {
                SearchString += "and CoustomerTell Like N'%" + txt_tell.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_Adres.Text.Trim()))
            {
                SearchString += "and CoustoemrAddres Like N'%" + txt_Adres.Text.Trim() + "%'";
            }
            if (rdb_active.IsChecked == true)
            {
                SearchString += "and CoustomerActive=1";
            }
            else if (rdb_dactive.IsChecked == true) {
                SearchString += "and CoustomerActive=2";
            }



            
            return SearchString;
        }

        private void imagesearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowCustomerINfo(SearchStatement);
        }

    

        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Win_AddCoustomer WN = new Win_AddCoustomer();
            WN.Window_State = 1;
            WN.Show();
            ShowCustomerINfo(SearchStatement);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_Coustomer.SelectedItem;
            Win_AddCoustomer WN = new Win_AddCoustomer();
            WN.Window_State = 2;
            if (item == null)
            {
                MessageBox.Show("لطفا مشتری را از لیست انتخاب کنید");
                return;
            }
                WN.CID = Convert.ToInt32((dataGrid_Coustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                WN.CName = (dataGrid_Coustomer.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                WN.CTell = (dataGrid_Coustomer.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                WN.CAddres = (dataGrid_Coustomer.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                WN.Show();

            ShowCustomerINfo(SearchStatement);

        }

        private void btn_activeordeactive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataGrid_Coustomer.SelectedItem;
                if (item == null)
                {
                    MessageBox.Show("لطفا مشتری را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int CoustomerID = Convert.ToInt32((dataGrid_Coustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Coustomer c = (from cu in database.Coustomers where cu.CoutomerID == CoustomerID select cu).SingleOrDefault();
                if (c.CoustomerActive == 1)
                {
                    c.CoustomerActive = 2;
                }
                else
                {
                    c.CoustomerActive = 1;
                }
                database.SaveChanges();
                MyMessageBox.ShowMessageSuccess("اطلاعات مشتری به روز شد");
                ShowCustomerINfo(SearchStatement);
            }
            catch
            {

                MessageBox.Show("مشکلی در غیر فعال کردن مشتری به وجود آمده است");
            }
        }
    }
}
