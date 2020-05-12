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
using Sales_and_warehouse_system.Windows;
using Microsoft.Win32;
using Sales_and_warehouse_system.Modul;
using ModelLayer;
using System.Security.Cryptography;
using System.Net;

namespace Sales_and_warehouse_system
{
    /// <summary>
    /// Interaction logic for Win_Login.xaml
    /// </summary>
    public partial class Win_Login : Window
    {
        public Win_Login()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }



        private void btn_enter_Click_1(object sender, RoutedEventArgs e)
        {
            SHA256CryptoServiceProvider sh2 = new SHA256CryptoServiceProvider();
            Byte[] b1;
            Byte[] b2;
            b1 = UTF8Encoding.UTF8.GetBytes(txt_password.Password.Trim());
            b2 = sh2.ComputeHash(b1);
            string UserPaswordHashed = BitConverter.ToString(b2);
            var qurey = from u in database.Users
                        where u.UserUserName == txt_UserName.Text.Trim()
                        where u.UserPassword == UserPaswordHashed
                        where u.UserActive == 1
                        select u;
            var users = qurey.ToList();
            if (users.Count == 1)
            {
                RegistryKey UserNameKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\sell");
                try
                {
                    UserNameKey.SetValue("UserNameRegister", txt_UserName.Text.Trim());
                    PublicVariable.GUserName = users[0].UserName;
                    PublicVariable.GUserFamily = users[0].UserFamily;
                    PublicVariable.GUserID = users[0].UserID;
                    //
                    string ComputerName = System.Environment.MachineName;
                    string IP = "";
                    IPHostEntry iph = Dns.GetHostByName(ComputerName);
                    IPAddress[] ipa = iph.AddressList;
                    IP = ipa[0].ToString();
                    //
                    UserLog us = new UserLog();
                    us.ComputerName = ComputerName;
                    us.IPAddres = IP;
                    us.EnterDateTime = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(CalnderUserLog.Text)) + "-" + String.Format("{0:HH:mm:ss}",DateTime.Now);
                    us.UserID = PublicVariable.GUserID;
                    database.UserLogs.Add(us);
                    database.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("خطایی به وجود آمده است");
                }
                finally
                {
                    UserNameKey.Close();
                }


                this.Close();
            }
            else {
                MessageBox.Show("با این نام کاربری و رمز عبور کاربر فعالی یافت نشد","خطا",MessageBoxButton.OK,MessageBoxImage.Error);
                txt_password.Password = "";
                txt_password.Focus();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            RegisterName();
            timelbl();
            txt_password.Focus();

        }

        private void timelbl()
        {
            lbl_day.Content = MyCalender.SelectedDate.PersianDayOfWeek;
            lbl_dayofmounth.Content = MyCalender.SelectedDate.Day;
            lbl_mounth.Content = MyCalender.SelectedDate.MonthAsPersianMonth;
            lbl_year.Content = MyCalender.SelectedDate.Year;
        }

        private void RegisterName()
        {
            RegistryKey UserNameKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\sell");
            txt_UserName.Text = (string)UserNameKey.GetValue("UserNameRegister");
        }
    }
}
