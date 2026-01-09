using System;

namespace StokTakip.DataAccess.Models
{
    public class StokHareket
    {
        public int HareketID { get; set; }
        public int UrunID { get; set; }
        public string HareketTipi { get; set; }
        public int Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? ToplamTutar { get; set; }
        public int OncekiStok { get; set; }
        public int YeniStok { get; set; }
        public int PersonelID { get; set; }
        public string Aciklama { get; set; }
        public DateTime HareketTarihi { get; set; }

        // Navigation Properties
        public string UrunAdi { get; set; }
        public string PersonelAdi { get; set; }
    }
}
