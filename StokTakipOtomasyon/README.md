# 🏪 Stok Takip Otomasyonu

Modern UI, AI Chatbot ve 3 katmanlı mimari yapıya sahip Stok Takip Otomasyonu projesi.

![Login Ekranı](Ekran%20Görüntüleri/login.png)

## 🎯 Proje Yapısı

```
StokTakipOtomasyon/
├── StokTakip.DataAccess/    # Data Access Layer (DAL)
│   ├── Context/              # SqlHelper
│   ├── Models/               # 7 Entity Model
│   └── Repositories/         # 5 Repository
├── StokTakip.Business/       # Business Logic Layer (BLL)
│   ├── Services/             # AIService, AuthService, UrunService, SatisService
│   ├── Validators/           # Validation Logic
│   └── Helpers/              # SecurityHelper
└── StokTakip.UI/             # Presentation Layer (UI)
    ├── Forms/                # Login, AnaSayfa, AI Asistan
    ├── Components/           # ModernCard, ModernButton, DashboardWidget
    └── Helpers/              # ThemeColors, SessionManager, MessageHelper
```

## 🛠️ Teknolojiler

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms (Modern Custom Controls)
- **Database:** MS SQL Server
- **Architecture:** 3-Tier Architecture (N-Tier)
- **Security:** SHA-256 Password Hashing
- **AI:** Groq API (LLaMA 3.3 70B) - Ücretsiz

## ✨ Özellikler

### 🤖 AI Chatbot (Yeni!)
- **Groq API** ile güçlendirilmiş akıllı asistan
- İş analizi ve stratejik öneriler
- Kategori bazlı kar marjı analizi
- Stok durumu sorgulama
- Finansal öngörüler ve raporlama
- Türkçe doğal dil desteği

![AI Asistan](Ekran%20Görüntüleri/ai-asistan.png)

### 🔐 Güvenlik & Yetkilendirme
- SHA-256 şifre hash'leme
- Kullanıcı rolleri (Admin, Personel, Satıcı)
- Session yönetimi

### 📦 Ürün Yönetimi
- Ürün CRUD işlemleri
- Kategori bazlı yönetim (12 kategori: Telefon, Bilgisayar, Tablet, vb.)
- Barkod desteği
- Kritik stok uyarıları
- Ürün arama ve filtreleme
- Kar marjı hesaplama

![Ürünler](Ekran%20Görüntüleri/urunler.png)

![Ürün Düzenle](Ekran%20Görüntüleri/urun-duzenle.png)

### 📊 Stok Yönetimi
- Stok giriş/çıkış kayıtları
- Stok hareketi geçmişi
- Stok iade işlemleri
- Stok sayım özelliği
- Gerçek zamanlı stok durumu

![Stok Hareketleri](Ekran%20Görüntüleri/stok-hareketleri.png)

![Yeni Hareket](Ekran%20Görüntüleri/yeni-hareket.png)

### 🛒 Satış Modülü
- Hızlı satış işlemleri
- Otomatik satış numarası
- İndirim ve KDV hesaplaması
- Kar/Zarar analizi
- Transaction güvenliği (stok + satış senkronizasyonu)

![Satış](Ekran%20Görüntüleri/satis.png)

### 📈 Raporlama & Dashboard
- **5 Widget'lı modern dashboard:**
  1. Toplam Ürün Çeşidi
  2. Toplam Ürün Miktarı (Yeni!)
  3. Stok Değeri
  4. Günlük Satış
  5. Kritik Stok
- Günlük/Aylık satış raporları
- Toplam ciro ve kar göstergeleri
- Kritik stok bildirimleri
- Grafik ve istatistikler

![Dashboard](Ekran%20Görüntüleri/dashboard.png)

![Kritik Stok](Ekran%20Görüntüleri/kritik-stok.png)

### 🎨 Modern UI
- **Dark Theme** (Cyberpunk/Slate renk paleti)
- **Custom Components:**
  - ModernCard (Hover efekti, rounded corners)
  - ModernButton (Gradient, animasyonlu)
  - DashboardWidget (İstatistik kartları)
