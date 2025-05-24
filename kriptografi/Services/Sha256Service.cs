using System.Security.Cryptography;
using System.Text;

namespace kriptografi.Services
{
    public class Sha256Service
    {
        public string ComputeHash(Stream stream)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public string ComputeHash(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Metin boş olamaz.");

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public bool CompareHashes(string hash1, string hash2)
        {
            if (string.IsNullOrEmpty(hash1) || string.IsNullOrEmpty(hash2))
                return false;

            return string.Equals(hash1, hash2, StringComparison.OrdinalIgnoreCase);
        }

        public string SaveHash(string hash, string fileName)
        {
            if (string.IsNullOrEmpty(hash))
                throw new ArgumentException("Hash değeri boş olamaz.");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Dosya adı boş olamaz.");

            var hashFilePath = Path.Combine("Hashes", $"{fileName}.hash");
            Directory.CreateDirectory("Hashes");
            File.WriteAllText(hashFilePath, hash);
            return hashFilePath;
        }

        public string LoadHash(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Dosya adı boş olamaz.");

            var hashFilePath = Path.Combine("Hashes", $"{fileName}.hash");
            if (!File.Exists(hashFilePath))
                throw new FileNotFoundException("Hash dosyası bulunamadı.");

            return File.ReadAllText(hashFilePath);
        }
    }
} 