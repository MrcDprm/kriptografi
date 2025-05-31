# Kriptografi Projesi

Bu proje, ASP.NET Core MVC teknolojisi kullanılarak geliştirilmiş, temel kriptografi işlemlerini (RSA & SHA256) uygulamalı olarak gerçekleştiren bir bitirme/final projesidir.

## 🎯 Proje Amacı
Kullanıcı dostu bir arayüz üzerinden:
- RSA Anahtar Üretimi
- RSA ile Metin Şifreleme
- RSA ile Şifreli Metni Çözme
- SHA256 Hashleme
gibi temel şifreleme işlemlerini gerçekleştirmeyi amaçlar.

## 🧰 Kullanılan Teknolojiler
- ASP.NET Core MVC (.NET 6+)
- C#
- Bootstrap 5
- RSA & SHA256 (System.Security.Cryptography)
- Visual Studio / Rider IDE

## ⚙️ Özellikler
- ✅ RSA Public & Private Key üretme
- ✅ Metin tabanlı RSA şifreleme ve çözme
- ✅ SHA256 ile tek yönlü özetleme
- ✅ Kullanımı kolay sade ve modern bir arayüz
- ✅ Tüm işlemlerde Base64 destekli veri çıktısı

## 🚀 Kurulum ve Çalıştırma
1. Projeyi klonlayın
2. Visual Studio veya Rider IDE ile açın
3. Gerekli NuGet paketlerinin yüklendiğinden emin olun
4. Projeyi derleyin ve çalıştırın

## 📝 Kullanım
Proje her işlemi tek bir sayfada gerçekleştiriyor ve bu anasayfanın görüntüsüdür.
![Ekran görüntüsü 2025-05-24 174458](https://github.com/user-attachments/assets/24dcdb25-cb2b-464d-b846-de2935e71dbd)

AES Şifreleme işlemi için belirli alana metni giriyoruz ve şifrele butonuna tıklıyoruz. Şifrelenmiş metin karşımıza çıkıyor.
![Ekran görüntüsü 2025-05-24 174913](https://github.com/user-attachments/assets/e2d2af52-ec96-490a-b533-278795537ebe)

Şifrelenmiş metni çözüp yazılı olan metni görmek için şifreyi çöz butonuna tıklıyoruz ve çözülmüş metin karşımıza çıkıyor.
![Ekran görüntüsü 2025-05-24 174920](https://github.com/user-attachments/assets/7610045c-4e4a-4a44-bfcf-1c352e26d6e4)

SHA256  Dosya özeti alma ve metin özeti alma kısmında ise yüklediğimiz dosyanın dosya özetini alabiliyoruz.
![Ekran görüntüsü 2025-05-24 174649](https://github.com/user-attachments/assets/6c6f74d8-ee17-4b17-a8d1-bcf0d92b0a23)

Girilmiş olan metnin özetini de metin özeti al butonuna tıklayarak alabiliyoruz.
![Ekran görüntüsü 2025-05-24 174657](https://github.com/user-attachments/assets/2e41f8c4-3996-4d82-a4fc-e85a16473cfa)

RSA Şifreleme işleminde ise öncelikle anahtar çifti oluşturuyoruz(Public-Private Key).
Eğer oluşturmadan şifrelemeye çalışırsak bize uyarı veriyor.(Önce anahtar çifti oluşturulmalı diye)
Anahtar çiftini oluşturduktan sonra istenilen metni giriyoruz ve şifreliyoruz.
Sonra bu metnin şifrelenmiş halini çözmek için şifre çöz butonuna basıyoruz ve çözülmüş metin karşımıza çıkıyor.
![Ekran görüntüsü 2025-05-24 174617](https://github.com/user-attachments/assets/6ae2dcde-7124-40a4-9766-ad92fd2825ca)

