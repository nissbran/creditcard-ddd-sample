using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card.Services
{
    public static class PanEncryptor
    {
        private static readonly string InputKey = "b779121c-dbb1-4d58-87ab-7239853218b6";
        private static readonly byte[] Salt = Encoding.UTF8.GetBytes("SuperSecretSalt");
        
        public static string EncryptPan(CardNumber cardNumber)
        {
            using (Rijndael aesAlg = CreateManagedRijndael())
            {
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                var msEncrypt = new MemoryStream();
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(cardNumber.Pan);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public static string DecryptPan(string encryptedPan)
        {
            using (Rijndael aesAlg = CreateManagedRijndael())
            {
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                var cipher = Convert.FromBase64String(encryptedPan);

                string pan;

                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            pan = srDecrypt.ReadToEnd();
                        }
                    }
                }
                return pan;
            }
        }

        private static Rijndael CreateManagedRijndael()
        {
            var key = new Rfc2898DeriveBytes(InputKey, Salt);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);
            
            return aesAlg;
        }
    }
}