using StoicDiary;
using System;
using System.IO;
using System.Windows;

namespace GunlukUygulamasi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            string tarih = DateTime.Now.ToString("yyyy-MM-dd");
            string dosyaAdi = $"Gunluk_{tarih}.txt";

            using (StreamWriter sw = new StreamWriter(dosyaAdi, true))
            {
                sw.WriteLine("📓 Günlük - " + tarih);
                sw.WriteLine("\n--- Şükür Günlüğü ---");
                sw.WriteLine("1. " + txtSukur1.Text);
                sw.WriteLine("2. " + txtSukur2.Text);
                sw.WriteLine("3. " + txtSukur3.Text);

                sw.WriteLine("\n--- Stoic Günlük ---");
                sw.WriteLine("Yanlışlarım: " + txtYanlislar.Text);
                sw.WriteLine("Düzeltmelerim: " + txtDuzeltmeler.Text);
                sw.WriteLine("Doğrularım: " + txtDogrular.Text);
                sw.WriteLine("Gün İçinde Yaptıklarım: " + txtGunIcinde.Text);

                sw.WriteLine("\n-----------------------------\n");
            }

            MessageBox.Show("Günlük kaydedildi: " + dosyaAdi,
                            "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);

            // Kaydettikten sonra alanları temizle
            txtSukur1.Clear();
            txtSukur2.Clear();
            txtSukur3.Clear();
            txtYanlislar.Clear();
            txtDuzeltmeler.Clear();
            txtDogrular.Clear();
            txtGunIcinde.Clear();
        }

        private void btnGoruntule_Click(object sender, RoutedEventArgs e)
        {
            GunlukGoruntuleWindow pencere = new GunlukGoruntuleWindow();
            pencere.ShowDialog();
        }
    }
}
