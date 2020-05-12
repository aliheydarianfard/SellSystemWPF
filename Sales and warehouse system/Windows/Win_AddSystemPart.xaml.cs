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
    /// Interaction logic for Win_AddSystemPart.xaml
    /// </summary>
    public partial class Win_AddSystemPart : Window
    {
     
        public Win_AddSystemPart()
        {
            InitializeComponent();
        }
        sellEntities database = new sellEntities();
        public int Get_SystePartID { get; set;}
        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_Name.Text == "") {
                    MessageBox.Show("لطفا نام بخش را کامل کنید");
                    return;
                    }
                SystemPart sp = new SystemPart();
                sp.SystemPartName = txt_Name.Text.Trim();
                sp.SystemPartDetaile = txt_Desc.Text.Trim();
                sp.SystemPartLevel =Get_SystePartID;
                database.SystemParts.Add(sp);
                database.SaveChanges();
                MessageBox.Show("بخش جدید با موفقیت اضافه شد","موفقیت",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("در ثبت اطلاعات مشکلی به وجود آمده است","خطا",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally {
                this.Close();
            }
        }
    }
}
