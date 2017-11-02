using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOtomasyonu
{
    public class Urun
    {
        private string _urunno, _urunadi;
        private decimal _fiyat;
        private DateTime _satistarihi, _garantibaslama;
        private int _adet;

        public UrunKategorisi Kategori { get; set; }
        public DateTime SatisTarihi
        {
            get { return _satistarihi; }
            set { _satistarihi = value; }
        }

        public string UrunNo
        {
            get { return _urunno; }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new Exception("Ürün numarası boş geçilemez");

                if (value.Trim().Length == 6)
                    _urunno = value;
                else
                    throw new Exception("Ürün numarası 6 karakter olmalıdır.");
                

            }
        }

        public string UrunAdi
        {
            get { return _urunadi; }
            set
            {
                foreach (char ad in value)
                {
                    if (char.IsDigit(ad) || char.IsWhiteSpace(ad))
                        throw new Exception("Ürün adında geçersiz karakterler vardır");
                }
                _urunadi = value;

            }
        }

        public DateTime GarantiBitis
        {
            get { return _garantibaslama.AddYears(2); }
            
        }

        public DateTime GarantiBaslama
        {
            get { return _garantibaslama; }
            set { _garantibaslama = value; }
        }

        public int Adet
        {
            get { return _adet; }
            set {_adet = value; }
        }

        
        public decimal Fiyat
        {
            get { return _fiyat; }
            set {
                if (_adet >= 5)
                    _fiyat = (value * _adet) * 0.8m;
                else
                    _fiyat = (value * _adet);

            }
        }




        public override string ToString()
        {
            return $"Urun Kodu: {_urunno} - {_adet} adet - Urun: {_urunadi} - Fiyat: {Fiyat:C2}";
        }
    }



    public enum UrunKategorisi
    {
        ElektrikliEvAletleri,
        Bilgisayar,
        Telefon,
        Oyun,
        KişiselBakım
    }
}