- Responsive layout
- Smooth animations

![Ayarlar](Ekran%20Görüntüleri/ayarlar.png)

## 🗄️ Veritabanı Şeması

**7 Ana Tablo:**
- `Kullanicilar` - Kullanıcı yönetimi
- `Kategoriler` - 12 ürün kategorisi
- `Tedarikciler` - Tedarikçi bilgileri
- `Urunler` - Ürün bilgileri (Kategori ve Tedarikçi ilişkili)
- `StokHareketleri` - Stok giriş/çıkış/iade/sayım kayıtları
- `Satislar` - Satış master kayıtları
- `SatisDetaylari` - Satış detay kayıtları

**ER Diyagram:** `ER_Diagram.puml` dosyasında PlantUML formatında mevcuttur.

## 🚀 Kurulum

### 1. Veritabanı Kurulumu
```sql
-- SQL Server Management Studio'da çalıştırın:
1. Database\CreateDatabase.sql dosyasını açın
2. Tüm SQL komutlarını çalıştırın
3. Veritabanı adı: EnGuncelStokTakip
```

### 2. Bağlantı Ayarları
`App.config.example` dosyasını `App.config` olarak kopyalayın ve güncelleyin:
```xml
<connectionStrings>
  <add name="StokTakipDB" 
       connectionString="Server=YOUR_SERVER\SQLEXPRESS;Database=EnGuncelStokTakip;Integrated Security=true;" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### 3. AI Chatbot Kurulumu (İsteğe Bağlı)
1. [Groq Console](https://console.groq.com/keys) adresinden ücretsiz API key alın
2. `App.config` dosyasına ekleyin:
```xml
<appSettings>
  <add key="Groq_ApiKey" value="YOUR_GROQ_API_KEY_HERE" />
</appSettings>
```

### 4. Projeyi Derleyin
- Visual Studio'da `StokTakipOtomasyon.sln` dosyasını açın
- Build → Rebuild Solution
- F5 ile çalıştırın

## 👤 Varsayılan Kullanıcılar

| Kullanıcı Adı | Şifre | Rol |
|---------------|-------|-----|
| admin | admin | Admin |

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
- `AIService` - Groq API entegrasyonu, iş analizi (Yeni!)
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

## 🎯 Proje Özellikleri

### ✅ Tamamlanan Özellikler
- [x] Modern dark theme UI
- [x] 5 kartlı dashboard
- [x] AI Chatbot (Groq API)
- [x] Ürün yönetimi (CRUD)
- [x] Stok yönetimi
- [x] Satış modülü
- [x] Kritik stok uyarıları
- [x] Kullanıcı yönetimi
- [x] SHA-256 güvenlik
- [x] 3-tier architecture
- [x] PlantUML ER diyagram

### 📝 Gelecek Özellikler
- [ ] Excel export/import
- [ ] Barkod okuyucu entegrasyonu
- [ ] Fiş/Fatura yazdırma
- [ ] Detaylı grafik raporları
- [ ] E-posta bildirimleri
- [ ] Mobil uygulama entegrasyonu

## 👨‍💻 Geliştirici Notları

### Yeni Form Eklemek İçin:
1. `Forms` klasörüne `.cs`, `.Designer.cs`, `.resx` ekle
2. `SessionManager.AktifKullanici` ile kullanıcı bilgisine eriş
3. `MessageHelper` ile kullanıcı bildirimleri göster
4. `ThemeColors` ile tutarlı renk paleti kullan

### AI Asistan Kullanımı:
```csharp
// Örnek sorular:
- "Hangi kategori en karlı?"
- "Kritik stok ürünleri neler?"
- "İşletmem için öneriler neler?"
- "MacBook Pro stoğu ne durumda?"
```

---

**Geliştirme Tarihi:** Ocak 2026  
**Durum:** ✅ Aktif geliştirme - AI özelliği eklendi

**🚀 Proje GitHub'da! Login: admin / admin**
