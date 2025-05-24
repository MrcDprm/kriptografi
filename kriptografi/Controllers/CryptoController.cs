using Microsoft.AspNetCore.Mvc;
using kriptografi.Models;
using kriptografi.Services;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace kriptografi.Controllers
{
    public class CryptoController : Controller
    {
        private readonly AesService _aesService;
        private readonly Sha256Service _sha256Service;
        private readonly RsaService _rsaService;

        public CryptoController()
        {
            _aesService = new AesService();
            _sha256Service = new Sha256Service();
            _rsaService = new RsaService();
        }

        public IActionResult Index()
        {
            var model = new CryptoModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Encrypt(CryptoModel model)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("Text", "Lütfen şifrelenecek bir metin girin.");
                return View("Index", model);
            }

            try
            {
                model.EncryptedText = _aesService.Encrypt(model.Text);
                model.HashValue = null;
                model.DecryptedText = null;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Şifreleme hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Decrypt(CryptoModel model)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.EncryptedText))
            {
                ModelState.AddModelError("EncryptedText", "Lütfen şifre çözülecek bir metin girin.");
                return View("Index", model);
            }

            try
            {
                model.DecryptedText = _aesService.Decrypt(model.EncryptedText);
                model.HashValue = null;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Şifre çözme hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HashFile(CryptoModel model)
        {
            ModelState.Clear();
            if (model.File == null)
            {
                ModelState.AddModelError("File", "Lütfen bir dosya seçin.");
                return View("Index", model);
            }

            try
            {
                using (var stream = model.File.OpenReadStream())
                {
                    model.HashValue = _sha256Service.ComputeHash(stream);
                }
                model.EncryptedText = null;
                model.DecryptedText = null;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Dosya özeti hesaplama hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HashText(CryptoModel model)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("Text", "Lütfen bir metin girin.");
                return View("Index", model);
            }

            try
            {
                model.TextHash = _sha256Service.ComputeHash(model.Text);
                TempData["SuccessMessage"] = "Metin özeti başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Metin özeti hesaplama hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateRsaKeys(CryptoModel model)
        {
            ModelState.Clear();
            try
            {
                var (publicKey, privateKey) = _rsaService.GenerateKeyPair();
                model.PublicKey = publicKey;
                model.PrivateKey = privateKey;
                model.RsaEncryptedText = null;
                model.RsaDecryptedText = null;
                model.Text = null;
                TempData["SuccessMessage"] = "RSA anahtar çifti başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Anahtar üretme hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RsaEncrypt(CryptoModel model)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("Text", "Lütfen şifrelenecek bir metin girin.");
                return View("Index", model);
            }

            if (string.IsNullOrEmpty(model.PublicKey))
            {
                ModelState.AddModelError("PublicKey", "Lütfen önce 'Yeni Anahtar Çifti Oluştur' butonuna tıklayın.");
                return View("Index", model);
            }

            try
            {
                model.RsaEncryptedText = _rsaService.Encrypt(model.Text, model.PublicKey);
                model.RsaDecryptedText = null;
                TempData["SuccessMessage"] = "Metin başarıyla şifrelendi.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "RSA şifreleme hatası: " + ex.Message);
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RsaDecrypt(CryptoModel model)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.RsaEncryptedText))
            {
                ModelState.AddModelError("RsaEncryptedText", "Lütfen şifre çözülecek bir metin girin.");
                return View("Index", model);
            }

            if (string.IsNullOrEmpty(model.PrivateKey))
            {
                ModelState.AddModelError("PrivateKey", "Lütfen önce 'Yeni Anahtar Çifti Oluştur' butonuna tıklayın.");
                return View("Index", model);
            }

            try
            {
                model.RsaDecryptedText = _rsaService.Decrypt(model.RsaEncryptedText, model.PrivateKey);
                TempData["SuccessMessage"] = "Metin başarıyla çözüldü.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "RSA şifre çözme hatası: " + ex.Message);
            }
            return View("Index", model);
        }
    }
} 