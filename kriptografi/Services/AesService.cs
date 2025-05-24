using System.Security.Cryptography;
using System.Text;

namespace kriptografi.Services
{
    public class AesService
    {
        private readonly string _key = "deneme123"; 
        private readonly string _iv = "deneme12333"; 

        public string Encrypt(string plainText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    
                    byte[] keyBytes = new byte[32]; 
                    byte[] ivBytes = new byte[16]; 
                    
                    
                    byte[] tempKey = Encoding.UTF8.GetBytes(_key);
                    Array.Copy(tempKey, keyBytes, Math.Min(tempKey.Length, 32));
                    
                    
                    byte[] tempIV = Encoding.UTF8.GetBytes(_iv);
                    Array.Copy(tempIV, ivBytes, Math.Min(tempIV.Length, 16));

                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

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
            catch (Exception ex)
            {
                throw new Exception("AES şifreleme hatası: " + ex.Message);
            }
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    // Key ve IV'yi doğru boyutlara getir
                    byte[] keyBytes = new byte[32]; // 256 bit
                    byte[] ivBytes = new byte[16];  // 128 bit
                    
                    // Key'i 32 byte'a tamamla
                    byte[] tempKey = Encoding.UTF8.GetBytes(_key);
                    Array.Copy(tempKey, keyBytes, Math.Min(tempKey.Length, 32));
                    
                    // IV'yi 16 byte'a tamamla
                    byte[] tempIV = Encoding.UTF8.GetBytes(_iv);
                    Array.Copy(tempIV, ivBytes, Math.Min(tempIV.Length, 16));

                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AES şifre çözme hatası: " + ex.Message);
            }
        }
    }
} 
