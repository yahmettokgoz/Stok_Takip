# 🏪 Stok Takip Otomasyonu

Modern UI, AI Chatbot ve 3 katmanlı mimari yapıya sahip Stok Takip Otomasyonu projesi.

![Login Ekranı](StokTakipOtomasyon/Ekran%20Görüntüleri/login.png)

## 🎯 Özellikler

### 🤖 AI Chatbot (Yeni!)
- **Groq API** entegrasyonu (LLaMA 3.3 70B)
- İş zekası analizi ve öneriler
- Kategori bazlı kar marjı analizi
- Risk değerlendirmesi (yüksek değer, düşük stok)
- Doğal dil ile sorgu

### 🔐 Güvenlik
- SHA-256 şifre hash'leme
- Kullanıcı rolleri (Admin, Personel, Satıcı)
- Session yönetimi

### 📦 Ürün Yönetimi
- CRUD işlemleri, kategori yönetimi
- Barkod desteği, kritik stok uyarıları
- Ürün arama ve filtreleme

### 📊 Stok Yönetimi
- Stok giriş/çıkış kayıtları
- Stok hareketi geçmişi
- Gerçek zamanlı stok durumu

### 🛒 Satış Modülü
- Hızlı satış, otomatik satış numarası
- İndirim ve KDV hesaplaması
- Kar/Zarar analizi
- Transaction güvenliği

### 📈 Dashboard
- **5 Widget:** Toplam Satış, Toplam Ürün Çeşidi, Toplam Ürün Miktarı, Aylık Ciro, Kritik Stok
- Gerçek zamanlı veriler
- Modern dark theme UI

## 🛠️ Teknolojiler

- **.NET Framework 4.8** - Windows Forms
- **MS SQL Server** - 7 tablo
- **Groq API** - AI entegrasyonu
- **3-Tier Architecture** - DAL, BLL, UI
- **SHA-256** - Güvenlik

## 🗄️ Veritabanı

**7 Tablo:** Kullanicilar, Kategoriler, Urunler, StokHareketleri, Satislar, SatisDetaylari, Tedarikciler



## 🚀 Kurulum

### 1. Veritabanı
```sql
-- SQL Server Management Studio'da:
StokTakipOtomasyon/Database/CreateDatabase.sql
```

### 2. Groq API Key
1. [Groq Console](https://console.groq.com/keys)'dan API key alın
2. `StokTakipOtomasyon/StokTakip.UI/App.config.example` dosyasını kopyalayın
3. `App.config` olarak kaydedin ve API key'i ekleyin:
```xml
<add key="Groq_ApiKey" value="YOUR_GROQ_API_KEY_HERE" />
```

### 3. Connection String
`StokTakip.UI\App.config`:
```xml
<connectionStrings>
  <add name="StokTakipDB" 
       connectionString="Server=SUNUCU_ADI\SQLEXPRESS;Database=EnGuncelStokTakip;Integrated Security=true;" />
</connectionStrings>
```

### 4. Çalıştır
- Visual Studio'da `StokTakipOtomasyon.sln` açın
- Build → Rebuild Solution → F5

## 👤 Test Kullanıcıları

| Kullanıcı | Şifre | Rol |
|-----------|-------|-----|
| admin | admin | Admin |
| personel1 | password | Personel |

## 📂 Proje Yapısı

```
StokTakipOtomasyon/
├── StokTakip.DataAccess/    # DAL - SqlHelper, Models, Repositories
├── StokTakip.Business/       # BLL - Services, Validators, Helpers
└── StokTakip.UI/             # UI - Forms, Components, Helpers
```

### Business Logic Layer (BLL)
- `AuthService` - Giriş, şifreleme
- `UrunService` - Ürün iş mantığı
- `StokService` - Stok hareketleri
- `SatisService` - Satış ve kar hesaplama
- `AIService` - Groq API entegrasyonu, iş analizi (Yeni!)

## 🎨 Modern UI

- **Dark Theme** (Slate renk paleti)
- **Custom Components:** ModernCard, ModernButton, DashboardWidget
- Smooth animations, hover efektleri
- Responsive layout

## 🔧 Gelişmiş Özellikler

### Transaction Güvenliği
Satış işleminde: Satış kaydı → Detaylar → Stok güncelleme → Stok hareketi
**Hata durumunda:** Rollback

### AI Asistan Kullanımı
```
- "Hangi kategori en karlı?"
- "Kritik stok ürünleri neler?"
- "İşletmem için öneriler neler?"
- "MacBook Pro stoğu ne durumda?"
```

## 🎯 Proje Özellikleri

### ✅ Tamamlanan
- [x] Modern dark theme UI
- [x] 5 kartlı dashboard
- [x] AI Chatbot (Groq API)
- [x] Ürün yönetimi (CRUD)
- [x] Stok yönetimi
- [x] Satış modülü
- [x] Kritik stok uyarıları
- [x] 3-tier architecture
- [x] PlantUML ER diyagram

### 📝 Gelecek
- [ ] Excel export/import
- [ ] Barkod okuyucu
- [ ] Fiş/Fatura yazdırma
- [ ] Detaylı grafik raporları
- [ ] E-posta bildirimleri

---

## 📸 Ekran Görüntüleri

### Login Ekranı
![Login](StokTakipOtomasyon/Ekran%20Görüntüleri/login.png)

### Dashboard - 5 Widget
![Dashboard](StokTakipOtomasyon/Ekran%20Görüntüleri/dashboard.png)

### Kritik Stok Uyarıları
![Kritik Stok](StokTakipOtomasyon/Ekran%20Görüntüleri/kritik-stok.png)

### Ürün Listesi
![Ürünler](StokTakipOtomasyon/Ekran%20Görüntüleri/urunler.png)

### Ürün Düzenleme
![Ürün Düzenle](StokTakipOtomasyon/Ekran%20Görüntüleri/urun-duzenle.png)

### Satış Formu
![Satış](StokTakipOtomasyon/Ekran%20Görüntüleri/satis.png)

### Stok Hareketleri
![Stok Hareketleri](StokTakipOtomasyon/Ekran%20Görüntüleri/stok-hareketleri.png)

### Yeni Stok Hareketi
![Yeni Hareket](StokTakipOtomasyon/Ekran%20Görüntüleri/yeni-hareket.png)

### Ayarlar
![Ayarlar](StokTakipOtomasyon/Ekran%20Görüntüleri/ayarlar.png)

### AI Asistan (Yeni!)
![AI Chatbot](StokTakipOtomasyon/Ekran%20Görüntüleri/ai-asistan.png)

---

**Geliştirme Tarihi:** Ocak 2026  
**Durum:** ✅ Aktif geliştirme - AI özelliği eklendi

**🚀 Proje GitHub'da! Login: admin / admin**
