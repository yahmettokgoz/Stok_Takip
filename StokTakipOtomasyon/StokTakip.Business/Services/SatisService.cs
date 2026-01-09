using System;
using System.Collections.Generic;
using System.Data;
using StokTakip.DataAccess.Models;
using StokTakip.DataAccess.Repositories;

namespace StokTakip.Business.Services
{
    public class SatisService
    {
        private readonly SatisRepository _satisRepo;
        private readonly UrunRepository _urunRepo;

        public SatisService()
        {
            _satisRepo = new SatisRepository();
            _urunRepo = new UrunRepository();
        }

        /// <summary>
        /// Yeni satış kaydı oluşturur
        /// </summary>
        public int CreateSatis(Satis satis, List<SatisDetay> detaylar, int personelId)
        {
            // Validasyonlar
            if (detaylar == null || detaylar.Count == 0)
                throw new Exception("Satış için en az bir ürün seçilmelidir!");

            if (satis.OdenenTutar < 0)
                throw new Exception("Ödenen tutar negatif olamaz!");

            // Stok kontrolü
            foreach (var detay in detaylar)
            {
                Urun urun = _urunRepo.GetById(detay.UrunID);
                if (urun == null)
                    throw new Exception($"Ürün bulunamadı! (ID: {detay.UrunID})");

                if (urun.StokMiktari < detay.Miktar)
                    throw new Exception($"{urun.UrunAdi} için yetersiz stok! Mevcut: {urun.StokMiktari}, İstenen: {detay.Miktar}");
            }

            // Satış numarası oluştur
            satis.SatisNo = _satisRepo.GenerateSatisNo();
            satis.SaticPersonelID = personelId;

            // Toplam kar hesapla
            decimal toplamKar = 0;
            foreach (var detay in detaylar)
            {
                if (detay.AlisFiyati.HasValue)
                {
                    toplamKar += (detay.BirimFiyat - detay.AlisFiyati.Value) * detay.Miktar;
                }
            }
            satis.NetKar = toplamKar;

            // Kalan tutarı hesapla
            satis.KalanTutar = satis.ToplamTutar - satis.OdenenTutar;

            // Durumu belirle
            if (satis.KalanTutar > 0)
                satis.Durum = "Beklemede";
            else
                satis.Durum = "Tamamlandi";

            // Satışı kaydet
            return _satisRepo.Insert(satis, detaylar, personelId);
        }

        /// <summary>
        /// Tüm satışları getirir
        /// </summary>
        public List<Satis> GetAll()
        {
            return _satisRepo.GetAll();
        }

        /// <summary>
        /// Tarih aralığına göre satışları getirir
        /// </summary>
        public List<Satis> GetByDateRange(DateTime baslangic, DateTime bitis)
        {
            return _satisRepo.GetByDateRange(baslangic, bitis);
        }

        /// <summary>
        /// Bugünkü satışları getirir
        /// </summary>
        public List<Satis> GetToday()
        {
            return _satisRepo.GetToday();
        }

        /// <summary>
        /// Satış detaylarını getirir
        /// </summary>
        public List<SatisDetay> GetDetaylar(int satisId)
        {
            return _satisRepo.GetDetaylar(satisId);
        }

        /// <summary>
        /// Bugünkü toplam satış tutarını döner
        /// </summary>
        public decimal GetTodayTotal()
        {
            return _satisRepo.GetTodayTotal();
        }

        /// <summary>
        /// Bugünkü toplam kar tutarını döner
        /// </summary>
        public decimal GetTodayProfit()
        {
            return _satisRepo.GetTodayProfit();
        }

        /// <summary>
        /// Aylık satış istatistiklerini getirir
        /// </summary>
        public DataTable GetMonthlySalesStats(int year)
        {
            return _satisRepo.GetMonthlySalesStats(year);
        }

        /// <summary>
        /// Satış özeti bilgisi döner
        /// </summary>
        public SatisOzet GetSatisOzet(DateTime? baslangic = null, DateTime? bitis = null)
        {
            DateTime start = baslangic ?? DateTime.Today;
            DateTime end = bitis ?? DateTime.Today.AddDays(1).AddSeconds(-1);

            List<Satis> satislar = _satisRepo.GetByDateRange(start, end);

            decimal toplamCiro = 0;
            decimal toplamKar = 0;
            int satisSayisi = 0;

            foreach (var satis in satislar)
            {
                if (satis.Durum == "Tamamlandi")
                {
                    toplamCiro += satis.ToplamTutar;
                    toplamKar += satis.NetKar ?? 0;
                    satisSayisi++;
                }
            }

            return new SatisOzet
            {
                ToplamCiro = toplamCiro,
                ToplamKar = toplamKar,
                SatisSayisi = satisSayisi,
                OrtalamaFis = satisSayisi > 0 ? toplamCiro / satisSayisi : 0
            };
        }
    }

    /// <summary>
    /// Satış özet bilgileri için model
    /// </summary>
    public class SatisOzet
    {
        public decimal ToplamCiro { get; set; }
        public decimal ToplamKar { get; set; }
        public int SatisSayisi { get; set; }
        public decimal OrtalamaFis { get; set; }
    }
}
