
USE master;
GO

-- Veritabanı varsa sil, yoksa oluştur
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EnGuncelStokTakip')
BEGIN
    ALTER DATABASE EnGuncelStokTakip SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EnGuncelStokTakip;
END
GO

CREATE DATABASE EnGuncelStokTakip;
GO

USE EnGuncelStokTakip;
GO

-- ============================================================
-- 1. KULLANICILAR TABLOSU
-- ============================================================
CREATE TABLE Kullanicilar (
    KullaniciID INT PRIMARY KEY IDENTITY(1,1),
    KullaniciAdi NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Sifre NVARCHAR(256) NOT NULL,  -- SHA-256 encrypted
    AdSoyad NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Admin', 'Personel', 'Satici')),
    Telefon NVARCHAR(15),
    Aktif BIT DEFAULT 1,
    KayitTarihi DATETIME DEFAULT GETDATE(),
    SonGirisTarihi DATETIME NULL
);
GO

-- ============================================================
-- 2. KATEGORİLER TABLOSU
-- ============================================================
CREATE TABLE Kategoriler (
    KategoriID INT PRIMARY KEY IDENTITY(1,1),
    KategoriAdi NVARCHAR(50) NOT NULL UNIQUE,
    Aciklama NVARCHAR(200),
    UstKategoriID INT NULL,
    Ikon NVARCHAR(50),  -- Font Awesome ikon adı (fa-laptop, fa-phone, vb.)
    Renk NVARCHAR(7) DEFAULT '#3B82F6',  -- Hex renk kodu
    Aktif BIT DEFAULT 1,
    CONSTRAINT FK_Kategori_UstKategori FOREIGN KEY (UstKategoriID) 
        REFERENCES Kategoriler(KategoriID)
);
GO

-- ============================================================
-- 3. ÜRÜNLER TABLOSU
-- ============================================================
CREATE TABLE Urunler (
    UrunID INT PRIMARY KEY IDENTITY(1,1),
    BarkodNo NVARCHAR(50) UNIQUE,
    UrunAdi NVARCHAR(100) NOT NULL,
    KategoriID INT NOT NULL,
    Marka NVARCHAR(50),
    Model NVARCHAR(50),
    Renk NVARCHAR(30),
    Beden NVARCHAR(10),
    AlisFiyati DECIMAL(18,2) NOT NULL CHECK (AlisFiyati >= 0),
    SatisFiyati DECIMAL(18,2) NOT NULL CHECK (SatisFiyati >= 0),
    StokMiktari INT NOT NULL DEFAULT 0 CHECK (StokMiktari >= 0),
    MinimumStok INT DEFAULT 10,
    Aciklama NVARCHAR(500),
    ResimUrl NVARCHAR(200),
    KayitTarihi DATETIME DEFAULT GETDATE(),
    GuncellemeTarihi DATETIME NULL,
    Aktif BIT DEFAULT 1,
    CONSTRAINT FK_Urun_Kategori FOREIGN KEY (KategoriID) 
        REFERENCES Kategoriler(KategoriID)
);
GO

-- ============================================================
-- 4. STOK HAREKETLERİ TABLOSU
-- ============================================================
CREATE TABLE StokHareketleri (
    HareketID INT PRIMARY KEY IDENTITY(1,1),
    UrunID INT NOT NULL,
    HareketTipi NVARCHAR(20) NOT NULL CHECK (HareketTipi IN ('Giris', 'Cikis', 'Iade', 'Sayim')),
    Miktar INT NOT NULL,
    BirimFiyat DECIMAL(18,2),
    ToplamTutar DECIMAL(18,2),
    OncekiStok INT NOT NULL,
    YeniStok INT NOT NULL,
    PersonelID INT NOT NULL,
    Aciklama NVARCHAR(200),
    HareketTarihi DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_StokHareket_Urun FOREIGN KEY (UrunID) 
        REFERENCES Urunler(UrunID),
    CONSTRAINT FK_StokHareket_Personel FOREIGN KEY (PersonelID) 
        REFERENCES Kullanicilar(KullaniciID)
);
GO

