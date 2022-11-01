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
using static Labb3Bordsbokning.Bokningar;
using static Labb3Bordsbokning.BokningsBord;

namespace Labb3Bordsbokning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> comboTimeLista = new List<string>() { "Välj en tid...", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
        private List<string> comboBordLista = new List<string>() { "Välj ett bord...", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        //En lista för alla sparade bokningar
        public List<Bokning> sparadeBokningarLista = new List<Bokning>();

        //TODO: endast 5 bord ska tillåtas vara bokade samma datum och tid krav 13 VG
        //TODO: Filhantering uppdatera fil vid bokning och avbokning samt läsa från fil vid valet ”Visa bokningar” krav 16 VG
        //TODO: Asyncrona metoder krav 17 VG

        public MainWindow()
        {

            InitializeComponent();
            
            //Fyll Comboboxar
            CBox_Time.ItemsSource = comboTimeLista;
            CBox_Table.ItemsSource = comboBordLista;

            //Skapa exempeldata
            //Krav[6]
            DoTheBoking("2022-10-14", "17:00", 1, "Kalle");
            DoTheBoking("2022-10-22", "16:00", 4, "Inez");
            DoTheBoking("2022-10-30", "18:00", 10, "Otto");

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
            BokningsDagar dag = new BokningsDagar(inputDatum, inputTid);

            //Skicka in datum och tid användaren valde i dialogen till klassen Dag för att spara denna dag med sin tid i en lista
            BokningsDagar.Dag resultDag = dag.SaveThisBokingDay(inputDatum, inputTid);

            BokningsBord bord = new BokningsBord(inputBordNummer, inputNamn);

            //Skicka in bordnummer och namn användaren valde i dialogen till klassen Bord för att spara detta bord med sitt nummer och kundens namn i en lista
            BokningsBord.Bord resultBord = bord.SaveThisTable(inputBordNummer, inputNamn);

            Bokningar bokningar = new Bokningar(resultDag, resultBord);

            //Skicka dag och bord till klassen Bokningar
            Bokning resultBokning = bokningar.SaveBoking(resultDag, resultBord);

            //Lägg till bokningen i en lista
            sparadeBokningarLista.Add(resultBokning);

            string outputString = resultBokning.dag.datum.ToString() + ", " + resultBokning.dag.tid + ", " + resultBokning.bord.namn + ", Bord " + resultBokning.bord.nummer;

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
            //Krav[14]
            var result = sparadeBokningarLista.Where(item => item.dag.datum == listboxDatum && item.dag.tid == listboxTid && item.bord.namn == listboxNamn && item.bord.nummer == listBoxBordNummer).First();

            sparadeBokningarLista.Remove(result);

            string deleteString = result.dag.datum.ToString() + ", " + result.dag.tid + ", " + result.bord.namn + ", Bord " + result.bord.nummer;

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
            //Krav[11]
            //foreach (var bokning in sparadeBokningarLista)
            //{
            //string outputDatum = bokning.dag.datum.ToString();
            //string outputTid = bokning.dag.tid.ToString();
            //string outputBord = bokning.bord.nummer.ToString();
            //string outputNamn = bokning.bord.namn.ToString();

            //LB_Bokningar.Items.Add(outputDatum + ", " + outputTid + ", " + outputNamn + ", Bord " + outputBord);
            //}

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
            //Krav [1]
            string inputDatum = "";
            string inputTid = "";
            int inputBordNummer = 0;
            string inputNamn = "";

            //Krav[7] och [8]
            try
            {
                inputDatum = MyDatePicker.SelectedDate.Value.ToShortDateString().ToString();
                inputTid = CBox_Time.Text;
                inputBordNummer = int.Parse(CBox_Table.Text);

                //Krav[7]
                //TODO: Varför funkar inte regex här? :-(
                //if (!Regex.Match(TBox_Name.Text, "^[A-Z][a-zA-Z]*$").Success)
                //{
                //    //name was incorrect
                //    MessageBox.Show("Detta är inget namn. Vänligen mata in ett rikigt namn.", "OBS!", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}

                inputNamn = TBox_Name.Text;

                bool ärBokat = false;

                //Krav[11]
                foreach(var bokning in sparadeBokningarLista)
                {
                    if(bokning.dag.datum == inputDatum && bokning.dag.tid == inputTid && bokning.bord.nummer == inputBordNummer)
                    {
                        ärBokat = true;
                    }
                }

                //Krav [2] och [3]
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
        
        // Krav[4]
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

        // Krav[5]
        private void Button_Click_CancelBoking(object sender, RoutedEventArgs e)
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
