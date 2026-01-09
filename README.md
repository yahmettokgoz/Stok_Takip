# 🏪 Stok Takip Otomasyonu

Modern UI ile geliştirilmiş, 3 katmanlı mimari yapıya sahip Stok Takip Otomasyonu projesi.

## 🎯 Proje Yapısı

```
StokTakipOtomasyon/
├── StokTakip.DataAccess/    # Data Access Layer (DAL)
│   ├── Context/              # SqlHelper
│   ├── Models/               # 7 Entity Model
│   └── Repositories/         # 5 Repository
├── StokTakip.Business/       # Business Logic Layer (BLL)
│   ├── Services/             # 4 Service
│   ├── Validators/           # Validation Logic
│   └── Helpers/              # SecurityHelper
└── StokTakip.UI/             # Presentation Layer (UI)
    ├── Forms/                # Login, AnaSayfa
    ├── Components/           # ModernCard, ModernButton, DashboardWidget
    └── Helpers/              # ThemeColors, SessionManager, MessageHelper
```

## 🛠️ Teknolojiler

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms (Modern Custom Controls)
- **Database:** MS SQL Server
- **Architecture:** 3-Tier Architecture (N-Tier)
- **Security:** SHA-256 Password Hashing

## ✨ Özellikler

### 🔐 Güvenlik & Yetkilendirme
- SHA-256 şifre hash'leme
- Kullanıcı rolleri (Admin, Personel, Satıcı)
- Session yönetimi

### 📦 Ürün Yönetimi
- Ürün CRUD işlemleri
- Kategori bazlı yönetim (Ana/Alt kategori)
- Barkod desteği
- Kritik stok uyarıları
- Ürün arama ve filtreleme

### 📊 Stok Yönetimi
- Stok giriş/çıkış kayıtları
- Stok hareketi geçmişi
- Stok iade işlemleri
- Stok sayım özelliği
- Gerçek zamanlı stok durumu

### 🛒 Satış Modülü
- Hızlı satış işlemleri
- Otomatik satış numarası (FS2025-00001)
- İndirim ve KDV hesaplaması
- Kar/Zarar analizi
- Transaction güvenliği (stok + satış senkronizasyonu)

### 📈 Raporlama & Dashboard
- 4 Widget'lı modern dashboard
- Günlük/Aylık satış raporları
- Toplam ciro ve kar göstergeleri
- Kritik stok bildirimleri
- Grafik ve istatistikler

### 🎨 Modern UI
- **Dark Theme** (Cyberpunk/Slate renk paleti)
- **Custom Components:**
  - ModernCard (Hover efekti, rounded corners)
  - ModernButton (Gradient, animasyonlu)
  - DashboardWidget (İstatistik kartları)
- Responsive layout
- Smooth animations

## 🗄️ Veritabanı Şeması

**8 Ana Tablo:**
- `Kullanicilar` - Kullanıcı yönetimi
- `Kategoriler` - Ürün kategorileri
- `Urunler` - Ürün bilgileri
- `StokHareketleri` - Stok giriş/çıkış kayıtları
- `Satislar` - Satış master
- `SatisDetaylari` - Satış detayları
- `Tedarikciler` - Tedarikçi bilgileri
- `UrunTedarikci` - Ürün-Tedarikçi ilişkisi

## 🚀 Kurulum

### 1. Veritabanı Kurulumu
```sql
-- SQL Server Management Studio'da çalıştırın:
Database\CreateDatabase.sql
```

