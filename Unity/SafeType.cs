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
using System.Runtime.InteropServices;
using UnityEngine;

public class SafeType<T> where T : struct
{
    private const ulong Magic = 0xB16B00B1E5;

    private static readonly INumeric<T> Ops;
    private readonly ulong _checksum;
    private readonly ulong _encrypted;
    private readonly ulong _key;
    private readonly ulong _magic;

    static SafeType()
    {
        if (typeof(T) == typeof(int))
            Ops = (INumeric<T>)new IntOperations();
        else if (typeof(T) == typeof(float))
            Ops = (INumeric<T>)new FloatOperations();
        else if (typeof(T) == typeof(double))
            Ops = (INumeric<T>)new DoubleOperations();
        else if (typeof(T) == typeof(Vector2))
            Ops = (INumeric<T>)new Vector2Operations();
        else if (typeof(T) == typeof(Vector3))
            Ops = (INumeric<T>)new Vector3Operations();
        else if (typeof(T) == typeof(Quaternion))
            Ops = (INumeric<T>)new QuaternionOperations();
    }

    public SafeType(T value = default)
    {
        _magic = Magic;
        _key = CryptoUtils.GenerateKey();
        var raw = ConvertToUlong(value);
        _checksum = raw ^ _key;
        _encrypted = CryptoUtils.Encrypt(raw, _key);
    }

    private bool Verify()
    {
        if (_magic != Magic)
#if UNITY_EDITOR
            throw new Exception("Memory tampering detected!");
#else
            return false;
#endif

        var decrypted = CryptoUtils.Decrypt(_encrypted, _key);
        if ((decrypted ^ _key) != _checksum)
#if UNITY_EDITOR
            throw new Exception("Memory tampering detected!");
#else
            return false;
#endif

        return true;
    }

    private static ulong ConvertToUlong(T value)
    {
        var bytes = new byte[8];
        var size = Marshal.SizeOf<T>();
        if (size > 8)
#if UNITY_EDITOR
            throw new ArgumentException("Type too large");
#else
            return 0;
#endif

        var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
        try
        {
            Marshal.Copy(handle.AddrOfPinnedObject(), bytes, 0, size);
        }
        finally
        {
            handle.Free();
        }

        return BitConverter.ToUInt64(bytes, 0);
    }

    public static implicit operator T(SafeType<T> safe)
    {
        if (!safe.Verify()) return default;
        var decrypted = CryptoUtils.Decrypt(safe._encrypted, safe._key);
        return ConvertFromUlong(decrypted);
    }

    public static implicit operator SafeType<T>(T value) => new SafeType<T>(value);

    private static T ConvertFromUlong(ulong value)
    {
        var bytes = BitConverter.GetBytes(value);
        var result = default(T);
        var handle = GCHandle.Alloc(result, GCHandleType.Pinned);
        try
        {
            Marshal.Copy(bytes, 0, handle.AddrOfPinnedObject(), Marshal.SizeOf<T>());
            result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }

        return result;
    }

    public static SafeType<T> operator +(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Add((T)a, (T)b));

    public static SafeType<T> operator -(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Subtract((T)a, (T)b));

    public static SafeType<T> operator *(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Multiply((T)a, (T)b));

    public static SafeType<T> operator /(SafeType<T> a, SafeType<T> b)
        => new SafeType<T>(Ops.Divide((T)a, (T)b));

    public static bool operator <(SafeType<T> a, SafeType<T> b)
        => Ops.LessThan((T)a, (T)b);

    public static bool operator >(SafeType<T> a, SafeType<T> b)
        => Ops.GreaterThan((T)a, (T)b);

    public static bool operator ==(SafeType<T> a, SafeType<T> b)
        => EqualityComparer<T>.Default.Equals((T)a, (T)b);

    public static bool operator !=(SafeType<T> a, SafeType<T> b)
        => !EqualityComparer<T>.Default.Equals((T)a, (T)b);

    public override bool Equals(object obj)
    {
        if (obj is SafeType<T> other) return this == other;
        return false;
    }

    public override int GetHashCode() => ((T)this).GetHashCode();

    public override string ToString() => ((T)this).ToString();
}