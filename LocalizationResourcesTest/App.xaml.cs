using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizationResourcesTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var dataContext = new MainWindowVM();
            var mainWindow = new MainWindow(dataContext);
            mainWindow.Show();
        }
    }
}
