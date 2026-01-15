🏦 Banka Simülasyon Sistemi (WPF & Entity Framework)
Bu proje, temel bankacılık süreçlerini (Para Yatırma, Çekme, Transfer ve İşlem Geçmişi) modern bir kullanıcı arayüzü ile sunan, C# ve WPF kullanılarak geliştirilmiş bir masaüstü uygulamasıdır. Arka planda Entity Framework Core ve SQL Server kullanarak verilerin kalıcılığını ve tutarlılığını sağlar.

🚀 Öne Çıkan Özellikler
Modern Dashboard: Kullanıcının bakiyesini ve son işlemlerini tek bir ekranda görebildiği dinamik ana sayfa.

Finansal İşlemler: Kullanıcı dostu popup pencereleri (ParaIslemWindow) üzerinden para yatırma ve çekme işlemleri.

Akıllı Transfer Sistemi: Hesap numarası üzerinden anlık para transferi ve çift taraflı (gönderen/alıcı) işlem loglama.

Detaylı İşlem Geçmişi: Tüm finansal hareketlerin tarih, tip ve miktar bazlı listelendiği gelişmiş IslemlerView ekranı.

Veri Tutarlılığı: Entity Framework ile ilişkisel veritabanı yönetimi ve işlem bazlı bakiye güncellemeleri.

🛠 Teknik Mimari (Tech Stack)
Frontend: WPF (Windows Presentation Foundation) & XAML

Backend: C# (.NET Core / Framework)

ORM: Entity Framework Core (Code-First)

Veritabanı: SQL Server

Desen: Service Layer Pattern (Sorumlulukların ayrılması prensibi)

🏗 Proje Yapısı
UI Layer: Kullanıcı arayüzü bileşenleri (MainWindow, DashboardView, IslemlerView).

Service Layer (BankaService): İş mantığının (business logic) ve veritabanı işlemlerinin yürütüldüğü katman.

Data Layer (BankaDbContext): Veritabanı tablolarının (Musteriler, Hesaplar, Islemler) ve ilişkilerinin tanımlandığı katman.
