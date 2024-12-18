using System;
using System.Security.Cryptography;

public static class CryptoUtils
{
    private static ulong RotateLeft(ulong x, int r) => x << r | x >> 64 - r;

    public static ulong Encrypt(ulong value, ulong key)
    {
        value ^= key;
        value = RotateLeft(value, 13);
        value ^= RotateLeft(key, 7);
        return value;
    }

    public static ulong Decrypt(ulong value, ulong key)
    {
        value ^= RotateLeft(key, 7);
        value = RotateLeft(value, 64 - 13);
        value ^= key;
        return value;
    }

    public static ulong GenerateKey()
    {
        var buffer = new byte[8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }

        return BitConverter.ToUInt64(buffer, 0) ^ (ulong)DateTime.UtcNow.Ticks;
    }
}