### 2. Bağlantı Ayarları
`StokTakip.UI\App.config` dosyasında connection string'i güncelleyin:
```xml
<connectionStrings>
  <add name="StokTakipDB" 
       connectionString="Server=SUNUCU_ADI\SQLEXPRESS;Database=EnGuncelStokTakip;Integrated Security=true;" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### 3. Projeyi Derleyin
- Visual Studio'da `StokTakipOtomasyon.sln` dosyasını açın
- Build → Rebuild Solution
- F5 ile çalıştırın

## 👤 Test Kullanıcıları

| Kullanıcı Adı | Şifre | Rol |
|---------------|-------|-----|
| admin | admin123 | Admin |
| personel1 | password | Personel |

## 📸 Ekran Görüntüleri

### Login Ekranı
- Modern split-screen tasarım
- Gradient butonlar
- Hover efektleri

### Dashboard
- 4 İstatistik widget'ı
- Sidebar menü sistemi
- Gerçek zamanlı veriler

## 🎯 Mimari Tasarım

### Data Access Layer (DAL)
**Sorumluluklar:**
- Veritabanı bağlantısı
- CRUD operasyonları
- Parameterized queries (SQL Injection koruması)

**Bileşenler:**
- `SqlHelper` - Database helper
- `Repositories` - Her entity için CRUD
- `Models` - Entity sınıfları

### Business Logic Layer (BLL)
**Sorumluluklar:**
- İş kuralları ve validasyon
- Karmaşık iş mantığı
- Transaction yönetimi

**Bileşenler:**
- `AuthService` - Giriş, şifreleme
- `UrunService` - Ürün iş mantığı
- `StokService` - Stok hareketleri
- `SatisService` - Satış ve kar hesaplama

### Presentation Layer (UI)
**Sorumluluklar:**
- Kullanıcı arayüzü
- Input validation
- Kullanıcı deneyimi

**Bileşenler:**
- `Forms` - Formlar (Login, AnaSayfa, vb.)
- `Components` - Custom kontroller
- `Helpers` - UI yardımcıları

## 🔧 Gelişmiş Özellikler

### Transaction Güvenliği
Satış işleminde:
1. Satış kaydı oluşturulur
2. Satış detayları eklenir
3. Stok miktarları güncellenir
4. Stok hareketi kaydedilir

**Hata durumunda:** Tüm işlemler geri alınır (Rollback)

### Kar/Zarar Hesaplama
```csharp
NetKar = (SatisFiyati - AlisFiyati) * Miktar
```

### Kritik Stok Kontrolü
```csharp
KritikStok = StokMiktari <= MinimumStok
```

## 📊 Performans Optimizasyonları

- **Indexler:** Sık kullanılan alanlarda
- **Parameterized Queries:** SQL Injection koruması
- **Double Buffering:** UI flicker önleme
- **Lazy Loading:** İhtiyaç anında veri yükleme

## 🎨 Renk Paleti

```csharp
// Dark Theme
PrimaryDark   = #0F172A (Slate-900)
SecondaryDark = #1E293B (Slate-800)

// Accent Colors
AccentBlue    = #3B82F6 (Blue-500)
AccentGreen   = #22C55E (Green-500)
AccentOrange  = #F97316 (Orange-500)
AccentRed     = #EF4444 (Red-500)

// Text
TextPrimary   = #F8FAFC (Slate-50)
TextSecondary = #94A3B8 (Slate-400)
```

## 🐛 Hata Yönetimi

- Try-catch blokları
- Kullanıcı dostu hata mesajları
- Exception logging hazır

## 📝 TODO - Gelecek Özellikler

- [ ] Ürünler sayfası (Responsive card layout)
- [ ] Satış formu (POS benzeri arayüz)
- [ ] Stok hareketleri sayfası
- [ ] Detaylı raporlar ve grafikler
- [ ] Excel export/import
- [ ] Barkod okuyucu entegrasyonu
- [ ] Fiş/Fatura yazdırma
- [ ] Kullanıcı yönetimi sayfası
- [ ] Kategori yönetimi sayfası
- [ ] Tedarikçi yönetimi

## 👨‍💻 Geliştirici Notları

### Yeni Form Eklemek İçin:
1. `Forms` klasörüne `.cs`, `.Designer.cs`, `.resx` ekle
2. `SessionManager.AktifKullanici` ile kullanıcı bilgisine eriş
3. `MessageHelper` ile kullanıcı bildirimleri göster
4. `ThemeColors` ile tutarlı renk paleti kullan

### Yeni Repository Eklemek İçin:
1. `DataAccess/Models` klasörüne model ekle
2. `DataAccess/Repositories` klasörüne repository ekle
3. `Business/Services` klasörüne service ekle
4. İş kurallarını service katmanına yaz

---

**Geliştirme Tarihi:** Aralık 2025  
**Durum:** ✅ Temel yapı tamamlandı, ek özellikler geliştirme aşamasında

**🚀 Proje Hazır! Login yapıp test edebilirsiniz.**