-- ============================================================
-- 5. SATIŞLAR TABLOSU
-- ============================================================
CREATE TABLE Satislar (
    SatisID INT PRIMARY KEY IDENTITY(1,1),
    SatisNo NVARCHAR(50) UNIQUE NOT NULL,
    SaticPersonelID INT NOT NULL,
    MusteriAdSoyad NVARCHAR(100),
    MusteriTelefon NVARCHAR(15),
    ToplamTutar DECIMAL(18,2) NOT NULL CHECK (ToplamTutar >= 0),
    OdenenTutar DECIMAL(18,2) NOT NULL CHECK (OdenenTutar >= 0),
    KalanTutar DECIMAL(18,2) DEFAULT 0 CHECK (KalanTutar >= 0),
    OdemeYontemi NVARCHAR(30) CHECK (OdemeYontemi IN ('Nakit', 'KrediKarti', 'Havale', 'Diger')),
    IndirimOrani DECIMAL(5,2) DEFAULT 0 CHECK (IndirimOrani BETWEEN 0 AND 100),
    IndirimTutari DECIMAL(18,2) DEFAULT 0 CHECK (IndirimTutari >= 0),
    KDVOrani DECIMAL(5,2) DEFAULT 20 CHECK (KDVOrani >= 0),
    NetKar DECIMAL(18,2),
    Durum NVARCHAR(20) DEFAULT 'Tamamlandi' CHECK (Durum IN ('Tamamlandi', 'Beklemede', 'Iptal')),
    SatisTarihi DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Satis_Personel FOREIGN KEY (SaticPersonelID) 
        REFERENCES Kullanicilar(KullaniciID)
);
GO

-- ============================================================
-- 6. SATIŞ DETAYLARI TABLOSU
-- ============================================================
CREATE TABLE SatisDetaylari (
    DetayID INT PRIMARY KEY IDENTITY(1,1),
    SatisID INT NOT NULL,
    UrunID INT NOT NULL,
    Miktar INT NOT NULL CHECK (Miktar > 0),
    BirimFiyat DECIMAL(18,2) NOT NULL CHECK (BirimFiyat >= 0),
    ToplamFiyat DECIMAL(18,2) NOT NULL CHECK (ToplamFiyat >= 0),
    AlisFiyati DECIMAL(18,2),
    CONSTRAINT FK_SatisDetay_Satis FOREIGN KEY (SatisID) 
        REFERENCES Satislar(SatisID) ON DELETE CASCADE,
    CONSTRAINT FK_SatisDetay_Urun FOREIGN KEY (UrunID) 
        REFERENCES Urunler(UrunID)
);
GO

-- ============================================================
-- 7. TEDARİKÇİLER TABLOSU
-- ============================================================
CREATE TABLE Tedarikciler (
    TedarikciID INT PRIMARY KEY IDENTITY(1,1),
    FirmaAdi NVARCHAR(100) NOT NULL,
    YetkiliKisi NVARCHAR(100),
    Telefon NVARCHAR(15),
    Email NVARCHAR(100),
    Adres NVARCHAR(300),
    VergiNo NVARCHAR(20),
    Aktif BIT DEFAULT 1,
    KayitTarihi DATETIME DEFAULT GETDATE()
);
GO

-- ============================================================
-- 8. ÜRÜN-TEDARİKÇİ İLİŞKİSİ
-- ============================================================
CREATE TABLE UrunTedarikci (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UrunID INT NOT NULL,
    TedarikciID INT NOT NULL,
    TedarikSuresi INT,
    MinimumSiparisMiktari INT,
    CONSTRAINT FK_UrunTedarikci_Urun FOREIGN KEY (UrunID) 
        REFERENCES Urunler(UrunID),
    CONSTRAINT FK_UrunTedarikci_Tedarikci FOREIGN KEY (TedarikciID) 
        REFERENCES Tedarikciler(TedarikciID)
);
GO

-- ============================================================
-- INDEXLER (Performans için)
-- ============================================================

-- Kullanıcılar
CREATE INDEX IX_Kullanicilar_Email ON Kullanicilar(Email);
CREATE INDEX IX_Kullanicilar_KullaniciAdi ON Kullanicilar(KullaniciAdi);

-- Ürünler
CREATE INDEX IX_Urunler_BarkodNo ON Urunler(BarkodNo);
CREATE INDEX IX_Urunler_KategoriID ON Urunler(KategoriID);
CREATE INDEX IX_Urunler_UrunAdi ON Urunler(UrunAdi);
CREATE INDEX IX_Urunler_Aktif ON Urunler(Aktif);

-- Satışlar
CREATE INDEX IX_Satislar_SatisNo ON Satislar(SatisNo);
CREATE INDEX IX_Satislar_SatisTarihi ON Satislar(SatisTarihi);
CREATE INDEX IX_Satislar_Durum ON Satislar(Durum);

