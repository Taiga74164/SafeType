// GitHub: https://github.com/Taiga74164
// 
// MIT License
// 
// Copyright (c) 2024 Joaquin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public readonly struct SafeType<T> where T : struct
{
    private const ulong Magic = 0xB16B00B1E5;
    private static readonly INumeric<T> Ops = NumericOperations<T>.Get();

    private readonly ulong _checksum;
    private readonly ulong _encrypted;
    private readonly ulong _key;
    private readonly ulong _magic;

    // Doesn't look clean but I don't wanna increase memory usage for small types hehe
    private readonly ulong[] _checksumArray;
    private readonly ulong[] _encryptedArray;
    private readonly ulong[] _keyArray;
    private readonly bool _isLargeType;

    static SafeType()
    {
        if (!NumericOperations<T>.IsSupported())
        {
#if UNITY_EDITOR
            throw new NotSupportedException($"SafeType<{typeof(T).Name}> is not supported");
#else
            throw new NotSupportedException();
#endif
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SafeType(T value = default)
    {
        _magic = Magic;
        var size = Marshal.SizeOf<T>();

        if (size <= 8)
        {
            _isLargeType = false;
            _key = CryptoUtils.GenerateKey();
            var raw = ConvertToUlong(value);
            _checksum = raw ^ _key;
            _encrypted = CryptoUtils.Encrypt(raw, _key);

            _checksumArray = null;
            _encryptedArray = null;
            _keyArray = null;
        }
        else
        {
            _isLargeType = true;
            var rawArray = ConvertToUlongArray(value);
            _keyArray = CryptoUtils.GenerateKeyArray(rawArray.Length);

            _checksumArray = new ulong[rawArray.Length];
            for (var i = 0; i < rawArray.Length; i++)
            {
                _checksumArray[i] = rawArray[i] ^ _keyArray[i];
            }

            _encryptedArray = CryptoUtils.EncryptArray(rawArray, _keyArray);

            _checksum = 0;
            _encrypted = 0;
            _key = 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Verify()
    {
        try
        {
            if (_magic != Magic)
            {
#if UNITY_EDITOR
                Debug.LogError("Memory tampering detected: Invalid magic number!");
#endif
                return false;
            }

            if (_isLargeType)
            {
                if (_encryptedArray == null || _keyArray == null || _checksumArray == null)
                    return false;

                var decrypted = CryptoUtils.DecryptArray(_encryptedArray, _keyArray);
                for (var i = 0; i < decrypted.Length; i++)
                {
                    if ((decrypted[i] ^ _keyArray[i]) != _checksumArray[i])
                    {
#if UNITY_EDITOR
                        Debug.LogError($"Memory tampering detected: Checksum mismatch at index {i}!");
#endif
                        return false;
                    }
                }
            }
            else
            {
                // Verify small type (original logic)
                var decrypted = CryptoUtils.Decrypt(_encrypted, _key);
                if ((decrypted ^ _key) != _checksum)
                {
#if UNITY_EDITOR
                    Debug.LogError("Memory tampering detected: Checksum mismatch!");
#endif
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"Verification failed: {ex.Message}");
#endif
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong ConvertToUlong(T value)
    {
        var size = Marshal.SizeOf<T>();
        if (size > 8)
        {
#if UNITY_EDITOR
            throw new ArgumentException($"Use ConvertToUlongArray for large types. Type {typeof(T).Name} is {size} bytes");
#else
            throw new ArgumentException();
#endif
        }

        var bytes = new byte[8];
        GCHandle handle = default;

        try
        {
            handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            Marshal.Copy(handle.AddrOfPinnedObject(), bytes, 0, size);
            return BitConverter.ToUInt64(bytes, 0);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"ConvertToUlong failed for {typeof(T).Name}: {ex.Message}");
#endif
            return 0;
        }
        finally
        {
            if (handle.IsAllocated)
                handle.Free();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong[] ConvertToUlongArray(T value)
    {
        var size = Marshal.SizeOf<T>();
        // Round up to the nearest ulong
        var ulongCount = (size + 7) / 8;
        var bytes = new byte[ulongCount * 8];

        GCHandle handle = default;
        try
        {
            handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            Marshal.Copy(handle.AddrOfPinnedObject(), bytes, 0, size);

            var result = new ulong[ulongCount];
            for (var i = 0; i < ulongCount; i++)
            {
                result[i] = BitConverter.ToUInt64(bytes, i * 8);
            }

            return result;
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"ConvertToUlongArray failed for {typeof(T).Name}: {ex.Message}");
#endif
            return new ulong[ulongCount];
        }
        finally
        {
            if (handle.IsAllocated)
                handle.Free();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertFromUlong(ulong value)
    {
        var bytes = BitConverter.GetBytes(value);
        var result = default(T);
        GCHandle handle = default;

        try
        {
            handle = GCHandle.Alloc(result, GCHandleType.Pinned);
            Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), Marshal.SizeOf<T>());
            result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            return result;
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"ConvertFromUlong failed for {typeof(T).Name}: {ex.Message}");
#endif
            return default;
        }
        finally
        {
            if (handle.IsAllocated)
                handle.Free();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T ConvertFromUlongArray(ulong[] values)
    {
        var size = Marshal.SizeOf<T>();
        var bytes = new byte[values.Length * 8];

        for (var i = 0; i < values.Length; i++)
        {
            var valueBytes = BitConverter.GetBytes(values[i]);
            Array.Copy(valueBytes, 0, bytes, i * 8, 8);
        }

        var result = default(T);
        GCHandle handle = default;

        try
        {
            handle = GCHandle.Alloc(result, GCHandleType.Pinned);
            Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), size);
            result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            return result;
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.LogError($"ConvertFromUlongArray failed for {typeof(T).Name}: {ex.Message}");
#endif
            return default;
        }
        finally
        {
            if (handle.IsAllocated)
                handle.Free();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T(SafeType<T> safe)
    {
        if (!safe.Verify())
        {
#if UNITY_EDITOR
            Debug.LogWarning($"SafeType<{typeof(T).Name}> verification failed, returning default value");
#endif
            return default;
        }

        if (safe._isLargeType)
        {
            var decrypted = CryptoUtils.DecryptArray(safe._encryptedArray, safe._keyArray);
            return ConvertFromUlongArray(decrypted);
        }
        else
        {
            var decrypted = CryptoUtils.Decrypt(safe._encrypted, safe._key);
            return ConvertFromUlong(decrypted);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator SafeType<T>(T value) => new SafeType<T>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SafeType<T> operator +(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Add(a, b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SafeType<T> operator -(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Subtract(a, b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SafeType<T> operator *(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Multiply(a, b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SafeType<T> operator /(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Divide(a, b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SafeType<T> a, SafeType<T> b)
        => Ops.LessThan(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SafeType<T> a, SafeType<T> b)
        => Ops.GreaterThan(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SafeType<T> a, SafeType<T> b)
        => Ops.LessThan(a, b) || EqualityComparer<T>.Default.Equals(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SafeType<T> a, SafeType<T> b)
        => Ops.GreaterThan(a, b) || EqualityComparer<T>.Default.Equals(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SafeType<T> a, SafeType<T> b)
        => EqualityComparer<T>.Default.Equals(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SafeType<T> a, SafeType<T> b)
        => !EqualityComparer<T>.Default.Equals(a, b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj switch
        {
            SafeType<T> other => this == other,
            T value => EqualityComparer<T>.Default.Equals(this, value),
            _ => false
        };
    }

    public override int GetHashCode() => ((T)this).GetHashCode();

    public override string ToString() => ((T)this).ToString();
}