﻿using System;
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
        List<string> comboTimeLista = new List<string>() { "Välj en tid...", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
        List<string> comboBordLista = new List<string>() { "Välj ett bord...", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };



        public MainWindow()
        {

            InitializeComponent();

            CBox_Time.ItemsSource = comboTimeLista;
            CBox_Table.ItemsSource = comboBordLista;
                
        }

        private void DoTheBoking(string inputDatum, string inputTid, int inputBordNummer, string inputNamn)
        {
            BokningsDag dag = new BokningsDag(inputDatum, inputTid);

            var resultDag = dag.SparaDennaBokningDag(inputDatum, inputTid);

            BokningsBord bord = new BokningsBord(inputBordNummer, inputNamn);

            var resultBord = bord.SparaDennaBord(inputBordNummer, inputNamn);

            Bokningar bokningar = new Bokningar(resultDag, resultBord);

            bokningar.SparaBokning(resultDag, resultBord);
        }

        private void ClearFields()
        {
            MyDatePicker.Text = "";
            TBox_Name.Text = "";
            CBox_Time.Text = "";
            CBox_Table.Text = "";
        }

        private void Button_Click_SparaBokning(object sender, RoutedEventArgs e)
        {
            string inputDatum = MyDatePicker.SelectedDate.Value.ToShortDateString().ToString();
            string inputTid = CBox_Time.Text;
            int inputBordNummer = int.Parse(CBox_Table.Text);
            string inputNamn = TBox_Name.Text;

            DoTheBoking(inputDatum, inputTid, inputBordNummer, inputNamn);

            MessageBox.Show("Din bokning är nu sparat.");

            ClearFields();
        }
    }
}