-- Stok Hareketleri
CREATE INDEX IX_StokHareketleri_UrunID ON StokHareketleri(UrunID);
CREATE INDEX IX_StokHareketleri_HareketTarihi ON StokHareketleri(HareketTarihi);

GO

-- ============================================================
-- ÖRNEK VERİLER (Test için)
-- ============================================================

-- 1. Admin Kullanici (Sifre: admin123 -> SHA256 hash'i)
INSERT INTO Kullanicilar (KullaniciAdi, Email, Sifre, AdSoyad, Rol, Telefon)
VALUES 
('admin', 'admin@stoktakip.com', '240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 'Admin Kullanici', 'Admin', '0555 111 22 33'),
('personel1', 'personel@stoktakip.com', '5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8', 'Ahmet Yilmaz', 'Personel', '0555 222 33 44');
GO

-- 2. Kategoriler
INSERT INTO Kategoriler (KategoriAdi, Aciklama, Ikon, Renk, UstKategoriID)
VALUES 
('Elektronik', 'Elektronik urunler', 'fa-laptop', '#3B82F6', NULL),
('Giyim', 'Giyim urunleri', 'fa-shirt', '#10B981', NULL),
('Ev & Yasam', 'Ev ve yasam urunleri', 'fa-home', '#F59E0B', NULL);

-- Alt kategoriler
INSERT INTO Kategoriler (KategoriAdi, Aciklama, Ikon, Renk, UstKategoriID)
VALUES 
('Telefon', 'Akilli telefonlar', 'fa-mobile', '#8B5CF6', 1),
('Bilgisayar', 'Dizustu ve masaustu bilgisayarlar', 'fa-desktop', '#06B6D4', 1),
('Erkek Giyim', 'Erkek giyim urunleri', 'fa-male', '#14B8A6', 2),
('Kadin Giyim', 'Kadin giyim urunleri', 'fa-female', '#EC4899', 2);
GO

-- 3. Ornek Urunler
INSERT INTO Urunler (BarkodNo, UrunAdi, KategoriID, Marka, Model, Renk, AlisFiyati, SatisFiyati, StokMiktari, MinimumStok)
VALUES 
('8691234567890', 'iPhone 15 Pro', 4, 'Apple', 'iPhone 15 Pro', 'Siyah', 45000.00, 52000.00, 8, 5),
('8691234567891', 'Samsung Galaxy S24', 4, 'Samsung', 'Galaxy S24', 'Beyaz', 38000.00, 44000.00, 12, 5),
('8691234567892', 'MacBook Air M2', 5, 'Apple', 'MacBook Air', 'Gumus', 65000.00, 75000.00, 5, 3),
('8691234567893', 'Dell XPS 15', 5, 'Dell', 'XPS 15', 'Siyah', 55000.00, 63000.00, 3, 2),
('8691234567894', 'Erkek Kot Pantolon', 6, 'Levi''s', '501', 'Mavi', 450.00, 650.00, 25, 10),
('8691234567895', 'Kadin Elbise', 7, 'Zara', 'Summer Collection', 'Kirmizi', 380.00, 580.00, 15, 8);
GO

-- 4. Tedarikciler
INSERT INTO Tedarikciler (FirmaAdi, YetkiliKisi, Telefon, Email, Adres, VergiNo)
VALUES 
('Teknoloji A.S.', 'Mehmet Demir', '0212 555 11 22', 'info@teknoloji.com', 'Istanbul', '1234567890'),
('Giyim Ltd.', 'Ayse Kaya', '0216 444 33 55', 'info@giyim.com', 'Ankara', '0987654321');
GO

-- 5. Stok Hareketleri (Ilk stok girisi)
INSERT INTO StokHareketleri (UrunID, HareketTipi, Miktar, BirimFiyat, ToplamTutar, OncekiStok, YeniStok, PersonelID, Aciklama)
VALUES 
(1, 'Giris', 8, 45000.00, 360000.00, 0, 8, 1, 'Ilk stok girisi'),
(2, 'Giris', 12, 38000.00, 456000.00, 0, 12, 1, 'Ilk stok girisi'),
(3, 'Giris', 5, 65000.00, 325000.00, 0, 5, 1, 'Ilk stok girisi');
GO

PRINT 'Veritabani basariyla olusturuldu!';
PRINT 'Ornek veriler eklendi.';
PRINT '';
PRINT 'Test Kullanicilari:';
PRINT '   Admin     -> Kullanici: admin      Sifre: admin123';
PRINT '   Personel  -> Kullanici: personel1  Sifre: password';
PRINT '';
GO
