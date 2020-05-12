using Sales_and_warehouse_system.Modul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Win_Change_PassWord.xaml
    /// </summary>
    public partial class Win_Change_PassWord : Window
    {
        public Win_Change_PassWord()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void image_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (txt_new.Password == "" || txt_old.Password == "") {
                MessageBox.Show("رمز عبور جدید یا رمز عبور قدیمی خالی است");
                return;
                 
            }
            try
            {
                SHA256CryptoServiceProvider shaoldpass = new SHA256CryptoServiceProvider();
                Byte[] b1_oldpass;
                Byte[] b2_oldpass;
                b1_oldpass = UTF8Encoding.UTF8.GetBytes(txt_old.Password.Trim());
                b2_oldpass = shaoldpass.ComputeHash(b1_oldpass);
                string UserPaswordHashedold = BitConverter.ToString(b2_oldpass);
                var query = from User in database.Users
                            where User.UserID == PublicVariable.GUserID
                            where User.UserPassword == UserPaswordHashedold
                            select User;
                var result = query.ToList();
                if (result.Count == 0)
                {
                    MessageBox.Show("رمز قدیمی اشتباه وارد شده است");
                    return;
                }
                else if (result.Count == 1) {
               
                    SHA256CryptoServiceProvider shnewpass = new SHA256CryptoServiceProvider();
                    Byte[] b1_newpass;
                    Byte[] b2_newpass;
                    b1_newpass = UTF8Encoding.UTF8.GetBytes(txt_new.Password.Trim());
                    b2_newpass = shaoldpass.ComputeHash(b1_newpass);
                    string usernewpassword = BitConverter.ToString(b2_newpass);
                    var updatepasswordquery = (from u in database.Users where u.UserID == PublicVariable.GUserID select u).SingleOrDefault();
                    updatepasswordquery.UserPassword = usernewpassword;
                    database.SaveChanges();
                    MessageBox.Show("رمز عبور به روز رسانی شد");
                    System.Environment.Exit(0);
                }
            }
            catch
            {
                MessageBox.Show("خطایی رخ داده است");
            }
            finally {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_username.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;
        }
    }
}
