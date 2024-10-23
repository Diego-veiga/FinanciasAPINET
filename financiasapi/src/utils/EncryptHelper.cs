using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace financiasapi.src.utils
{
    public class EncryptHelper
    {
        public  static Byte[] GenerateSalt(byte[] salt)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string EncryptPassword(string senha, byte[] salt)
        {
            string senhaCriptografada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return senhaCriptografada;
        }
    }
}
