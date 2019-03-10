# BoynerCase

# Projenin Amaçı

Web.config, app.config gibi dosyalarda tutulan appkey’lerin ortak ve dinamik bir yapıyla erişilebilir olması ve deployment veya restart, recycle gerektirmeden güncellemelerin yapılabilir olmasıdır.

## Uygulamanın local'de çalıştırılması

-- ilk olarak Startup proje "Boyner.Web.UI" seçilir.(db initilize etmek için)

-- appsettings dosyasında db'ye bağlanmak için localhost verilmiştir.

-- Package Manager Console 'dan default proje Boyner.Core seçilir.

-- add-migration "migration_name" update-database komutlarıyla database yaratılır.

## ConfigurationReader Kullanımı

Projelerde ConfigurationReader kullanımı Boyner.Client uygulamasında kullanılmıştır.

## Koşullar
 
•	Kütüphane storage’a erişemediğinde son başarılı konfigürasyon kayıtları ile çalışabilmelidir.

      database'e erişim sağlanmadığında cache'de son çalışan app varsa geriye son çalışan app döner
      
•	Kütüphane her tipe ait dönüş bilgisini kendi içerisinde halletmelidir. 

      Generic tip kullanılmıştır.
      
•	Sistem parametrik olarak verilen süre periyodunda yeni kayıtları ve kayıt değişikliklerini kontrol etmelidir. 

      ConfigurationReader kullanırken dakika cinsinden zaman tanımlanır. bu zaman içinde yeni kayıt varsa cache null olduktan sonra yeni       kayıtlarda geriye dönebilmektedir. (Test yazıldı kontrol edildi.)
      
•	Her servis yalnızca kendi konfigürasyon kayıtlarına erişebilmeli, başkasının kayıtlarını görmemelidir.

      ConfigurationReader verilen app name ile T GetValue<T>(string key) methodundaki appname parametresi aynı ise geriye istek yapılan       değer döner


# Technology

- C#
- ASP.NET Core
- ASP.NET Core MVC
- JavaScript, jQuery
- Entity Framework Core 2.2.0
- MSSQL
- Caching;
- RabbitMQ
- Xunit Test
# Prerequisites

-  Visual Studio 2017
-  .NET Core 2.2 (https://www.microsoft.com/net/core)
-  ReSharper (optional)

 





