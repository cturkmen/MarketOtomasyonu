using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otomasyon
{
    public class Telefon
    {
        public Telefon()
        {
            string imei = string.Empty;
            do
            {// Eşsiz değerler üretir.Sayı ve harf karışık değerler vardır
                string guid = Guid.NewGuid().ToString().Replace("-", "");
                foreach (char item in guid)
                {
                    if (imei.Length == 15) break;
                    if (char.IsDigit(item))//sadece sayıları tutucaz.
                    {
                        imei += item;
                    }
                }
            } while (imei.Length != 15);
            IMEI = imei;

        }

        private decimal _fiyat;
        private DateTime _satisTarihi;
        public string IMEI { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public IsletimSistemleri IsletimSistemi { get; set; }
        public decimal Fiyat
        {
            get { return _fiyat; }
            set
            {
                if (value <= 0)
                    throw new Exception("Ürün fiyatı 0 ve altı olamaz");
                _fiyat = value;
            }
        }
        public DateTime SatisTarihi
        {
            get { return _satisTarihi; }
            set
            {
                if (DateTime.Now < value)
                    throw new Exception("Geleceğe satış yapılamaz");
                _satisTarihi = value;
            }
        }
        public Color Renk { get; set; }
        public byte[] Fotograf { get; set; }
        public override string ToString()
        {
            return $"{Marka} - {Model} - {Fiyat:C2}";
        }

        public static List<Telefon>Ara(string kelime)
        {
            //if (kelime.Length < 3)
            //    return new List<Telefon>();
            List<Telefon> bulunan = Form1.telefonlar.Where(
                x =>
                x.Marka.ToLower().StartsWith(kelime.ToLower()) ||//sa yazınca otomatik gelir
                x.Model.ToLower().Contains(kelime.ToLower()) ||
                x.IMEI == kelime
            ).ToList();
            return bulunan;
        }
    }

    public enum IsletimSistemleri
    {
        IOS,
        Android,
        WindowsPhone,
        Symbian
    }


}
