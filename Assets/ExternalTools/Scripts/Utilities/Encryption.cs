using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public static class Encryption
    {
        private static readonly byte[] _key =
        {
            0x45, 0x4E, 0x3A, 0x8C, 0x89, 0x70, 0x37, 0x99, 0x58, 0x31, 0x24, 0x98, 0x3A, 0x87, 0x9B, 0x34, 0x45, 0x4E, 0x3A, 0x8C, 0x89, 0x70, 0x37, 0x99, 0x58, 0x31, 0x24, 0x98, 0x3A, 0x87, 0x9B, 0x34
        };
    
        public static string EncryptDecryptString(string raw)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < raw.Length; i++)
                stringBuilder.Append((char)(raw[i] ^ _key[i % _key.Length]));
            return stringBuilder.ToString();
        }

        public static void EncryptDecryptBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= _key[i % _key.Length];
        }
    
        public static string ComputeSha256(string rawData)  
        {
        
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
            
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                    builder.Append(bytes[i].ToString("x2"));  
            
                return builder.ToString();  
            }  
        }  
    }
}
