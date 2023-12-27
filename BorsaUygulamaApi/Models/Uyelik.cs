namespace BorsaUygulamaApi.Models
{
    public class Uyelik
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public DateTime UyelikTarih { get; set; }
        public string Cinsiyet { get; set; }
    }
}
