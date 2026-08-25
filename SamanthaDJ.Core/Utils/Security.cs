using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Utils {
    
    public class Security {
        string myKey = $"{Global.tempKD}{Global.tempKG}{Global.tempKB}{Global.tempKE}{Global.tempKA}{Global.tempKC}{Global.tempKF}";
        string myIV = $"{Global.tempIC}{Global.tempIA}{Global.tempID}{Global.tempIB}";

        public string Encrypt(string text) {
            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(myKey);
            aes.IV = Encoding.UTF8.GetBytes(myIV);

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
            aes.Key = Encoding.UTF8.GetBytes(myKey);
            aes.IV = Encoding.UTF8.GetBytes(myIV);

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cipheredBytes = Convert.FromBase64String(text);
            MemoryStream msEncrypt = new MemoryStream(cipheredBytes);
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csEncrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
