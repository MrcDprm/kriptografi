using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace kriptografi.Models
{
    public class CryptoModel
    {
        [Required(ErrorMessage = "Lütfen bir metin giriniz")]
        [Display(Name = "Metin")]
        public string Text { get; set; }

        [Display(Name = "Şifrelenmiş Metin")]
        public string EncryptedText { get; set; }

        [Display(Name = "Çözülmüş Metin")]
        public string DecryptedText { get; set; }

        [Display(Name = "SHA256 Özet")]
        public string HashValue { get; set; }

        [Display(Name = "Dosya")]
        public IFormFile File { get; set; }

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string RsaEncryptedText { get; set; }
        public string RsaDecryptedText { get; set; }
        public string TextHash { get; set; }
    }
} 