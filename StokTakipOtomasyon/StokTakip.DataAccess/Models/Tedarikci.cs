using System;

namespace StokTakip.DataAccess.Models
{
    public class Tedarikci
    {
        public int TedarikciID { get; set; }
        public string FirmaAdi { get; set; }
        public string YetkiliKisi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string VergiNo { get; set; }
        public bool Aktif { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
