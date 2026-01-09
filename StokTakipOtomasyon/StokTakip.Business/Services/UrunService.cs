using System;
using System.Collections.Generic;
using StokTakip.DataAccess.Models;
using StokTakip.DataAccess.Repositories;

namespace StokTakip.Business.Services
{
    public class UrunService
    {
        private readonly UrunRepository _urunRepo;
        private readonly KategoriRepository _kategoriRepo;

        public UrunService()
        {
            _urunRepo = new UrunRepository();
            _kategoriRepo = new KategoriRepository();
        }

        /// <summary>
        /// Tüm ürünleri getirir
        /// </summary>
        public List<Urun> GetAll()
        {
            return _urunRepo.GetAll();
        }

        /// <summary>
        /// Kategoriye göre ürünleri getirir
        /// </summary>
        public List<Urun> GetByCategory(int kategoriId)
        {
            return _urunRepo.GetByCategory(kategoriId);
        }

        /// <summary>
        /// Kritik stok seviyesindeki ürünleri getirir
        /// </summary>
        public List<Urun> GetCriticalStock()
        {
            return _urunRepo.GetCriticalStock();
        }

        /// <summary>
        /// Ürün arar
        /// </summary>
        public List<Urun> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAll();

            return _urunRepo.Search(searchText);
        }

        /// <summary>
        /// ID'ye göre ürün getirir
        /// </summary>
        public Urun GetById(int id)
        {
            return _urunRepo.GetById(id);
        }

        /// <summary>
        /// Barkod numarasına göre ürün getirir
        /// </summary>
        public Urun GetByBarkod(string barkodNo)
        {
            if (string.IsNullOrWhiteSpace(barkodNo))
                return null;

            return _urunRepo.GetByBarkod(barkodNo);
        }

        /// <summary>
        /// Yeni ürün ekler
        /// </summary>
        public int Insert(Urun urun)
        {
            // Validasyon
            ValidateUrun(urun);

            // Barkod kontrolü
            if (!string.IsNullOrEmpty(urun.BarkodNo))
            {
                Urun mevcutUrun = _urunRepo.GetByBarkod(urun.BarkodNo);
                if (mevcutUrun != null)
                    throw new Exception("Bu barkod numarası zaten kayıtlı!");
            }

            // Kategori kontrolü
            Kategori kategori = _kategoriRepo.GetById(urun.KategoriID);
            if (kategori == null)
                throw new Exception("Seçilen kategori bulunamadı!");

            urun.Aktif = true;
            return _urunRepo.Insert(urun);
        }

        /// <summary>
        /// Ürün günceller
        /// </summary>
        public bool Update(Urun urun)
        {
            // Validasyon
            ValidateUrun(urun);

            // Mevcut ürünü kontrol et
            Urun mevcutUrun = _urunRepo.GetById(urun.UrunID);
            if (mevcutUrun == null)
                throw new Exception("Güncellenecek ürün bulunamadı!");

            // Barkod kontrolü (başka bir ürüne ait değilse)
            if (!string.IsNullOrEmpty(urun.BarkodNo))
            {
                Urun barkodluUrun = _urunRepo.GetByBarkod(urun.BarkodNo);
                if (barkodluUrun != null && barkodluUrun.UrunID != urun.UrunID)
                    throw new Exception("Bu barkod numarası başka bir ürüne ait!");
            }

            // Kategori kontrolü
            Kategori kategori = _kategoriRepo.GetById(urun.KategoriID);
            if (kategori == null)
                throw new Exception("Seçilen kategori bulunamadı!");

            return _urunRepo.Update(urun);
        }

        /// <summary>
        /// Ürün siler
        /// </summary>
        public bool Delete(int id)
        {
            Urun urun = _urunRepo.GetById(id);
            if (urun == null)
                throw new Exception("Silinecek ürün bulunamadı!");

            return _urunRepo.Delete(id);
        }

        /// <summary>
        /// Toplam ürün sayısını döner
        /// </summary>
        public int GetTotalCount()
        {
            return _urunRepo.GetTotalCount();
        }

        /// <summary>
        /// Toplam stok değerini hesaplar
        /// </summary>
        public decimal GetTotalStockValue()
        {
            return _urunRepo.GetTotalStockValue();
        }

        /// <summary>
        /// Ürün validasyonu
        /// </summary>
        private void ValidateUrun(Urun urun)
        {
            if (string.IsNullOrWhiteSpace(urun.UrunAdi))
                throw new Exception("Ürün adı boş olamaz!");

            if (urun.UrunAdi.Length < 2)
                throw new Exception("Ürün adı en az 2 karakter olmalıdır!");

            if (urun.KategoriID <= 0)
                throw new Exception("Kategori seçilmelidir!");

            if (urun.AlisFiyati < 0)
                throw new Exception("Alış fiyatı negatif olamaz!");

            if (urun.SatisFiyati < 0)
                throw new Exception("Satış fiyatı negatif olamaz!");

            if (urun.SatisFiyati < urun.AlisFiyati)
                throw new Exception("Satış fiyatı alış fiyatından düşük olamaz!");

            if (urun.StokMiktari < 0)
                throw new Exception("Stok miktarı negatif olamaz!");

            if (urun.MinimumStok < 0)
                throw new Exception("Minimum stok negatif olamaz!");
        }
    }
}
