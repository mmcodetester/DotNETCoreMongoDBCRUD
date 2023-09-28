namespace DotNETCoreMongoDBCRUD.Utli
{
    public class PasswordHashHelper
    {
        public PasswordHashHelper()
        {

        }
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return System.Web.Helpers.Crypto.HashPassword(password);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return System.Web.Helpers.Crypto.VerifyHashedPassword(correctHash, password);
        }
    }
}
