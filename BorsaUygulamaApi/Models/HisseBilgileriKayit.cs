namespace BorsaUygulamaApi.Models
{
    public class HisseBilgileriKayit
    {
        public int Id { get; set; }
        public Guid HisseSahibiId { get; set; }
        public string AlimSatim { get; set; }
        public string HisseAdi { get; set; }
        public string HisseAdedi { get; set; }
        public string HisseFiyat { get; set; }
        public DateTime HisseAlimSatimDate { get; set; }
    }
}
