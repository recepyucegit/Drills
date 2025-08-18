using Microsoft.Data.Sqlite;
using System.Windows;

namespace GunlukUygulamasi
{
    public partial class GunlukGoruntuleWindow : Window
    {
        private int secilenId = -1;

        public GunlukGoruntuleWindow()
        {
            InitializeComponent();
            YukleGunlukler();
        }

        private void YukleGunlukler()
        {
            lstGunlukler.Items.Clear();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("SELECT Id, Tarih FROM Gunlukler ORDER BY Id DESC", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstGunlukler.Items.Add($"{reader["Id"]} - {reader["Tarih"]}");
                }
            }
        }

        private void lstGunlukler_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstGunlukler.SelectedItem == null) return;

            string secilen = lstGunlukler.SelectedItem.ToString();
            secilenId = int.Parse(secilen.Split('-')[0].Trim());

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("SELECT * FROM Gunlukler WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", secilenId);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtTarih.Text = reader["Tarih"].ToString();
                    txtSukur1.Text = reader["Sukur1"].ToString();
                    txtSukur2.Text = reader["Sukur2"].ToString();
                    txtSukur3.Text = reader["Sukur3"].ToString();
                    txtYanlislar.Text = reader["Yanlislar"].ToString();
                    txtDuzeltmeler.Text = reader["Duzeltmeler"].ToString();
                    txtDogrular.Text = reader["Dogrular"].ToString();
                    txtGunIcinde.Text = reader["GunIcinde"].ToString();
                }
            }
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (secilenId == -1) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand(@"UPDATE Gunlukler SET 
                                              Sukur1=@s1, Sukur2=@s2, Sukur3=@s3,
                                              Yanlislar=@yanlis, Duzeltmeler=@duzelt,
                                              Dogrular=@dogru, GunIcinde=@gun
                                              WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@s1", txtSukur1.Text);
                cmd.Parameters.AddWithValue("@s2", txtSukur2.Text);
                cmd.Parameters.AddWithValue("@s3", txtSukur3.Text);
                cmd.Parameters.AddWithValue("@yanlis", txtYanlislar.Text);
                cmd.Parameters.AddWithValue("@duzelt", txtDuzeltmeler.Text);
                cmd.Parameters.AddWithValue("@dogru", txtDogrular.Text);
                cmd.Parameters.AddWithValue("@gun", txtGunIcinde.Text);
                cmd.Parameters.AddWithValue("@id", secilenId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("✅ Günlük güncellendi!");
            YukleGunlukler();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (secilenId == -1) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("DELETE FROM Gunlukler WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", secilenId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("🗑️ Günlük silindi!");
            secilenId = -1;

            // TextBox’ları temizle
            txtTarih.Text = "";
            txtSukur1.Clear();
            txtSukur2.Clear();
            txtSukur3.Clear();
            txtYanlislar.Clear();
            txtDuzeltmeler.Clear();
            txtDogrular.Clear();
            txtGunIcinde.Clear();

            YukleGunlukler();
        }
    }
}
