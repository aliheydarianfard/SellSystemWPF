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
using Sales_and_warehouse_system.Modul;
using ModelLayer;

namespace Sales_and_warehouse_system.Windows
{
	/// <summary>
	/// Interaction logic for Win_Main.xaml
	/// </summary>
	public partial class Win_Main : Window
	{
		UserLog us = null;
		public Win_Main()
		{
			InitializeComponent();
		}

		sellEntities databse = new sellEntities();
		private void Btn_Exit_Click(object sender, RoutedEventArgs e)
		{


			databse.sp_Update_exitttime(PublicVariable.GUserID, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)) + "-" + String.Format("{0:HH:mm:ss}", DateTime.Now));
			databse.SaveChanges();
			System.Environment.Exit(0);
		}
		private void Btn_Customer_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 10 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_Customer wn = new Win_Customer();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void Btn_UserLog_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 11 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_UserLogxaml wn = new Win_UserLogxaml();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
		private void Btn_Invoce_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 12 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_Invoice wn = new Win_Invoice();
				wn.Win_state = 1;
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void ShowChart_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 8 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_sellchart wn = new Win_sellchart();

				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btn_showInvoce_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 13 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_InvoceShow wn = new Win_InvoceShow();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Btn_Show_Report(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 15 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_ReportList wn = new Win_ReportList();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void btn_dastresi_click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 19 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_users wn = new Win_users();
				wn.btn_newUser.Visibility = Visibility.Hidden;
				wn.Btn_Deactive.Visibility = Visibility.Hidden;
				wn.button_Edit.Visibility = Visibility.Hidden;
				wn.button_dastresi.Visibility = Visibility.Visible;
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Btn_User_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 9 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{

				Win_users wn = new Win_users();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Btn_ChangePasword_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 27 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_Change_PassWord wn = new Win_Change_PassWord();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void Btn_Product_Click(object sender, RoutedEventArgs e)
		{

			//var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 4 select ua);
			//var result = query.ToList();
			//if (result.Count > 0)
			//{
			Win_Product wn = new Win_Product();
			wn.ShowDialog();
			//}
			//else
			//{
			//    MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			//}
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

			databse.sp_Update_exitttime(PublicVariable.GUserID, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(Calnder1.Text)) + "-" + String.Format("{0:HH:mm:ss}", DateTime.Now));
			databse.SaveChanges();
			databse.SaveChanges();
			System.Environment.Exit(0);

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//setabad();
			//lbl();
			//showInfoRec();

		}



		private void showInfoRec()
		{
			var query = from i in databse.Invoices select i;
			var result = query.ToList();
			lbl_totalcount.Content = result.Count;
			lbl_LastInvoice.Content = result[result.Count - 1].InvoiceId;
			var query1 = databse.sp_porforoshtarin().ToList();
			lbl_porforshtaringheymat.Content = query1[0].ProductName;
			var query2 = databse.sp_porforoshtarintedad().ToList();
			lbl_porfoeoshtarintedad.Content = query2[0].ProductName;
		}

		private void lbl()
		{
			lbl_name.Content = PublicVariable.GUserName;
			lbl_family.Content = PublicVariable.GUserFamily;
			lbl_time.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
		}

		private void setabad()
		{
			this.MaxHeight = 640;
			this.MinHeight = 640;
			this.MaxWidth = 1240;
			this.MinWidth = 1240;
		}

		private void image_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
		{

		}

		private void RibbonButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Btn_AddPart_Click(object sender, RoutedEventArgs e)
		{
			var query = (from ua in databse.UserAccesses where ua.UserID == PublicVariable.GUserID where ua.SystemPartID == 20 select ua);
			var result = query.ToList();
			if (result.Count > 0)
			{
				Win_AddPart wn = new Win_AddPart();
				wn.ShowDialog();
			}
			else
			{
				MessageBox.Show("شما به این بخش دسترسی ندارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
