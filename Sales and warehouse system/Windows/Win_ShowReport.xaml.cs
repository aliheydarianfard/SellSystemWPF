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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_ShowReport.xaml
    /// </summary>
    public partial class Win_ShowReport : Window
    {
        public string ReportName { get; set; }
        public string ShowFormoula { get; set; }
        public string [] Params = new string[10];
        public Win_ShowReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            string patch = System.AppDomain.CurrentDomain.BaseDirectory + "Report\\" + this.ReportName;
            rd.Load(patch);
            rd.RecordSelectionFormula = this.ShowFormoula;
            switch (ReportName) {

                case "CustomerList.rpt":
                    {
                        rd.Refresh();
                        rd.SetParameterValue("SetDate", String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)));
                        break;
                    }
                case "UserList.rpt":
                    {
                        rd.Refresh();
                        rd.SetParameterValue("SetDate", String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)));
                        break;
                    }
                case "SellList.rpt":
                    {
                        rd.Refresh();
                        rd.SetParameterValue("DateNow", String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)));
                        rd.SetParameterValue("az",Params[0]);
                        rd.SetParameterValue("ta",Params[1]);
                      
                        break;
                    }
                case "Sell_Coustomer.rpt":
                    {
                        rd.Refresh();
                        rd.SetParameterValue("DateNow", String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)));
                        rd.SetParameterValue("az", Params[0]);
                        rd.SetParameterValue("ta", Params[1]);
                     
                        break;
                    }
                case "Factor.rpt":
                    {
                        rd.Refresh();
                        rd.SetParameterValue("cname", Params[1]);
                        rd.SetParameterValue("Ctell", Params[2]);
                        rd.SetParameterValue("invoiceid", Params[0]);
                        rd.SetParameterValue("price", Params[6]);
                        rd.SetParameterValue("caddres", Params[3]);
                        rd.SetParameterValue("FactorDate", Params[5]);
                        rd.SetParameterValue("caddres", Params[3]);
                        rd.SetParameterValue("print dare", String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)));
                        rd.SetParameterValue("FactorTypr", Params[4]);
                      

                        break;
                    }

            }
            CRV.ViewerCore.ReportSource = rd;
         
        }
    }
}
