namespace Sample.Core.Infrastructure.Security
{
    public static class Crypto
    {
        public static string ComputeHash(string plainText)
        {
            var salt = BCrypt.GenerateSalt();
            var hash = BCrypt.HashPassword(plainText, salt);
            return hash;
        }

        public static bool VerifyHash(string plainText, string hash)
        {
            return BCrypt.Verify(plainText, hash);
        }
    }
}