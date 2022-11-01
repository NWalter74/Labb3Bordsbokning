using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Labb3Bordsbokning
{
    public enum ApplicationExitCode
    {
        Success = 0,
        Failure = 1,
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void App_Exit(object sender, ExitEventArgs e)
        {
                if (e.ApplicationExitCode == (int)ApplicationExitCode.Success)
                {
                    MessageBox.Show("Tack att du testade min applikation.\nHoppas du tyckte om den.\nHa en fin dag!", "Hejdå och Tack!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
        }
    }
}