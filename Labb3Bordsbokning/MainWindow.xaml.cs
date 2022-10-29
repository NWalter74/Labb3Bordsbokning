using Labb3Bordsbokning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        //List of all bokingdays like a calender
        List<BokningsDag> listaAvBokningsdagar = new List<BokningsDag>();

        public MainWindow()
        {
            InitializeComponent();

            //Create exampledata for listbox
            DoTheBoking("2022-10-14", "17:00", 1, "Kalle");
            DoTheBoking("2022-10-22", "16:00", 4, "Inez");
            DoTheBoking("2022-10-30", "18:00", 10, "Otto");

            //Show data in listbox
            DisplayContent();

        }

        /// <summary>
        /// Create objekt of day and time and add to list of bokings
        /// </summary>
        /// <param name="bokningsDatum"></param>
        /// <param name="bokningsTid"></param>
        /// <param name="bokningsBordNo"></param>
        /// <param name="bokningNamn"></param>
        private void DoTheBoking(string bokningsDatum, string bokningsTid, int bokningsBordNo, string bokningNamn)
        {
            BokningsTid tid = new BokningsTid(bokningsTid);

            //take table number and name to method for boking a table
            tid.BokingATable(bokningsBordNo, bokningNamn);

            BokningsDag dag = new BokningsDag(bokningsDatum);

            //take time to method for boking this table we choosed now
            dag.BokingOfTime(tid);

            //save day in list of bokings
            listaAvBokningsdagar.Add(dag);
        }

        private void CancelTheBoking(string datum, string tid, string namn, int bordNummer)
        {
            var result = listaAvBokningsdagar.Where(item => item.datum == datum).First();
            result.CancelTime(tid, namn, bordNummer);
        }

        /// <summary>
        /// Show content of listbox
        /// </summary>
        private void DisplayContent()
        {
            //take all dates with a boking
            foreach (var datum in listaAvBokningsdagar)
            {
                //takes all times with a date
                foreach(var tid in datum.tidLista)
                {
                    //takes all tables which are boked
                    foreach(var bord in tid.bordArray.Where(item => item.IsBoked() == true))
                    {
                        LB_Bokningar.Items.Add(datum.datum + ", " + tid.tid + ", " + bord.namn + ", Bord " + bord.number );
                    }
                }
            }
        }

        private void ClearFields()
        {
            MyDatePicker.Text = "";
            TBox_Name.Text = "";
            CBox_Time.Text = "";
            CBox_Table.Text = "";
        }

        private void Bt_Bokning_Click(object sender, RoutedEventArgs e)
        {
            string datum = MyDatePicker.SelectedDate.Value.ToShortDateString().ToString();
            string tid = CBox_Time.Text;
            int bordNummer = int.Parse(CBox_Table.Text.ToString().Substring(5));
            string namn = TBox_Name.Text;

            DoTheBoking(datum, tid, bordNummer, namn);

            MessageBox.Show("Din bokning är nu sparat.");

            ClearFields();
        }

        private void Bt_ShowBoking_Click(object sender, RoutedEventArgs e)
        {
            LB_Bokningar.Items.Clear();
            ClearFields();

            DisplayContent();
        }

        private void Bt_DeleteBokning_Click(object sender, RoutedEventArgs e)
        {
            var result = LB_Bokningar.SelectedItem.ToString().Split(',');

            string datum = result[0].Trim();
            string tid = result[1].Trim();
            string namn = result[2].Trim();
            int bordNummer = int.Parse(result[3].Substring(5).Trim());

            CancelTheBoking(datum, tid, namn, bordNummer);

            LB_Bokningar.Items.Clear();

            MessageBox.Show("Din bokning är nu raderat.");
            ClearFields();

            DisplayContent();
        }
    }
}