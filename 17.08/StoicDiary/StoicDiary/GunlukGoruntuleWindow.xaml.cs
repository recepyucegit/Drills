using System.IO;
using System.Windows;

namespace GunlukUygulamasi
{
    public partial class GunlukGoruntuleWindow : Window
    {
        public GunlukGoruntuleWindow()
        {
            InitializeComponent();
            YukleGunlukler();
        }

        private void YukleGunlukler()
        {
            string[] dosyalar = Directory.GetFiles(Directory.GetCurrentDirectory(), "Gunluk_*.txt");

            foreach (string dosya in dosyalar)
            {
                lstGunlukler.Items.Add(System.IO.Path.GetFileName(dosya));
            }
        }

        private void lstGunlukler_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstGunlukler.SelectedItem != null)
            {
                string dosyaAdi = lstGunlukler.SelectedItem.ToString();
                string icerik = File.ReadAllText(dosyaAdi);
                txtIcerik.Text = icerik;
            }
        }
    }
}
