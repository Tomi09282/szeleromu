using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace szeleromu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Beolvas();
        }


        List<Szeleromu> szeleromuvek = new List<Szeleromu>();
        public void Beolvas()
        {
            StreamReader sr = new StreamReader("szeleromu.txt");
            while (!sr.EndOfStream) {
                string[] adatok = sr.ReadLine().Split(";");

                adatok[1].Split(",");
                szeleromuvek.Add(new Szeleromu(adatok[0], int.Parse(adatok[1]), int.Parse(adatok[2]), int.Parse(adatok[3])));
            }
            dataGridSzeleromuk.ItemsSource = szeleromuvek;
        }

        private void btnPowerUnits_Click(object sender, RoutedEventArgs e)
        {
            string location = txtLocation.Text;
            var teljesitmenyEgyseg = szeleromuvek.FindAll(s => s.Helyszin == location);
            listBoxPowerUnits.ItemsSource = teljesitmenyEgyseg;
            lblTotalPowerUnits.Content = $"Összes egység: {teljesitmenyEgyseg.Count}";
        }

        private void btnCategorize_Click(object sender, RoutedEventArgs e)
        {
            var kategoriak = new List<char>();
            foreach (var szeleromu in szeleromuvek)
            {
                char category = szeleromu.GetCategory();
                kategoriak.Add(category);
            }
            string message = string.Join(", ", kategoriak);
            MessageBox.Show(message);
        }

        private void btnAveragePerformance_Click(object sender, RoutedEventArgs e)
        {
            double averagePerformance = CalculateAveragePerformance();
            MessageBox.Show($"Átlagos teljesítmény (2010): {averagePerformance:F2} W");
        }


        private double CalculateAveragePerformance()
        {
            double sum = 0;
            int count = 0;
            foreach (var szeleromu in szeleromuvek)
            {
                if (szeleromu.MukesKezdete.Year == 2010)
                {
                    sum += szeleromu.Teljesitmeny;
                    count++;
                }
            }
            return sum / count;
        }


        private Szeleromu Max()
        {
            Szeleromu MaxTeljesitmeny = szeleromuvek[0];
            foreach (var szeleromu in szeleromuvek)
            {
                if (szeleromu.Teljesitmeny > MaxTeljesitmeny.Teljesitmeny)
                {
                    MaxTeljesitmeny = szeleromu;
                }
            }
            return MaxTeljesitmeny;
        }

        private void btnMaxPerformance_Click(object sender, RoutedEventArgs e)
        {
            var maxPerformance = Max();
            MessageBox.Show($"Legnagyobb teljesítmény: Helyszín: {maxPerformance.Helyszin}, Teljesítmény: {maxPerformance.Teljesitmeny} W, Működés kezdete: {maxPerformance.MukesKezdete.Year}");
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder report = new StringBuilder();
            foreach (var szeleromu in szeleromuvek)
            {
                string kategoria = szeleromu.GetCategory().ToString();
                report.AppendLine($"{szeleromu.Helyszin}, {szeleromu.Egységek}, {szeleromu.Teljesitmeny}, {kategoria}");
            }

            File.WriteAllText("jelentes.txt", report.ToString());
            MessageBox.Show("Jelentés generálva: jelentes.txt");
        }

        private void btnTotalCount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Szélerőművek száma: {szeleromuvek.Count}");
        }
    }
    }
