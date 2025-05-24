using System.Security.Cryptography;
using System.Text;

namespace kriptografi.Services
{
    public class CryptoService
    {
        private readonly string _key = "deneme12333"; 
        private readonly string _iv = "deneme11eee"; 

        public string EncryptAES(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key.PadRight(32).Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(_iv.PadRight(16).Substring(0, 16));

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string DecryptAES(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key.PadRight(32).Substring(0, 32));
                aes.IV = Encoding.UTF8.GetBytes(_iv.PadRight(16).Substring(0, 16));

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public string ComputeSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task<string> ComputeSHA256FromFile(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = await sha256.ComputeHashAsync(stream);
                return Convert.ToBase64String(hash);
            }
        }
    }
} 