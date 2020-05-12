using Sales_and_warehouse_system.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sales_and_warehouse_system
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender , StartupEventArgs e) {
            Win_Login wn = new Win_Login();
            wn.ShowDialog();
            Win_Main wm = new Win_Main();
            wm.ShowDialog();
        }
    }
}
