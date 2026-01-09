using System;
using System.Collections.Generic;
using StokTakip.DataAccess.Models;
using StokTakip.DataAccess.Repositories;

namespace StokTakip.Business.Services
{
    public class StokService
    {
        private readonly StokHareketRepository _stokHareketRepo;
        private readonly UrunRepository _urunRepo;

        public StokService()
        {
            _stokHareketRepo = new StokHareketRepository();
            _urunRepo = new UrunRepository();
        }

        /// <summary>
        /// Stok girişi yapar
        /// </summary>
        public bool StokGirisi(int urunId, int miktar, decimal birimFiyat, int personelId, string aciklama = null)
        {
            if (miktar <= 0)
                throw new Exception("Miktar sıfırdan büyük olmalıdır!");

            if (birimFiyat < 0)
                throw new Exception("Birim fiyat negatif olamaz!");

            // Mevcut ürünü al
            Urun urun = _urunRepo.GetById(urunId);
            if (urun == null)
                throw new Exception("Ürün bulunamadı!");

            // Stok hareketi oluştur
            StokHareket hareket = new StokHareket
            {
                UrunID = urunId,
                HareketTipi = "Giris",
                Miktar = miktar,
                BirimFiyat = birimFiyat,
                ToplamTutar = miktar * birimFiyat,
                OncekiStok = urun.StokMiktari,
                YeniStok = urun.StokMiktari + miktar,
                PersonelID = personelId,
                Aciklama = aciklama ?? "Stok girişi"
            };

            return _stokHareketRepo.Insert(hareket);
        }

        /// <summary>
        /// Stok çıkışı yapar (manuel)
        /// </summary>
        public bool StokCikisi(int urunId, int miktar, int personelId, string aciklama = null)
        {
            if (miktar <= 0)
                throw new Exception("Miktar sıfırdan büyük olmalıdır!");

            // Mevcut ürünü al
            Urun urun = _urunRepo.GetById(urunId);
            if (urun == null)
                throw new Exception("Ürün bulunamadı!");

            if (urun.StokMiktari < miktar)
                throw new Exception("Yetersiz stok! Mevcut stok: " + urun.StokMiktari);

            // Stok hareketi oluştur
            StokHareket hareket = new StokHareket
            {
                UrunID = urunId,
                HareketTipi = "Cikis",
                Miktar = miktar,
                BirimFiyat = null,
                ToplamTutar = null,
                OncekiStok = urun.StokMiktari,
                YeniStok = urun.StokMiktari - miktar,
                PersonelID = personelId,
                Aciklama = aciklama ?? "Manuel stok çıkışı"
            };

            return _stokHareketRepo.Insert(hareket);
        }

        /// <summary>
        /// Stok iade işlemi yapar
        /// </summary>
        public bool StokIade(int urunId, int miktar, int personelId, string aciklama = null)
        {
            if (miktar <= 0)
                throw new Exception("Miktar sıfırdan büyük olmalıdır!");

            // Mevcut ürünü al
            Urun urun = _urunRepo.GetById(urunId);
            if (urun == null)
                throw new Exception("Ürün bulunamadı!");

            // Stok hareketi oluştur
            StokHareket hareket = new StokHareket
            {
                UrunID = urunId,
                HareketTipi = "Iade",
                Miktar = miktar,
                BirimFiyat = null,
                ToplamTutar = null,
                OncekiStok = urun.StokMiktari,
                YeniStok = urun.StokMiktari + miktar,
                PersonelID = personelId,
                Aciklama = aciklama ?? "Stok iadesi"
            };

            return _stokHareketRepo.Insert(hareket);
        }

        /// <summary>
        /// Stok sayımı yapar
        /// </summary>
        public bool StokSayim(int urunId, int sayilanMiktar, int personelId, string aciklama = null)
        {
            if (sayilanMiktar < 0)
                throw new Exception("Sayılan miktar negatif olamaz!");

            // Mevcut ürünü al
            Urun urun = _urunRepo.GetById(urunId);
            if (urun == null)
                throw new Exception("Ürün bulunamadı!");

            int fark = sayilanMiktar - urun.StokMiktari;

            // Stok hareketi oluştur
            StokHareket hareket = new StokHareket
            {
                UrunID = urunId,
                HareketTipi = "Sayim",
                Miktar = Math.Abs(fark),
                BirimFiyat = null,
                ToplamTutar = null,
                OncekiStok = urun.StokMiktari,
                YeniStok = sayilanMiktar,
                PersonelID = personelId,
                Aciklama = aciklama ?? $"Stok sayımı (Fark: {fark})"
            };

            return _stokHareketRepo.Insert(hareket);
        }

        /// <summary>
        /// Tüm stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetAll()
        {
            return _stokHareketRepo.GetAll();
        }

        /// <summary>
        /// Ürüne göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetByUrun(int urunId)
        {
            return _stokHareketRepo.GetByUrun(urunId);
        }

        /// <summary>
        /// Tarih aralığına göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetByDateRange(DateTime baslangic, DateTime bitis)
        {
            return _stokHareketRepo.GetByDateRange(baslangic, bitis);
        }

        /// <summary>
        /// Filtrelere göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetHareketler(DateTime baslangic, DateTime bitis, int? urunID = null, string hareketTipi = null)
        {
            return _stokHareketRepo.GetHareketlerFiltered(baslangic, bitis, urunID, hareketTipi);
        }

        /// <summary>
        /// Yeni stok hareketi ekler
        /// </summary>
        public bool AddHareket(StokHareket hareket)
        {
            // Mevcut ürünü al
            Urun urun = _urunRepo.GetById(hareket.UrunID);
            if (urun == null)
                throw new Exception("Ürün bulunamadı!");

            // Önceki ve yeni stok hesapla
            hareket.OncekiStok = urun.StokMiktari;

            if (hareket.HareketTipi == "Stok Girişi" || hareket.HareketTipi == "İade")
            {
                hareket.YeniStok = urun.StokMiktari + hareket.Miktar;
            }
            else // Fire/Kayıp
            {
                if (urun.StokMiktari < hareket.Miktar)
                    throw new Exception("Yetersiz stok! Mevcut stok: " + urun.StokMiktari);
                
                hareket.YeniStok = urun.StokMiktari - hareket.Miktar;
            }

            hareket.ToplamTutar = hareket.Miktar * (hareket.BirimFiyat ?? 0);
            hareket.HareketTarihi = DateTime.Now;
            hareket.PersonelID = hareket.PersonelID; // UI'dan gelecek

            return _stokHareketRepo.Insert(hareket);
        }
    }
}
