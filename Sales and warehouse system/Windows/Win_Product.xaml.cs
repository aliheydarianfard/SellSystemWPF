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
    public partial class Win_Product : Window
    {
        public Win_Product()
        {
            InitializeComponent();
        }
       
        sellEntities database = new sellEntities();
    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowCustomerINfo(SearchforProduct);
            ShowStutus();

        }

        private void ShowStutus()
        {
            cmb_stutus.Items.Add("همه");
            cmb_stutus.Items.Add("موجود");
            cmb_stutus.Items.Add("نا موجود");
            cmb_stutus.SelectedIndex = 0;
        }

        private void ShowCustomerINfo(Func<string> SearchforUser)
        {
            var query = database.Database.SqlQuery<VW_Product>("select * from VW_Product Where 1=1" + SearchforProduct());
            var U = query.ToList();
            dataGrid_Product.ItemsSource = U.ToList();
        }
        private string SearchforProduct()
        {
            string SearchString = "and ProductStartTime Between'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calender_az.Text)) + "' And '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
            if (!string.IsNullOrEmpty(txt_name.Text.Trim()))
            {
                SearchString += "and ProductName Like N'%" + txt_name.Text.Trim() + "%'";
            }
            if (cmb_stutus.SelectedIndex == 1) {
                SearchString += "and ProductLastSuply >0 ";
            }
            else
                   if (cmb_stutus.SelectedIndex == 2)
            {
                SearchString += "and ProductLastSuply <=0 ";
            }
            if (rdb_active.IsChecked == true)
            {
                SearchString += "and ProudactActive=1";
            }
            else if (rdb_dactive.IsChecked == true)
            {
                SearchString += "and ProudactActive=2";
            }


            return SearchString;
        }

        private void imagesearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowCustomerINfo(SearchforProduct);
        }



        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void button_Click_ShowPrice(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_Product.SelectedItem;
            Win_ProductPrice W_Price = new Win_ProductPrice();
            W_Price.ProducteName= (dataGrid_Product.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            W_Price.ProductID = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            W_Price.ShowDialog();
            ShowCustomerINfo(SearchforProduct);

        }
        private void button_Click_ShowInventory(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_Product.SelectedItem;
            Win_Inventory W_Inventory = new Win_Inventory();
            W_Inventory.ProducteName = (dataGrid_Product.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            W_Inventory.ProductID = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            W_Inventory.ShowDialog();
            ShowCustomerINfo(SearchforProduct);

        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Wiin_Addproduct wn = new Wiin_Addproduct();
            wn.window_starte = 1;
            wn.ShowDialog();
            ShowCustomerINfo(SearchforProduct);

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_Product.SelectedItem;
            if (item == null) {
                MessageBox.Show("لطفا کالایی را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Wiin_Addproduct w = new Wiin_Addproduct();
            w.window_starte = 2;
            w.ProudcatID = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            w.ProductName = (dataGrid_Product.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            w.ProudactDesc = (dataGrid_Product.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            w.ShowDialog();

            ShowCustomerINfo(SearchforProduct);
        }

        private void Btn_activeordeactive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataGrid_Product.SelectedItem;
                if (item == null)
                {
                    MessageBox.Show("لطفا کالایی را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int ProudactID = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Product p = (from pd in database.Products where pd.ProductID == ProudactID select pd).SingleOrDefault();
                if (p.ProudactActive == 1)
                {
                    p.ProudactActive = 2;
                }
                else
                {
                    p.ProudactActive = 1;
                }
                database.SaveChanges();
                MyMessageBox.ShowMessageSuccess("اطلاعات کالا  به روز شد");
                ShowCustomerINfo(SearchforProduct);
            }
            catch
            {

                MessageBox.Show("مشکلی در غیر فعال کردن مشتری به وجود آمده است");
            }
        }

    }
    }

