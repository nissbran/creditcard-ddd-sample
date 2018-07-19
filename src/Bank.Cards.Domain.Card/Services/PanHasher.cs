using System.Security.Cryptography;
using System.Text;
using Bank.Cards.Domain.Card.ValueTypes;

namespace Bank.Cards.Domain.Card.Services
{
    public static class PanHasher
    {
        private const string SecretSalt = "ItsASecret";

        public static string HashPan(CardNumber cardNumber)
        {
            using (SHA256 shaM = new SHA256Managed())
            {
                var data = Encoding.UTF8.GetBytes(cardNumber.Pan + SecretSalt);
                var hash = shaM.ComputeHash(data);

                var hashedInputStringBuilder = new StringBuilder(64);
                foreach (var b in hash)
                    hashedInputStringBuilder.Append(b.ToString("X2"));

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}