using System;

namespace StokTakip.DataAccess.Models
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string BarkodNo { get; set; }
        public string UrunAdi { get; set; }
        public int KategoriID { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Renk { get; set; }
        public string Beden { get; set; }
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public int StokMiktari { get; set; }
        public int MinimumStok { get; set; }
        public string Aciklama { get; set; }
        public string ResimUrl { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public bool Aktif { get; set; }

        // Navigation Properties (JOIN için)
        public string KategoriAdi { get; set; }
        public string KategoriRenk { get; set; }
        
        // Hesaplanan özellikler
        public decimal KarMarji => SatisFiyati - AlisFiyati;
        public decimal KarOrani => AlisFiyati > 0 ? ((SatisFiyati - AlisFiyati) / AlisFiyati) * 100 : 0;
        public bool KritikStok => StokMiktari <= MinimumStok;
    }
}
