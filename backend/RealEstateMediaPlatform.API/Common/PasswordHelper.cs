namespace RealEstateMediaPlatform.API.Common
{
    public static class PasswordHelper
    {
        public static string GenerateRandomPassword()
        {
            var random = new Random();

            // Fixed format: Tempass + 1 uppercase + 1 lowercase + 1 digit + 1 special char 
            var password = "Tempass" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[random.Next(26)] +      // 1 random uppercase letter
                "abcdefghijklmnopqrstuvwxyz"[random.Next(26)] +      // 1 random lowercase letter  
                "0123456789"[random.Next(10)] +                     // 1 random digit
                "!@#$%^&*"[random.Next(8)];                     // 1 random special character
                                       
            return password;
        }
    }
}