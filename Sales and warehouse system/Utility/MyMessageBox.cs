using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using system.Properties;
using Sales_and_warehouse_system.Windows;
using System.Windows;

namespace System
{
    public static class MyMessageBox
    {
        public static void ShowMessageValidation(string Message=" اطلاعات باکس ها را وارد نمایید ")
        {
            MessageBox.Show(Message, "خطا", MessageBoxButton.OK);
        }
        public static void ShowMessageSuccess(string Message = "عملیات با موفقیت انجام شد")
        {
            MessageBox.Show(Message, "اطلاع", MessageBoxButton.OK);
        }
        public static void ShowMessageSelection(string Message=" لطفا موردی را از لیست انتخاب نمایید ")
        {
            MessageBox.Show(Message, "خطا", MessageBoxButton.OK);
        }
       
      
    }
}
