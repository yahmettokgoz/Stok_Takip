using System;

namespace StokTakip.DataAccess.Models
{
    public class Satis
    {
        public int SatisID { get; set; }
        public string SatisNo { get; set; }
        public int SaticPersonelID { get; set; }
        public string MusteriAdSoyad { get; set; }
        public string MusteriTelefon { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal OdenenTutar { get; set; }
        public decimal KalanTutar { get; set; }
        public string OdemeYontemi { get; set; }
        public decimal IndirimOrani { get; set; }
        public decimal IndirimTutari { get; set; }
        public decimal KDVOrani { get; set; }
        public decimal? NetKar { get; set; }
        public string Durum { get; set; }
        public DateTime SatisTarihi { get; set; }

        // Navigation Properties
        public string PersonelAdi { get; set; }
    }
}
