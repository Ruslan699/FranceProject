using System;
using System.Security.Cryptography;

namespace DaddyAgencies.Common.Util
{

    public class HashUtil
    {
        public static string ComputeSaltedHash(string password, string salt)
        {
            using (var hasher = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt)))
            {
                return Convert.ToBase64String(hasher.GetBytes(256 / 8));
            }
        }

        public static bool VerifyHashes(string hashA, string hashB)
        {
            var digestA = Convert.FromBase64String(hashA);
            var digestB = Convert.FromBase64String(hashB);

            if (digestA.Length != digestB.Length) return false;

            var result = 0;
            for (var i = 0; i < digestA.Length; i++)
            {
                result |= digestA[i] ^ digestB[i];
            }
            return result == 0;
        }

        public static string GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
