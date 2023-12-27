using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsaUygulama.Services
{
    public class KullaniciBilgiler
    {
        private static KullaniciBilgiler instance;

        public static KullaniciBilgiler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KullaniciBilgiler();
                }
                return instance;
            }
        }

        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public DateTime UyelikTarih { get; set; }
        public string Cinsiyet { get; set; }
    }
}