using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class LocalEncryptionService : IEncryptionService
{
    private readonly ILogger<LocalEncryptionService> _logger;
    private readonly byte[] _key;

    public LocalEncryptionService(ILogger<LocalEncryptionService> logger, byte[] key)
    {
        _logger = logger;
        _key = key;
    }

    public string Encrypt(string plainText)
    {
        _logger.LogDebug("Encrypting text of length {Length}", plainText.Length);
        var bytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(bytes);
    }

    public string Decrypt(string cipherText)
    {
        _logger.LogDebug("Decrypting text of length {Length}", cipherText.Length);
        var bytes = Convert.FromBase64String(cipherText);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public string GenerateRandomToken(int length = 32)
    {
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
    }
}
