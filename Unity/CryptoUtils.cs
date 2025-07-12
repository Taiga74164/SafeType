using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public static class CryptoUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong RotateLeft(ulong x, int r) => x << r | x >> 64 - r;
    // [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // private static ulong RotateRight(ulong x, int r) => x >> r | x << 64 - r;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Encrypt(ulong value, ulong key)
    {
        value ^= key;
        value = RotateLeft(value, 13);
        value ^= RotateLeft(key, 7);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Decrypt(ulong value, ulong key)
    {
        value ^= RotateLeft(key, 7);
        value = RotateLeft(value, 64 - 13);
        value ^= key;
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong[] EncryptArray(ulong[] values, ulong[] keys)
    {
        var result = new ulong[values.Length];
        for (var i = 0; i < values.Length; i++)
        {
            result[i] = Encrypt(values[i], keys[i]);
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong[] DecryptArray(ulong[] values, ulong[] keys)
    {
        var result = new ulong[values.Length];
        for (var i = 0; i < values.Length; i++)
        {
            result[i] = Decrypt(values[i], keys[i]);
        }

        return result;
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

    public static ulong[] GenerateKeyArray(int count)
    {
        var keys = new ulong[count];
        for (var i = 0; i < count; i++)
        {
            keys[i] = GenerateKey();
        }

        return keys;
    }
}