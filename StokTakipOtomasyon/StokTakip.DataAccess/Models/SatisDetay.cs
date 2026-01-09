namespace StokTakip.DataAccess.Models
{
    public class SatisDetay
    {
        public int DetayID { get; set; }
        public int SatisID { get; set; }
        public int UrunID { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }
        public decimal? AlisFiyati { get; set; }

        // Navigation Properties
        public string UrunAdi { get; set; }
        public string UrunBarkod { get; set; }

        // Hesaplanan özellikler
        public decimal Kar => AlisFiyati.HasValue ? (BirimFiyat - AlisFiyati.Value) * Miktar : 0;
    }
}
