# ResimdenMetinOkuyucu
C# ile Tesseract OCR kullanarak resimden metin okuyan masaüstü uygulaması

## 📸 Uygulama Görseli
![Uygulama Ekran Görüntüsü](https://i.imgur.com/hhj1sqS.png)

## 🛠️ Nasıl Çalıştırılır?

1.  Projeyi klonlayın: `git clone https://github.com/dbrave-dot/ResimdenMetinOkuyucu.git`
2.  Visual Studio ile `.sln` dosyasını açın.
3.  **NuGet Paketleri** otomatik olarak yüklenecektir (Tesseract).
4.  **Önemli:** `bin\Debug\net8.0\` klasörü içinde `tessdata` adında bir klasör oluşturun ve içine [buradan](https://github.com/tesseract-ocr/tessdata) indireceğiniz `tur.traineddata` ile `eng.traineddata` dosyalarını koyun.
5.  `F5` ile çalıştırın.
