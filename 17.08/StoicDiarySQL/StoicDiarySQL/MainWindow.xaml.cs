using Microsoft.Data.Sqlite;
using System;
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
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO Gunlukler 
                      (Tarih, Sukur1, Sukur2, Sukur3, Yanlislar, Duzeltmeler, Dogrular, GunIcinde)
                       VALUES (@Tarih, @S1, @S2, @S3, @Yanlis, @Duzelt, @Dogru, @GunIcinde)";

                using (var cmd = new SqliteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@S1", txtSukur1.Text);
                    cmd.Parameters.AddWithValue("@S2", txtSukur2.Text);
                    cmd.Parameters.AddWithValue("@S3", txtSukur3.Text);
                    cmd.Parameters.AddWithValue("@Yanlis", txtYanlislar.Text);
                    cmd.Parameters.AddWithValue("@Duzelt", txtDuzeltmeler.Text);
                    cmd.Parameters.AddWithValue("@Dogru", txtDogrular.Text);
                    cmd.Parameters.AddWithValue("@GunIcinde", txtGunIcinde.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("✅ Günlük kaydedildi!", "Başarılı");

            Temizle();
        }

        private void Temizle()
        {
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
