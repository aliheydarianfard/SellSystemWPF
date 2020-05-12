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
using System.Transactions;
using System.Text.RegularExpressions;

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_Invoice.xaml
    /// </summary>
    public partial class Win_Invoice : Window
    {
        public Win_Invoice()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        public int InvoceID { get; set; }
        public byte Win_state { get; set; }
        public string CoustomerName { get; set;}
        public string Date { get; set; }
        public long InvoicePrice_kharid { get; set; }
        public long InvociePrice_frosoh { get; set; }
       
        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    
              
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Win_state == 1)
            {
                ShowInfoCustoemr(SearchStatement_Coustomer);
                ShowInformationInvoce();
                ShowInfoProudact(SearchforProduct);
                enbalebutton();
            }

            else if(Win_state == 2) {
                ShowInformationforedit();
            }
        }

        private void ShowInformationforedit()
        {
            btn_creatinvoice.IsEnabled = false;
            lbl_date.Content = this.Date;
            lbl_coustomername.Content = this.CoustomerName;
            lbl_invoicenumber.Content = this.InvoceID;
            lbl_invoiceprice.Content = this.InvociePrice_frosoh;
            lbl_invoicepricepurche.Content = this.InvoicePrice_kharid;
            //
            ShowInfoProudact(SearchforProduct);
            //
            ShowInfiInvoiceID();
            //
            btn_finishinvoice.Content = "به روز رسانی فاکتور";
        }

        private void enbalebutton()
        {
            btn_submitproduact.IsEnabled = false;
            btnselectproduct.IsEnabled = false;
        }

        private void ShowInfoProudact(Func<string> searchinProduct)
        {
            var query = database.Database.SqlQuery<VW_Product>("select * from VW_Product Where 1=1 and ProudactActive=1 and ProductLastSuply>0" + SearchforProduct());
            var U = query.ToList();
            dataGrid_Product.ItemsSource = U.ToList();
        }

        private void ShowInformationInvoce()
        {
            lbl_username.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;
            lbl_date.Content = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calndercoustomer.Text));
        }

        private void ShowInfoCustoemr(Func<string> SearchinCoustomer)
        {
            var query = database.Database.SqlQuery<VW_Coustomer>("select * from VW_Coustomer Where 1=1 and CoustomerActive= 1" + SearchinCoustomer());
            var U = query.ToList();
            dataGrid_Coustomer.ItemsSource = U.ToList();
        }
        private string SearchStatement_Coustomer()
        {
            string SearchString = "";

            if (!string.IsNullOrEmpty(txt_Cname.Text.Trim()))
            {
                SearchString += "and CoustomerName Like N'%" + txt_Cname.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_ctell.Text.Trim()))
            {
                SearchString += "and CoustomerTell Like N'%" + txt_ctell.Text.Trim() + "%'";
            }

            return SearchString;
        }

        private string SearchforProduct()
        {
            string SearchString = "";
            if (!string.IsNullOrEmpty(txt_Pname.Text.Trim()))
            {
                SearchString += "and ProductName Like N'%" + txt_Pname.Text.Trim() + "%'";
            }

            return SearchString;
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowInfoCustoemr(SearchStatement_Coustomer);
        }

        private void btn_creatinvoice_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    object item = dataGrid_Coustomer.SelectedItem;
                    if (item == null)
                    {
                        MessageBox.Show("لطفا برای ایجاد فاکتور ابتدا مشتری را از لیست انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Invoice inv = new Invoice();
                    inv.CoustoemrId = Convert.ToInt32((dataGrid_Coustomer.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    inv.InvoicePrice = 0;
                    inv.InvocePrice_Pourche = 0;
                    inv.InvoiceDate = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calndercoustomer.Text));
                    inv.UserID = PublicVariable.GUserID;
                    database.Invoices.Add(inv);
                    database.SaveChanges();
                    lbl_coustomername.Content = (dataGrid_Coustomer.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    var query = database.Database.SqlQuery<Invoice>("select top 1 * from Invoice order By InvoiceID desc");
                    var Result = query.ToList();
                    this.InvoceID = Result[0].InvoiceId;
                    lbl_invoicenumber.Content = this.InvoceID;
                    btn_creatinvoice.IsEnabled = false;
                    btnselectproduct.IsEnabled = true;
                    ts.Complete();

                }

                catch
                {
                    MessageBox.Show("در ثبت فاکتور مشکلی به وجود آمده است لطفا دوباره تلاش کنید");
                }

            }
        }

        private void searchforproudact_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowInfoProudact(SearchforProduct);
        }

        private void txt_ctell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }



        private void btnselectproduct_Click(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_Product.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("لطفا کالایی را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //Wiin_Addproduct w = new Wiin_Addproduct();
            lbl_ProductName.Content = (dataGrid_Product.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            ProudactLastSuplay.Content = (dataGrid_Product.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
            ProudactLastFee.Content = (dataGrid_Product.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            lbl_gheymatkharid.Content = Convert.ToInt64((dataGrid_Product.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
            lbl_productid.Content = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            btn_submitproduact.IsEnabled = true;

        }

        private void btn_submitproduact_Click(object sender, RoutedEventArgs e)
        {
            if (txt_xount_product.Text == "") {
                MessageBox.Show("لطفا تعداد را وارد نمایید", "خطا", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            object items = dataGrid_Product.SelectedItem;
            int Countorder = Convert.ToInt32(txt_xount_product.Text.Trim());
            int CountSpulay = Convert.ToInt32((dataGrid_Product.SelectedCells[4].Column.GetCellContent(items) as TextBlock).Text);
            if (Countorder > CountSpulay) {
                MessageBox.Show("تعداد سفارش از تعداد انبار بیشتر است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var query = from ii_search in database.InvoceItems
                                where ii_search.InvoiceID == this.InvoceID
                                select ii_search;
                    var result = query.ToList();
                    for (int i = 0; i < result.Count; i++)
                    {

                        if (result[i].ProudactID == Convert.ToInt32(lbl_productid.Content))
                        {
                            MessageBox.Show("این کالا از قبل در فاکتور موجود میباشد برای ویراش ابتدا آنرا حذف نمایید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    object item = dataGrid_Product.SelectedItem;
                    InvoceItem ii = new InvoceItem();
                    ii.InvoceItemCount = Convert.ToInt32(txt_xount_product.Text.Trim());
                    ii.InvoceItemFeeSell = Convert.ToInt64((dataGrid_Product.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
                    ii.InvoceItemFeePurche = Convert.ToInt64((dataGrid_Product.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
                    ii.InvoiceID = this.InvoceID;
                    ii.ProudactID = Convert.ToInt32((dataGrid_Product.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    database.InvoceItems.Add(ii);
                    database.SaveChanges();
                    database.sp_Update_LastSuplay(-Convert.ToInt32(txt_xount_product.Text.Trim()), ii.ProudactID);
                    ShowInfiInvoiceID();
                    ShowInfoProudact(SearchforProduct);
                    lbl_invoiceprice.Content = Convert.ToInt64(lbl_invoiceprice.Content) + (Convert.ToInt32(txt_xount_product.Text.Trim()) * Convert.ToInt64(ProudactLastFee.Content));
                    lbl_invoicepricepurche.Content = Convert.ToInt64(lbl_invoicepricepurche.Content) + (Convert.ToInt32(txt_xount_product.Text.Trim()) * Convert.ToInt64(lbl_gheymatkharid.Content));
                    ts.Complete();
                }
            }
            catch
            {
                MessageBox.Show("در ثبت اطلاعات مشکلی پیش آمده است");
            }
            finally {


                lbl_ProductName.Content = "";
                ProudactLastFee.Content = "";
                ProudactLastSuplay.Content = "";
                txt_xount_product.Text = "";
                btn_submitproduact.IsEnabled = false;
            }
        }

        private void ShowInfiInvoiceID()
        {
            var query = database.Database.SqlQuery<VW_InvoiceItem>("select * from VW_InvoiceItem Where InvoiceID=" + this.InvoceID);
            var U = query.ToList();
            datagrid_Invoiceitem.ItemsSource = U.ToList();
        }

        private void txt_xount_product_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }

        private void btn_REmove_Click(object sender, RoutedEventArgs e)
        {
            object itemes = datagrid_Invoiceitem.SelectedItem;
            if (itemes == null) {
                MyMessageBox.ShowMessageSelection();
                return;
            }
            int GetPurcheCount = Convert.ToInt32((datagrid_Invoiceitem.SelectedCells[4].Column.GetCellContent(itemes) as TextBlock).Text);
            int getpurcehid = Convert.ToInt32((datagrid_Invoiceitem.SelectedCells[0].Column.GetCellContent(itemes) as TextBlock).Text);
            long getpourcheprice = Convert.ToInt64((datagrid_Invoiceitem.SelectedCells[2].Column.GetCellContent(itemes) as TextBlock).Text);
            long gheymatkharid = Convert.ToInt64((datagrid_Invoiceitem.SelectedCells[3].Column.GetCellContent(itemes) as TextBlock).Text);

            if (MessageBox.Show("آیا عملیات حذف انجام شود", "هشدار", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {

                    var deletquery = (from II in database.InvoceItems
                                      where II.InvoiceID == this.InvoceID
                                      where II.ProudactID == getpurcehid
                                      select II).SingleOrDefault();
                    database.InvoceItems.Remove(deletquery);
                    database.SaveChanges();
                    ShowInformationInvoce();
                    //
                    database.sp_Update_LastSuplay(GetPurcheCount, getpurcehid);
                    //
                    ShowInfoProudact(SearchforProduct);
                    //
                    lbl_invoiceprice.Content = Convert.ToInt64(lbl_invoiceprice.Content) - (getpourcheprice * GetPurcheCount);
                    lbl_invoicepricepurche.Content = Convert.ToInt64(lbl_invoicepricepurche.Content) - (gheymatkharid * GetPurcheCount);
                    //
                    ShowInfiInvoiceID();
                }
                catch {
                    MessageBox.Show("در حذف اطلاعات مشکلی پیش آمده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btn_finishinvoice_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (MessageBox.Show("آیا بستن فاکتور انجام شود", "هشدار", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {


                        Invoice invoice = (from i in database.Invoices where i.InvoiceId == this.InvoceID select i).SingleOrDefault();
                        invoice.InvoicePrice = Convert.ToInt64(lbl_invoiceprice.Content);
                        invoice.InvocePrice_Pourche = Convert.ToInt64(lbl_invoicepricepurche.Content);
                        invoice.InvoiceType = 1;
                        database.SaveChanges();
                        if (Win_state == 1)
                        {
                            MessageBox.Show("فاکتور با موفقیت بسته شد", "پیغام", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearForm();
                            ts.Complete();
                        }
                        else if (Win_state == 2)
                        {
                            MessageBox.Show("فاکتور با موفقیت به روز شد", "پیغام", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            ts.Complete();
                        }

                    }
                    catch
                    {

                        MessageBox.Show("در بستن فاکتور مشکلی پیش آمده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            
        }

              

        private void ClearForm()
        {
            txt_Cname.Text = "";
            txt_ctell.Text = "";
            btn_creatinvoice.IsEnabled = true;
            lbl_username.Content = "";
            lbl_coustomername.Content = "";
            lbl_invoicenumber.Content = "0";
            lbl_invoiceprice.Content = "0";
            lbl_invoicepricepurche.Content = "0";
            txt_Pname.Text = "";
            btnselectproduct.IsEnabled = false;
            btn_submitproduact.IsEnabled = false;
            lbl_ProductName.Content = "....";
            ProudactLastFee.Content = "....";
            ProudactLastSuplay.Content = "....";
            txt_xount_product.Text = "";
            lbl_gheymatkharid.Content = "0";
            lbl_productid.Content = "";
            this.InvoceID = 0;
            datagrid_Invoiceitem.ItemsSource = null;
            datagrid_Invoiceitem.Items.Clear();
            ShowInfoCustoemr(SearchStatement_Coustomer);
            ShowInfoProudact(SearchforProduct);

        }
    
    }

}
