namespace Letstudy.Utils;

public static class PasswordHasher
{
    public static string Hash(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    
    public static bool Verify(string password, string passwordHash) => 
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}