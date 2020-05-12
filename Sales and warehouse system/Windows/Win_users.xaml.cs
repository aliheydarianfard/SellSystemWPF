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
    /// Interaction logic for Win_users.xaml
    /// </summary>
    public partial class Win_users : Window
    {
        public Win_users()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShowUserinfo(SearchStatement);
        }

        private void ShowUserinfo(Func<string> SearchforUser)
        {
            var query = database.Database.SqlQuery<VW_Users>("select * from VW_Users Where 1=1" + SearchforUser());
            var U = query.ToList();
            dataGrid_User.ItemsSource = U.ToList();
        }
        private string SearchStatement() {
            string SearchString = "and UserStartTime Between'" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calender_az.Text)) + "' And '" + String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder_ta.Text)) + "'";
            if (!string.IsNullOrEmpty(txt_name.Text.Trim())) {
                SearchString += "and UserName Like N'%" + txt_name.Text.Trim() + "%'";
            }
            if (!string.IsNullOrEmpty(txt_family.Text.Trim()))
            {
                SearchString += "and UserFamily Like N'%" + txt_family.Text.Trim() + "%'";
            }
            if (rd_deactive.IsChecked == true) {
                SearchString += "and UserActive=2";
            }
            else
            {
                SearchString += "and UserActive=1";
            }
        
            
            return SearchString;
        }
        private void image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imagesearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowUserinfo(SearchStatement);
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Win_AddUser wn = new Win_AddUser();
            wn.ShowDialog();
            ShowUserinfo(SearchStatement);
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_User.SelectedItem;
            Win_AddUser wn = new Win_AddUser();
            if (item == null)
            {
                MessageBox.Show("لطفا کاربر مورد نظر را انتخاب کنید");
                return;
            }
                wn.Window_State = 2;
                wn.UserID = Convert.ToInt32((dataGrid_User.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                wn.UserName = (dataGrid_User.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                wn.UserFamily = (dataGrid_User.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                wn.UserTell = (dataGrid_User.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                wn.UserAge = Convert.ToByte((dataGrid_User.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                wn.UserGender = (dataGrid_User.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                wn.UserUserName = (dataGrid_User.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text;
                wn.ShowDialog();
                ShowUserinfo(SearchStatement);
            
        }

        private void Btn_Deactive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = dataGrid_User.SelectedItem;
                if (item == null) {
                    MessageBox.Show("لطفا کاربری را انتخاب کنید","راهنمایی",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }
                int UserID = Convert.ToInt32((dataGrid_User.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                User u = (from us in database.Users where us.UserID == UserID select us).SingleOrDefault();
                if (u.UserActive == 1)
                {
                    u.UserActive = 2;
                }
                else {
                    u.UserActive = 1;
                }
                database.SaveChanges();
                MyMessageBox.ShowMessageSuccess("اطلاعات کاربر به روز شد");
                ShowUserinfo(SearchStatement);
            }
            catch {

                MessageBox.Show("مشکلی در غیر فعال کردن کاربر به وجود آمده است");
            }
        }

        private void Btn_showaccsesuser(object sender, RoutedEventArgs e)
        {
            object item = dataGrid_User.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("لطفا کاربری را انتخاب کنید", "راهنمایی", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Win_UserAcsses wn = new Win_UserAcsses();
            wn.lbl_UserName.Text= (dataGrid_User.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text+" "+ (dataGrid_User.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            wn.GetUserID= Convert.ToInt32((dataGrid_User.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            wn.ShowDialog();

        }
    }
}
