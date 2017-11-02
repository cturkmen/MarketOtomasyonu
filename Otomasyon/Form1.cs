using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<Telefon> telefonlar = new List<Telefon>();//Ram de 1 tane oldu. dolayısıyla telefon classından ulaşabiliriz.sadece public yapılsaydı instanceını alacaktık. yeni bir örnek yaratacaktık ve içi boş gelecekti.static yaparak insantce almayı engelledik. ve içi dolu geldi.

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbIsletimSistemi.Items.AddRange(Enum.GetNames(typeof(IsletimSistemleri)));

        }

        private void btnRenk_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                btnRenk.BackColor = colorDialog1.Color;

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                Telefon yeniTelefon = new Telefon();
                yeniTelefon.Fiyat = nFiyat.Value;
                yeniTelefon.Marka = txtMarka.Text;
                yeniTelefon.Model = txtModel.Text;
                yeniTelefon.IsletimSistemi = (IsletimSistemleri)Enum.Parse(typeof(IsletimSistemleri), cmbIsletimSistemi.SelectedItem.ToString());
                yeniTelefon.SatisTarihi = dtpSatisTarihi.Value;
                yeniTelefon.Renk = btnRenk.BackColor;

                telefonlar.Add(yeniTelefon);
                ListeyiDoldur();
                FormuTemizle();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message); ;
            }


        }
        void ListeyiDoldur()
        {
            lstTelefon.Items.Clear();
            telefonlar.ForEach(telefon => lstTelefon.Items.Add(telefon));// List oluşturmak lazım
        }

        void FormuTemizle()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                    (item as TextBox).Text = string.Empty;
                else if (item is ComboBox)
                    (item as ComboBox).SelectedIndex = -1;
                else if (item is NumericUpDown)
                    (item as NumericUpDown).Value = 0;
                else if (item is DateTimePicker)
                    (item as DateTimePicker).Value = DateTime.Now;
            }
            btnRenk.BackColor = this.BackColor;
        }

        Telefon seciliTelefon;
        private void lstTelefon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTelefon.SelectedItem == null) return;

            seciliTelefon = lstTelefon.SelectedItem as Telefon;
            this.Text = seciliTelefon?.IMEI;//seciliTelefon null değilse işlem yap
            txtMarka.Text = seciliTelefon.Marka;
            txtModel.Text = seciliTelefon.Model;
            cmbIsletimSistemi.SelectedIndex = (int)seciliTelefon.IsletimSistemi;
            nFiyat.Value = seciliTelefon.Fiyat;
            dtpSatisTarihi.Value = seciliTelefon.SatisTarihi;
            btnRenk.BackColor = seciliTelefon.Renk;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (lstTelefon.SelectedItem == null)
            {
                MessageBox.Show("Güncellemek için kayıt seçmelisin");
                return;
            }
            seciliTelefon = lstTelefon.SelectedItem as Telefon;

            Telefon guncellenenTelefon = telefonlar.Where(x => x.IMEI == seciliTelefon.IMEI).FirstOrDefault();

            guncellenenTelefon.Fiyat = nFiyat.Value;
            guncellenenTelefon.Marka = txtMarka.Text;
            guncellenenTelefon.Model = txtModel.Text;
            guncellenenTelefon.IsletimSistemi = (IsletimSistemleri)Enum.Parse(typeof(IsletimSistemleri), cmbIsletimSistemi.SelectedItem.ToString());
            guncellenenTelefon.SatisTarihi = dtpSatisTarihi.Value;
            guncellenenTelefon.Renk = btnRenk.BackColor;

            ListeyiDoldur();
            FormuTemizle();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            List<Telefon> bulunan = Telefon.Ara(txtAra.Text);
            this.Text = $"{bulunan.Count} adet kayıt bulundu";
            if (string.IsNullOrEmpty(txtAra.Text))
                ListeyiDoldur();
            else
                ListeyiDoldur(bulunan);
        }

        private void ListeyiDoldur(List<Telefon> bulunan)
        {
            lstTelefon.Items.Clear();
            bulunan.ForEach(telefon => lstTelefon.Items.Add(telefon));
        }
    }
}
