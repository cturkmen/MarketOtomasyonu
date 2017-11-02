using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Urun> Urunler = new List<Urun>();

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbKategori.Items.AddRange(Enum.GetNames(typeof(UrunKategorisi)));
            txtUrunNo.Enabled = true;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Urun yeniUrun = new Urun()
                {
                    UrunAdi = txtUrunAd.Text,
                    UrunNo = txtUrunNo.Text,
                    Adet = (int)nAdet.Value,
                    GarantiBaslama = dtpGBaslama.Value,
                    SatisTarihi = dtpSatis.Value,
                    Kategori = (UrunKategorisi)Enum.Parse(typeof(UrunKategorisi), cmbKategori.SelectedItem.ToString()),
                    Fiyat = nFiyat.Value
                };
                Urunler.Add(yeniUrun);
                ListeyiDoldur();
                Temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message); ;
            }

        }
        void ListeyiDoldur()
        {
            lstUrun.Items.Clear();
            foreach (Urun item in Urunler)
            {
                lstUrun.Items.Add(item);
            }

        }

        void Temizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is ComboBox)
                    (item as ComboBox).SelectedIndex = -1;
                else if (item is DateTimePicker)
                    (item as DateTimePicker).Value = DateTime.Now;
                else if (item is NumericUpDown)
                    (item as NumericUpDown).Value = 0;
            }
        }

        private void dtpSatis_ValueChanged(object sender, EventArgs e)
        {
            dtpGBaslama.Value = dtpSatis.Value;
            dtpGBitis.Value = dtpGBaslama.Value.AddYears(2);
        }
        Urun seciliUrun;

        private void lstUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUrunNo.Enabled = false;
            if (lstUrun.SelectedIndex == -1) return;
            seciliUrun = lstUrun.SelectedItem as Urun;
            txtUrunAd.Text = seciliUrun.UrunAdi;
            txtUrunNo.Text = seciliUrun.UrunNo;
            cmbKategori.SelectedIndex = (int)seciliUrun.Kategori;
            nAdet.Value = seciliUrun.Adet;
            nFiyat.Value = seciliUrun.Fiyat;
            dtpSatis.Value = seciliUrun.SatisTarihi;
            dtpGBaslama.Value = seciliUrun.GarantiBaslama;
            dtpGBitis.Value = seciliUrun.GarantiBitis;

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            if (seciliUrun == null)
            {
                MessageBox.Show("Güncellemek için ürün seçmelisin");
                return;
            }
            DialogResult dr = MessageBox.Show($"{seciliUrun.UrunAdi} adlı kişiyi güncellemek istiyor musunuz?", "Kişi güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    seciliUrun = Urunler.Where(item => item.UrunNo == seciliUrun.UrunNo).FirstOrDefault();

                    seciliUrun.UrunAdi = txtUrunAd.Text;
                    seciliUrun.Kategori = (UrunKategorisi)Enum.Parse(typeof(UrunKategorisi), cmbKategori.SelectedItem.ToString());
                    seciliUrun.Adet = Convert.ToInt32(nAdet.Value);
                    seciliUrun.Fiyat = Convert.ToInt32(nFiyat.Value);
                    seciliUrun.SatisTarihi = dtpSatis.Value;
                    seciliUrun.GarantiBaslama = dtpGBaslama.Value;

                    ListeyiDoldur();
                    Temizle();
                    seciliUrun = null;
                    MessageBox.Show("Güncelleme Başarılı");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { txtUrunNo.Enabled = true; }


            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstUrun.SelectedItem == null)
            {
                MessageBox.Show("Silmek için bir ürün seçiniz");
                return;
            }

            seciliUrun = lstUrun.SelectedItem as Urun;
            DialogResult dr = MessageBox.Show($"Sepete eklediğiniz {seciliUrun.UrunAdi} ürününü silmek istiyormusunuz?", "Ürün silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    Urunler.Remove(seciliUrun);
                    ListeyiDoldur();
                    Temizle();
                    seciliUrun = null;
                    MessageBox.Show("Ürününüz sepetten çıkarıldı");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                { txtUrunNo.Enabled = true; }
            }
        }
    }
}
