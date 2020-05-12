using Microsoft.Win32;
using ModelLayer;
using Sales_and_warehouse_system.Modul;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;
namespace Sales_and_warehouse_system.Windows
{
    /// <summary>
    /// Interaction logic for Wiin_Addproduct.xaml
    /// </summary>
    public partial class Wiin_Addproduct : Window
    {
        public Wiin_Addproduct()
        {
            InitializeComponent();
        }
        public string StrName;
        public string imagename=null;
        
        public byte window_starte { get; set;}
        public string ProductName { get; set;}
        public string ProudactDesc { get; set; }
        public string ProudactDate { get; set; }
        public int ProudcatID { get; set; }

        public 
        sellEntities database = new sellEntities();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_username.Content = PublicVariable.GUserName + " " + PublicVariable.GUserFamily;

            switch (window_starte) {
                case 2: {
                        txt_ProudactName.Text = ProductName;
                        txt_tozihat.Text = ProudactDesc;
                        ShowImage();

                    }
                    break;
                
            }
        }

        private void ShowImage()
        {
            var query = from p in database.Products where p.ProductID == ProudcatID select p;
            var Result = query.ToList();
            if (Result[0].ProductImage != null) {
                byte[] ImageArraye = (byte[]) Result[0].ProductImage;
                MemoryStream strim = new MemoryStream();
                strim.Write(ImageArraye, 0, ImageArraye.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(strim);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                imageproduct.Source = bi;




            }
        }

        private void image1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (!cheknullabalbel())
            {
                return;
            }
            try
            {
                switch (window_starte)
                {


                    case 1:
                        {
                            if (imagename != null)
                            {
                                FileStream fs = new FileStream(imagename, FileMode.Open, FileAccess.Read);
                                Byte[] imgByteArray = new byte[fs.Length];
                                fs.Read(imgByteArray, 0, Convert.ToInt32(fs.Length));
                                fs.Close();

                                database.sp_ins_product(txt_ProudactName.Text.Trim(), txt_tozihat.Text.Trim(), imgByteArray, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calender1.Text)), PublicVariable.GUserID);
                                database.SaveChanges();
                                MessageBox.Show("کالا با موفقیت ثیت شد");
                            }
                            else {
                                database.sp_ins_product_whitoutimage(txt_ProudactName.Text.Trim(), txt_tozihat.Text.Trim(), String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(calender1.Text)), PublicVariable.GUserID);
                                database.SaveChanges();
                                MessageBox.Show("کالا با موفقیت ثیت شد");
                            }
                            break;
                        }
                    case 2: {
                            if (imagename != null)
                            {
                                FileStream fs = new FileStream(imagename, FileMode.Open, FileAccess.Read);
                                Byte[] imgByteArray = new byte[fs.Length];
                                fs.Read(imgByteArray, 0, Convert.ToInt32(fs.Length));
                                fs.Close();

                                database.sp_Update_Product(ProudcatID, txt_ProudactName.Text.Trim(), txt_tozihat.Text.Trim(), imgByteArray);
                                database.SaveChanges();
                                MessageBox.Show("اطلاعات کالا به روز شد");
                            }
                            else {
                                database.sp_Update_Product_whitoutimage(ProudcatID, txt_ProudactName.Text.Trim(), txt_tozihat.Text.Trim());
                                database.SaveChanges();
                                MessageBox.Show("اطلاعات کالا به روز شد");
                            }
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
            if (txt_ProudactName.Text.Trim() == "")
            {
                MessageBox.Show("لطفا نام محصول را وارد کنید");
                return false;
            }
         

            return true;
        }

        private void imageproduct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FileDialog flg = new OpenFileDialog();
            flg.Filter = "Image File(*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            flg.ShowDialog();
                {
                StrName = flg.SafeFileName;
                imagename = flg.FileName;
              
                    ImageSourceConverter isc = new ImageSourceConverter();
                    imageproduct.SetValue(System.Windows.Controls.Image.SourceProperty, isc.ConvertFromString(imagename));
               
            }
            flg = null;
        }

        private void btn_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
