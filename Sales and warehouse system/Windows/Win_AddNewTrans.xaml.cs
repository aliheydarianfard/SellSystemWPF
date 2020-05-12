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
    public partial class Win_AddNewTrans : Window
    {

        public Win_AddNewTrans()
        {
            InitializeComponent();
        }
        public string ProductName;
        public int ProductID;
        sellEntities database = new sellEntities();


        private bool cheknullabalbel()
        {
            if (txtcount.Text.Trim() == "")
            {
                MessageBox.Show("لطفا تعداد تراکنش را وارد کنید");
                return false;
            }
            if (cmbstuts.SelectedIndex == -1)
            {
                MessageBox.Show("لطفا نوع تراکنش را وارد کنید");
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

            lblnameprudct.Content = this.ProductName;
            lbl_username.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;
            txtcount.Focus();
            cmbstuts.Items.Add("افزایش موجودی ");
            cmbstuts.Items.Add("کاهش موجودی ");
            cmbstuts.SelectedIndex = 0;
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

                    Inventory inv = new Inventory();
                    inv.InventoryDesc = txtdesc.Text.Trim();
                    inv.InventoryDate = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder.Text));
                    inv.ProductID = this.ProductID;
                    inv.UserID = PublicVariable.GUserID;
                    if (cmbstuts.SelectedIndex == 0)
                    {
                        inv.InventoryCount = Convert.ToInt32(txtcount.Text.Trim());
                    }
                    else
                       if (cmbstuts.SelectedIndex == 1)
                    {
                        inv.InventoryCount = -Convert.ToInt32(txtcount.Text.Trim());
                    }

                    database.Inventories.Add(inv);
                    database.SaveChanges();
                    MyMessageBox.ShowMessageSuccess();
               

                    //
                    database.sp_Update_LastSuplay(inv.InventoryCount, this.ProductID);
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

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void Cmbstuts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
    }
