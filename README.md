# BoynerCase

# Projenin Amaçı

Web.config, app.config gibi dosyalarda tutulan appkey’lerin ortak ve dinamik bir yapıyla erişilebilir olması ve deployment veya restart, recycle gerektirmeden güncellemelerin yapılabilir olmasıdır.

## Uygulamanın Çalıştırılması

-- ilk olarak Startup proje "Boyner.Web.UI" seçilir.(db initilize etmek için)

-- appsettings dosyasında db'ye bağlanmak için localhost verilmiştir.

-- Package Manager Console 'dan default proje Boyner.Core seçilir.

-- add-migration "migration_name" update-database komutlarıyla database yaratılır.

## ConfigurationReader Kullanımı

Projelerde ConfigurationReader kullanımı Boyner.Client uygulamasında kullanılmıştır.





