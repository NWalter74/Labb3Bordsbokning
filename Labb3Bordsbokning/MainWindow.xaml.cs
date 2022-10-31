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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb3Bordsbokning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> comboTimeList = new List<string>() { "Välj en tid...", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
        public List<string> comboBordList = new List<string>() { "Välj ett bord...", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };


        public MainWindow()
        {

            InitializeComponent();

            CBox_Time.ItemsSource = comboTimeList;
            CBox_Table.ItemsSource = comboBordList;
                
        }
    }
}
