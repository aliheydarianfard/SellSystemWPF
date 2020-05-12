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
using Sales_and_warehouse_system.Modul;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_AddCoustomerxaml.xaml
    /// </summary>
    public partial class Win_AddNewPrice : Window
    {
        
        public Win_AddNewPrice()
        {
            InitializeComponent();
        }
        public string ProductName;
        public int ProductID;
        sellEntities database = new sellEntities();
     

        private bool cheknullabalbel()
        {
         if (txtbuy.Text.Trim() == "") {
             MessageBox.Show("لطفا قیمت خرید را وارد کنید");
             return false;
         }
         if (txtsell.Text.Trim() == "")
         {
             MessageBox.Show("لطفا قیمت فروش را وارد کنید");
             return false;
         }
           
            return true;
        }


        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_date.Content =String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder.Text));
            lblnameprudct.Content = this.ProductName;
            lbl_username.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;
            txtbuy.Focus();

        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regix = new Regex("[^0-9]+");
            e.Handled = regix.IsMatch(e.Text);
        }

        private void textBox_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regix = new Regex("[^0-9]+");
            e.Handled = regix.IsMatch(e.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!cheknullabalbel())
            {
                return;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ProductPrice pc = new ProductPrice();
                    pc.ProductPricePurch = Convert.ToInt64(txtbuy.Text.Trim());
                    pc.ProductPriceSell = Convert.ToInt64(txtsell.Text.Trim());
                    pc.ProductPriceDate = lbl_date.Content.ToString();
                    pc.ProductPricedesc = textBox_Copy1.Text;
                    pc.ProductID = this.ProductID;
                    pc.UserID = PublicVariable.GUserID;
                    database.ProductPrices.Add(pc);
                    database.SaveChanges();
                    MyMessageBox.ShowMessageSuccess();


                    database.sp_lastfe_Product(this.ProductID, Convert.ToInt64(txtsell.Text.Trim()), Convert.ToInt64(txtbuy.Text.Trim()));
                    database.SaveChanges();
                    ts.Complete();

                }
                catch
                {
                    MessageBox.Show("در ارتباط با دیتا بیس مشکلی به وجود آمده است لطفا دئباره تلاش کنید");
                }
                finally
                {
                    this.Close();
                }
            }
            }
    }
}
