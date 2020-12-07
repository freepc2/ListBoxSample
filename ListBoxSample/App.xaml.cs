using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ListBoxSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Window window = new Window() { MaxHeight = 600, MaxWidth = 1024 };
            window.Content = new View.ListBoxView();

            MainWindow = window;

            window.Show();
        }
    }
}
