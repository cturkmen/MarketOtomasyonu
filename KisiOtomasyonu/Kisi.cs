using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisiOtomasyonu
{
    public class Kisi
    {

        private string _ad, _soyad, _tckn, _ceptel, _mail;
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string TCKN
        {
            get { return _tckn; }
            set
            {
                if (value.Length != 11)
                    throw new Exception("tckn 11 haneli olmalıdır");
                foreach (char item in value)
                {
                    if (!char.IsDigit(item))
                        throw new Exception("tckn sayıdan başka değer içeremez.");
                }

                _tckn = value;

            }
        }

        public string CepTel
        {
            get { return _ceptel; }
            set
            {
                if (value.StartsWith("0"))
                    throw new Exception("Numaranın başına 0 koymadan yazınız.");
                if (value.Length != 10)
                    throw new Exception("Numara 10 haneli olmalıdır.Eksik veya fazla giriş yaptınız.");
                _ceptel = value;
            }
        }

        public string Mail
        {
            get { return _mail; }
            set
            {
                if (!value.Contains("@"))
                    throw new Exception("Mail adresi @ karakteri içermelidir.");
                _mail = value;
            }
        }
        public DateTime DogumTarihi { get; set; }

        public Cinsiyetler Cinsiyet { get; set; }

        public string AdSoyadFormat(string kelime)
        {

            foreach (char item in kelime)
            {
                if (!char.IsLetter(item) || char.IsWhiteSpace(item))
                    throw new Exception("Adınız ve soyadınız sadece harflerden oluşmmalıdır");

                
            }
            
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(kelime);

        }


    }

    public enum Cinsiyetler
    {
        Kadın, Erkek
    }
}
