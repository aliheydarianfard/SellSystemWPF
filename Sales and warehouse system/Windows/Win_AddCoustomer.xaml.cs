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

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_AddCoustomerxaml.xaml
    /// </summary>
    public partial class Win_AddCoustomer : Window
    {

        public Win_AddCoustomer()
        {
            InitializeComponent();
        }
        public byte Window_State;
        public string CName;
        public int CID;
        public string CTell;
        public string CAddres;
        sellEntities database = new sellEntities();
        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if (!cheknullabalbel()) {
                return;
            }

            try
            {
                switch (Window_State)
                {
                    case 1:
                        {
                            Coustomer c = new Coustomer();
                            c.CoustomerName = txt_CustomerName.Text;
                            c.CoustomerTell = txttellcustomer.Text.Trim();
                            c.CoustoemrAddres = txt_Xoustomer.Text.Trim();
                            c.UserID = PublicVariable.GUserID;
                            c.SetDate = lbl_date.Content.ToString();
                            c.CoustomerActive = 1;
                            database.Coustomers.Add(c);
                            database.SaveChanges();
                            MyMessageBox.ShowMessageSuccess();
                            break;
                        }
                    case 2:

                        {
                            Coustomer cu = (from C in database.Coustomers where C.CoutomerID == this.CID select C).SingleOrDefault();
                         
                            cu.CoustomerName = txt_CustomerName.Text.Trim();
                            cu.CoustomerTell = txttellcustomer.Text.Trim();
                            cu.CoustoemrAddres = txt_Xoustomer.Text.Trim();
                            cu.UpdateDate = lbl_date.Content.ToString();
                            database.SaveChanges();
                            MyMessageBox.ShowMessageSuccess("اطلاعات مشتری ها به روز شد");
                            break;
                        }

                }
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

        private bool cheknullabalbel()
        {
            if (txt_CustomerName.Text.Trim() == "")
            {
                MessageBox.Show("لطفا نام مشتری را وارد کنید");
                return false;
            }
            if (txttellcustomer.Text.Trim() == "")
            {
                MessageBox.Show("لطفا شماره تماس مشتری را وارد کنید");
                return false;
            }
           
            return true;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLable();
            switch (Window_State)
            {
                case 2:
                    ShowInformationForEdit();
                    break;
            }
        }

        private void ShowInformationForEdit()
        {

            txt_CustomerName.Text = this.CName;
            txttellcustomer.Text = this.CTell;
            txt_Xoustomer.Text = this.CAddres;
            ShowLable();
        }

        private void ShowLable()
        {
            lbl_date.Content = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(persiandatetimepicker.Text));
        }

        private void txttellcustomer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regix = new Regex("[^0-9]+");
            e.Handled = regix.IsMatch(e.Text);
        }
    }
}
