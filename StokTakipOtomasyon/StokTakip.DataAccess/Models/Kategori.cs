using System;

namespace StokTakip.DataAccess.Models
{
    public class Kategori
    {
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }
        public int? UstKategoriID { get; set; }
        public string Ikon { get; set; }
        public string Renk { get; set; }
        public bool Aktif { get; set; }
    }
}
