using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Api.Utils {
    
    public class Security {

        byte[] Key = Encoding.UTF8.GetBytes(Global.Key);
        byte[] IV = Encoding.UTF8.GetBytes(Global.IV);

        public string Encrypt(string text) {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                swEncrypt.Write(text);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string text) {
            Aes aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cipheredBytes = Convert.FromBase64String(text);
            MemoryStream msEncrypt = new MemoryStream(cipheredBytes);
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csEncrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
