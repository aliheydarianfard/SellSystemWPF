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
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;

namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Win_AddCoustomerxaml.xaml
    /// </summary>
    public partial class Win_AddUser : Window
    {
        
        public Win_AddUser()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        public byte Window_State { get; set;}
        public string UserName { get; set;}
        public string UserFamily { get; set; }
        public string UserTell { get; set; }
        public byte UserAge { get; set; }
        public int UserID { get; set; }
        public string UserGender { get; set;}
        public string UserUserName { get; set; }

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
                            SHA256CryptoServiceProvider sh2 = new SHA256CryptoServiceProvider();
                    Byte[] b1;
                    Byte[] b2;
                    b1 = UTF8Encoding.UTF8.GetBytes(txtpasword.Password.Trim());
                    b2 = sh2.ComputeHash(b1);
                    string UserPaswordHashed = BitConverter.ToString(b2);
                    var query = from ut in database.Users
                                where ut.UserUserName == txtuserusername.Text.Trim()
                                select ut;
                    var Result = query.ToList();
                    if (Result.Count > 0)
                    {
                        MessageBox.Show("این نام کاربری قبلا ثبت شده است");
                        txtuserusername.Focus();
                        return;
                    }
                    User u = new User();
                    u.UserName = txt_username.Text.Trim();
                    u.UserFamily = txt_userfamily.Text.Trim();
                    u.UserAge = Convert.ToByte(txtage.Text.Trim());
                    u.UserActive = 1;
                    u.UserStartTime = String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calender1.Text));
                    u.UserTell = txttell.Text.Trim();
                    u.UserUserName = txtuserusername.Text.Trim();
                    u.UserPassword = UserPaswordHashed;
                    if (rdbmen.IsChecked == true)
                    {
                        u.UserGender = 1;
                    }
                    else
                        u.UserGender = 2;
                    database.Users.Add(u);
                    database.SaveChanges();
                    MyMessageBox.ShowMessageSuccess();
                    break;
                }
                    case 2:
                        {
                            User u = (from us in database.Users where us.UserID == UserID select us).SingleOrDefault();

                            u.UserName = txt_username.Text.Trim();
                            u.UserFamily = txt_userfamily.Text.Trim();
                            u.UserAge = Convert.ToByte(txtage.Text.Trim());
                            u.UserTell = txttell.Text.Trim();
                            if (rdbmen.IsChecked==true)
                            {
                                u.UserGender = 1;
                            }
                            else if(rdbwomen.IsChecked==true) {

                                u.UserGender = 2;
                            }
                            database.SaveChanges();
                            MyMessageBox.ShowMessageSuccess("اطلاعات کاربرها به روز شد");
                            break;
                        }
                
            }
                
            }
            catch
            {
                MessageBox.Show("خطا");
            }
            finally {
                this.Close();
            }
        }

        private bool cheknullabalbel()
        {

            if (txt_username.Text.Trim() == "")
            {
                MessageBox.Show("لطفا نام کاربر را وارد کنید");
                return false;
            }
            if (txttell.Text.Trim() == "")
            {
                MessageBox.Show("لطفا شماره تماس کاربر را وارد کنید");
                return false;
            }
            if (txtage.Text.Trim() == "")
            {
                MessageBox.Show("لطفا سن کاربر را وارد کنید");
                return false;
            }
            if (txt_userfamily.Text.Trim() == "")
            {
                MessageBox.Show("لطفا نام خانوادگی کاربر را وارد کنید");
                return false;
            }
            if (txtuserusername.Text.Trim() == "")
            {
                MessageBox.Show("لطفا  نام کاربری کاربر را وارد کنید");
                return false;
            }
            if (txtpasword.Password.Trim() == "")
            {
                MessageBox.Show("لطفا  رمز عبور کاربر را وارد کنید");
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
            txt_username.Focus();
            switch (Window_State) { 
            
            case 2:
                {
                    txt_username.Text = UserName;
                    txt_userfamily.Text = UserFamily;
                    txttell.Text = UserTell;
                    txtage.Text = UserAge.ToString();
                    txtuserusername.Text = UserUserName;
                    txtuserusername.IsEnabled = false;
                    txtpasword.Password = "******";
                    txtpasword.IsEnabled = false;
                    if (UserGender == "خانم")
                    {
                        rdbwomen.IsChecked = true;
                    }
                    else
                    {
                        rdbmen.IsChecked = true;
                    }
                    break;
                }
            }
        }

        private void txttell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }

        private void Rectangle_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_user.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;
        
        }

       
        }
    }

