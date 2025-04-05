# 🛍️ DefineX E-Ticaret Uygulaması (Fullstack Proje)

Bu proje, Vue.js frontend'i ile .NET Core tabanlı mikro servisleri entegre eden bir **fullstack e-ticaret uygulamasıdır**. Kullanıcılar ürünleri görüntüleyebilir, favorilere ekleyebilir, alışveriş sepeti oluşturabilir ve admin yetkisiyle yeni ürün ekleyebilir. Ayrıca gerçek zamanlı sohbet sistemi sayesinde kullanıcılar admin ile iletişim kurabilir.

---

## 🚀 Özellikler

### ✅ Genel Özellikler
- Katmanlı mimari ve SOLID prensiplerine uygun yapı
- Vue.js ile modern kullanıcı arayüzü
- .NET Core Web API'ler ile güçlü backend
- IdentityServer ile JWT tabanlı kimlik doğrulama ve yetkilendirme
- Gerçek zamanlı mesajlaşma (SignalR)
- JSON dosyasından veritabanına ürün yükleme

---

### 🧱 Backend (ASP.NET Core Web API)
#### Product API
- Ürünleri listeleme
- Ürün detayı görüntüleme
- Admin için ürün ekleme/güncelleme/silme
- DTO kullanımı (ProductDto)
- Repository pattern ile veri erişimi

#### ShoppingCart API
- Sepete ürün ekleme
- Sepet içeriğini görüntüleme
- Sepeti temizleme

#### Favorites API
- Favori ürün ekleme/kaldırma
- Kullanıcının favorilerini görüntüleme

#### Identity Server API
- Admin ve müşteri rolleri
- Token tabanlı giriş
- Roller ve kullanıcılar veritabanına otomatik yüklenir

#### Chat API (SignalR)
- Gerçek zamanlı müşteri-admin mesajlaşma
- SignalR Hub yapısı ile canlı sohbet

---

### 🌐 Frontend (Vue.js / Nuxt 3)
- Ürünleri kategoriye göre listeleme
- Ürün detay sayfası
- Favori ürünlere kalp ikonu ile ekleme/çıkarma
- Sepet yönetimi (ekle/görüntüle/temizle)
- Giriş yapan kullanıcının bilgilerini ve rolünü gösterme
- Admin kullanıcıya özel "Ürün Ekle" arayüzü
- Gerçek zamanlı sohbet kutusu

---

## 🛠️ Kullanılan Teknolojiler

- **Backend:** ASP.NET Core 7, Entity Framework Core, IdentityServer, SignalR, AutoMapper
- **Frontend:** Nuxt 3 (Vue 3), Pinia Store, TailwindCSS
- **Veritabanı:** MS SQL Server
- **Kimlik Doğrulama:** IdentityServer + JWT
- **Gerçek Zamanlı:** SignalR

---

## 🗃️ Veritabanı Başlatma (.json üzerinden)

Proje ilk başlatıldığında, aşağıdaki kod ile `products.json` dosyasındaki veriler otomatik olarak veritabanına yüklenir:

```csharp
if (!context.Products.Any())
{
	var jsonData = File.ReadAllText("Data/products.json");
	var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
	context.Products.AddRange(products);
	context.SaveChanges();
}
