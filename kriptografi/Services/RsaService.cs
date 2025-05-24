using System.Security.Cryptography;
using System.Text;

namespace kriptografi.Services
{
    public class RsaService
    {
        public (string publicKey, string privateKey) GenerateKeyPair()
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;
                    var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                    var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                    return (publicKey, privateKey);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RSA anahtar çifti oluşturma hatası: " + ex.Message);
            }
        }

        public string Encrypt(string text, string publicKey)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    throw new ArgumentException("Şifrelenecek metin boş olamaz.");

                if (string.IsNullOrEmpty(publicKey))
                    throw new ArgumentException("Public Key boş olamaz.");

                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = false;
                    var publicKeyBytes = Convert.FromBase64String(publicKey);
                    rsa.ImportRSAPublicKey(publicKeyBytes, out _);

                    var dataToEncrypt = Encoding.UTF8.GetBytes(text);
                    var encryptedData = rsa.Encrypt(dataToEncrypt, true);
                    return Convert.ToBase64String(encryptedData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RSA şifreleme hatası: " + ex.Message);
            }
        }

        public string Decrypt(string encryptedText, string privateKey)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                    throw new ArgumentException("Şifre çözülecek metin boş olamaz.");

                if (string.IsNullOrEmpty(privateKey))
                    throw new ArgumentException("Private Key boş olamaz.");

                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.PersistKeyInCsp = false;
                    var privateKeyBytes = Convert.FromBase64String(privateKey);
                    rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

                    var encryptedData = Convert.FromBase64String(encryptedText);
                    var decryptedData = rsa.Decrypt(encryptedData, true);
                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RSA şifre çözme hatası: " + ex.Message);
            }
        }
    }
} 