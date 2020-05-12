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

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_InvoceShow.xaml
    /// </summary>
    public partial class Win_InvoceShow : Window
    {
        public Win_InvoceShow()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInvoiceInfo(SearchStatement);
        }
        private void ShowInvoiceInfo(Func<string> SearchforUser)
        {
            var query = database.Database.SqlQuery<VW_InvoiceShow>("select * from VW_InvoiceShow Where 1=1 and UserID = "+ PublicVariable.GUserID+SearchforUser());
            var U = query.ToList();
            datagrid_invoiceshow.ItemsSource = U.ToList();
        }
        private string SearchStatement()
        {
            string SearchString = "and InvoiceDate Between'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calender_az.Text)) + "' And '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
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
            if (rdb_sell.IsChecked == true)
            {
                SearchString += "and InvoiceType=1";
            }
            else if (rdb_reject.IsChecked == true)
            {
                SearchString += "and InvoiceType=2";
            }




            return SearchString;
        }

        private void imagesearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowInvoiceInfo(SearchStatement);
        }

        private void btn_CrateInvoce_Click(object sender, RoutedEventArgs e)
        {
            Win_Invoice wn = new Win_Invoice();
            wn.Win_state = 1;
            wn.ShowDialog();

            ShowInvoiceInfo(SearchStatement);

        }

        private void btn_RejectInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("آیا بستن فاکتور انجام شود", "هشدار", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = datagrid_invoiceshow.SelectedItem;
                    if (item == null)
                    {
                        MessageBox.Show("لطفا فاکتور را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    int InvoiceID;
                    InvoiceID = Convert.ToInt32((datagrid_invoiceshow.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    Invoice i = (from inv in database.Invoices where inv.InvoiceId == InvoiceID select inv).SingleOrDefault();
                    i.InvoiceType = 2;
                    database.SaveChanges();
                    MessageBox.Show("فاکتور با موفقیت برگشت زده شد","موفقیت",MessageBoxButton.OK,MessageBoxImage.Information);
                    ShowInvoiceInfo(SearchStatement);
                }
                catch
                {

                    MessageBox.Show("مشکلی در برگشت زدن فاکتور به وجود آمده است");
                }
            }
        }

        private void btn_EditCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            object item = datagrid_invoiceshow.SelectedItem;
            Win_Invoice WN = new Win_Invoice();
           
            if (item == null)
            {
                MessageBox.Show("لطفا فاکتوری را از لیست انتخاب کنید");
                return;
            }
            WN.Win_state = 2;
            WN.InvoceID = Convert.ToInt32((datagrid_invoiceshow.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            WN.CoustomerName = (datagrid_invoiceshow.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            WN.Date = (datagrid_invoiceshow.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
            WN.InvociePrice_frosoh =Convert.ToInt64((datagrid_invoiceshow.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
            WN.InvoicePrice_kharid = Convert.ToInt64((datagrid_invoiceshow.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text);
            WN.ShowDialog();
            ShowInvoiceInfo(SearchStatement);
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            object item = datagrid_invoiceshow.SelectedItem;

            if (item == null)
            {
                MessageBox.Show("لطفا فاکتوری را از لیست انتخاب کنید");
                return;
            }

            Win_ShowReport wn = new Win_ShowReport();
            int getinvoiceid= Convert.ToInt32((datagrid_invoiceshow.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            // id
            wn.Params[0]= Convert.ToString((datagrid_invoiceshow.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            // name
            wn.Params[1] = Convert.ToString((datagrid_invoiceshow.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);
            //telephone
            wn.Params[2] = Convert.ToString((datagrid_invoiceshow.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
            //addres
            wn.Params[3]= Convert.ToString((datagrid_invoiceshow.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
            //type
            wn.Params[4] = Convert.ToString((datagrid_invoiceshow.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
            //date
            wn.Params[5] = Convert.ToString((datagrid_invoiceshow.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text);
            //price
            wn.Params[6] = Convert.ToString((datagrid_invoiceshow.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
            wn.ShowFormoula = "{ VW_InvoiceItem.InvoiceID} =" + getinvoiceid;
            wn.ReportName="Factor.rpt";
            wn.ShowDialog();
        }
    }
}
