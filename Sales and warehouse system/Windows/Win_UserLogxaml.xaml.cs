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
    /// Interaction logic for Win_UserLogxaml.xaml
    /// </summary>
    public partial class Win_UserLogxaml : Window
    {
        public Win_UserLogxaml()
        {
            InitializeComponent();
        }
        sellEntities databse = new sellEntities();
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
            Bindcmb();
            ShowUserLogInfo(Searchinfo);
        }

        private void ShowUserLogInfo(Func<string>SearchForUserLog)
        {
            var query=databse.Database.SqlQuery < VW_UserLog >("select * from VW_UserLog where "+ SearchForUserLog());
            var result = query.ToList();
            dataGrid_UserLog.ItemsSource = result;
        }

        private void Bindcmb()
        {
            cmb_fullname.ItemsSource = databse.VW_Users.ToList();
            cmb_fullname.DisplayMemberPath = "FullName";
            cmb_fullname.SelectedValuePath = "UserID";
        }

        private void button_Copy_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowUserLogInfo(Searchinfo);
        }

        private string Searchinfo()
        {
            string time_az = txt_saat_az.Text.Trim() + ":" + txt_daghighe_az + ":" + txt_sanie_Az.Text.Trim();
            string time_ta = txt_saat_ta.Text.Trim() + ":" + txt_daghighe_ta + ":" + txt_sanie_taa.Text.Trim();
            string searchstring = "EnterDateTime Between '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calnder1.Text)) + "-" + time_az + "'and'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calnder2.Text)) + "-" + time_ta+"'";
            if (cmb_fullname.Text!="") {
                searchstring += "and UserID =" + cmb_fullname.SelectedValue;
            }
            if (txt_IP.Text != "") {
                searchstring += "and IPAddres Like '%" + txt_IP.Text.Trim() + "%'";
            }
            if (txt_nameCopmputer.Text != "")
            {
                searchstring += "and ComputerName Like '%" + txt_nameCopmputer.Text.Trim() + "%'";
            }
            return searchstring;
        }
    }
}
