using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace Labb3Bordsbokning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> comboTimeLista = new List<string>() { "Välj en tid...", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
        private List<string> comboBordLista = new List<string>() { "Välj ett bord...", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public MainWindow()
        {

            InitializeComponent();
            
            //Fyll Comboboxar
            CBox_Time.ItemsSource = comboTimeLista;
            CBox_Table.ItemsSource = comboBordLista;

            //Skapa exempeldata bara om inte filen redan finns. Annars läggs data in dubbelt
            if (!File.Exists("FilMedAllaBokningar.txt"))
            {
                DoTheBoking("2022-10-14", "17:00", 1, "Kalle");
                DoTheBoking("2022-10-22", "16:00", 4, "Inez");
                DoTheBoking("2022-10-30", "18:00", 10, "Otto");
            }

            DisplayContent();

        }

        /// <summary>
        /// Denna metod genomför själva bokningen
        /// </summary>
        /// <param name="inputDatum"></param>
        /// <param name="inputTid"></param>
        /// <param name="inputBordNummer"></param>
        /// <param name="inputNamn"></param>
        private void DoTheBoking(string inputDatum, string inputTid, int inputBordNummer, string inputNamn)
        {

            string outputString = inputDatum.ToString() + ", " + inputTid + ", " + inputNamn + ", Bord " + inputBordNummer;

            //Skicka in outputsträngen till klassen för filhantering
            Filhantering filhantering = new Filhantering();
            filhantering.SaveBokingsToFile(outputString);
        }

        /// <summary>
        /// Denna metod radera en vald bokning
        /// </summary>
        /// <param name="listboxDatum"></param>
        /// <param name="listboxTid"></param>
        /// <param name="listboxNamn"></param>
        /// <param name="listBoxBordNummer"></param>
        private void CancelTheBoking(string listboxDatum, string listboxTid, string listboxNamn, int listBoxBordNummer)
        {
            string deleteString = listboxDatum + ", " + listboxTid + ", " + listboxNamn + ", Bord " + listBoxBordNummer;

            //Skicka in outputsträngen till klassen för filhantering
            Filhantering filhantering = new Filhantering();
            filhantering.DeleteBokingFromFile(deleteString);
        }

        /// <summary>
        /// Denna metod tömmer all innehåll i inmatningsfälten
        /// </summary>
        private void ClearFields()
        {
            MyDatePicker.Text = "";
            TBox_Name.Text = "";
            CBox_Time.Text = "";
            CBox_Table.Text = "";
        }

        /// <summary>
        /// Metoden visa all data i listboxen
        /// </summary>
        private void DisplayContent()
        {
            Filhantering filhantering = new Filhantering();

            foreach (var item in (filhantering.ReadAllBokingsFromFile()))
            {
                LB_Bokningar.Items.Add(item);
            }
        }

        /// <summary>
        /// Metoden körs efter knappen "Boka" klickades och kollar även om den bokningen som ska göras redan finns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SaveBoking(object sender, RoutedEventArgs e)
        {
            string inputDatum = "";
            string inputTid = "";
            int inputBordNummer = 0;
            string inputNamn = "";

            try
            {
                inputDatum = MyDatePicker.SelectedDate.Value.ToShortDateString().ToString();
                inputTid = CBox_Time.Text;
                inputBordNummer = int.Parse(CBox_Table.Text);

                if ((!Regex.IsMatch(TBox_Name.Text, @"^[\p{L}\p{M}' \.\-]+$")) || String.IsNullOrWhiteSpace(TBox_Name.Text))
                {
                    MessageBox.Show("Detta är inget namn. Vänligen mata in ett rikigt namn.", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                inputNamn = TBox_Name.Text;
                bool ärBokat = false;
                int counter = 0;

                Filhantering filhantering = new Filhantering();

                foreach (var item in (filhantering.ReadAllBokingsFromFile()))
                {
                    string[] result = item.ToString().Split(',');

                    string Datum = result[0].Trim();
                    string Tid = result[1].Trim();
                    string Namn = result[2].Trim();
                    int BordNummer = int.Parse(result[3].Substring(5).Trim());

                    if(Datum == inputDatum && Tid == inputTid && BordNummer == inputBordNummer)
                    {
                        ärBokat = true;
                    }

                    if(Datum == inputDatum && Tid == inputTid)
                    {
                        counter++;
                    }
                }

                if (counter > 4)
                {
                    MessageBox.Show("Denna dag och tid finns redan för många bokningar.", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }

                if (ärBokat == true)
                {
                    MessageBox.Show("Denna dag och tid är detta bord redan bokat.", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DoTheBoking(inputDatum, inputTid, inputBordNummer, inputNamn);

                    MessageBox.Show("Din bokning är nu sparat.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OBS! Något gick fel. Vänligen kontrollera dina inmatningar!", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        /// <summary>
        /// Denna metod körs efter knappen "Visa bokningar" klickade och kallar på metoden DisplayContent()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ShowBokings(object sender, RoutedEventArgs e)
        {
            LB_Bokningar.Items.Clear();
            ClearFields();

            DisplayContent();
        }

        /// <summary>
        /// Metoden körs efter knappen "Avboka" klickades och kör sedan metoden CancelTheBoking()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_CancelBoking(object sender, RoutedEventArgs e)
        {
            if (LB_Bokningar.SelectedItem != null)
            {
                var result = LB_Bokningar.SelectedItem.ToString().Split(',');

                string listboxDatum = result[0].Trim();
                string listboxTid = result[1].Trim();
                string listboxNamn = result[2].Trim();
                int listBoxBordNummer = int.Parse(result[3].Substring(5).Trim());

                CancelTheBoking(listboxDatum, listboxTid, listboxNamn, listBoxBordNummer);

                LB_Bokningar.Items.Clear();

                MessageBox.Show("Din bokning är nu raderat.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();

                DisplayContent();
            }
        }
    }
}
