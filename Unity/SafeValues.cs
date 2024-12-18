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
using UnityEngine;

[Serializable]
public class SafeBool : BaseSafeValue<bool>
{
    public SafeBool(bool defaultValue = false) : base(defaultValue) { }
    public static implicit operator bool(SafeBool safe) => safe.GetRaw();
    public static implicit operator SafeType<bool>(SafeBool safe) => safe.Get();
    public static implicit operator SafeBool(bool value) => new(value);
    public static implicit operator SafeBool(SafeType<bool> safe) => new(safe);

    // Boolean operators
    public static bool operator ==(SafeBool a, SafeBool b) => a?.GetRaw() == b?.GetRaw();
    public static bool operator !=(SafeBool a, SafeBool b) => a?.GetRaw() != b?.GetRaw();
    public static bool operator ==(SafeBool a, bool b) => a?.GetRaw() == b;
    public static bool operator !=(SafeBool a, bool b) => a?.GetRaw() != b;
    public static bool operator ==(bool a, SafeBool b) => a == b?.GetRaw();
    public static bool operator !=(bool a, SafeBool b) => a != b?.GetRaw();

    // Logical operators
    public static SafeBool operator &(SafeBool a, SafeBool b) => new(a.GetRaw() & b.GetRaw());
    public static SafeBool operator |(SafeBool a, SafeBool b) => new(a.GetRaw() | b.GetRaw());
    public static SafeBool operator ^(SafeBool a, SafeBool b) => new(a.GetRaw() ^ b.GetRaw());
    public static SafeBool operator !(SafeBool a) => new(!a.GetRaw());

    public override bool Equals(object obj)
    {
        if (obj is SafeBool other) return this == other;
        if (obj is bool b) return this == b;

        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();
}

[Serializable]
public class SafeInt : BaseSafeValue<int>
{
    public SafeInt(int defaultValue = 0) : base(defaultValue) { }
    public static implicit operator int(SafeInt safe) => safe.GetRaw();
    public static implicit operator SafeType<int>(SafeInt safe) => safe.Get();
    public static implicit operator SafeInt(int value) => new(value);
    public static implicit operator SafeInt(SafeType<int> safe) => new(safe);
}

[Serializable]
public class SafeFloat : BaseSafeValue<float>
{
    public SafeFloat(float defaultValue = 0.0f) : base(defaultValue) { }
    public static implicit operator float(SafeFloat safe) => safe.GetRaw();
    public static implicit operator SafeType<float>(SafeFloat safe) => safe.Get();
    public static implicit operator SafeFloat(float value) => new(value);
    public static implicit operator SafeFloat(SafeType<float> safe) => new(safe);
}

[Serializable]
public class SafeDouble : BaseSafeValue<double>
{
    public SafeDouble(double defaultValue = 0.0d) : base(defaultValue) { }
    public static implicit operator double(SafeDouble safe) => safe.GetRaw();
    public static implicit operator SafeType<double>(SafeDouble safe) => safe.Get();
    public static implicit operator SafeDouble(double value) => new(value);
    public static implicit operator SafeDouble(SafeType<double> safe) => new(safe);
}

[Serializable]
public class SafeVector2 : BaseSafeValue<Vector2>
{
    public SafeVector2(Vector2 defaultValue = default) : base(defaultValue) { }

    public static implicit operator Vector2(SafeVector2 safe) => safe.GetRaw();
    public static implicit operator SafeType<Vector2>(SafeVector2 safe) => safe.Get();
    public static implicit operator SafeVector2(Vector2 value) => new(value);
    public static implicit operator SafeVector2(SafeType<Vector2> safe) => new(safe);

    #region Vector2 properties

    public float magnitude => GetRaw().magnitude;
    public Vector2 normalized => GetRaw().normalized;
    public float sqrMagnitude => GetRaw().sqrMagnitude;
    public float x
    {
        get => GetRaw().x;
        set
        {
            var v = GetRaw();
            v.x = value;
            Set(v);
        }
    }
    public float y
    {
        get => GetRaw().y;
        set
        {
            var v = GetRaw();
            v.y = value;
            Set(v);
        }
    }

    #endregion
}

[Serializable]
public class SafeVector3 : BaseSafeValue<Vector3>
{
    public SafeVector3(Vector3 defaultValue = default) : base(defaultValue) { }

    public static implicit operator Vector3(SafeVector3 safe) => safe.GetRaw();
    public static implicit operator SafeType<Vector3>(SafeVector3 safe) => safe.Get();
    public static implicit operator SafeVector3(Vector3 value) => new(value);
    public static implicit operator SafeVector3(SafeType<Vector3> safe) => new(safe);

    #region Vector3 properties

    public float magnitude => GetRaw().magnitude;
    public Vector3 normalized => GetRaw().normalized;
    public float sqrMagnitude => GetRaw().sqrMagnitude;
    public float x
    {
        get => GetRaw().x;
        set
        {
            var v = GetRaw();
            v.x = value;
            Set(v);
        }
    }
    public float y
    {
        get => GetRaw().y;
        set
        {
            var v = GetRaw();
            v.y = value;
            Set(v);
        }
    }
    public float z
    {
        get => GetRaw().z;
        set
        {
            var v = GetRaw();
            v.z = value;
            Set(v);
        }
    }

    #endregion
}

[Serializable]
public class SafeQuaternion : BaseSafeValue<Quaternion>
{
    public SafeQuaternion(Quaternion defaultValue = default) : base(defaultValue) { }

    public static implicit operator Quaternion(SafeQuaternion safe) => safe.GetRaw();
    public static implicit operator SafeType<Quaternion>(SafeQuaternion safe) => safe.Get();
    public static implicit operator SafeQuaternion(Quaternion value) => new(value);
    public static implicit operator SafeQuaternion(SafeType<Quaternion> safe) => new(safe);

    #region Quaternion properties

    public Vector3 eulerAngles => GetRaw().eulerAngles;
    public Quaternion normalized => GetRaw().normalized;
    public float w
    {
        get => GetRaw().w;
        set
        {
            var v = GetRaw();
            v.w = value;
            Set(v);
        }
    }
    public float x
    {
        get => GetRaw().x;
        set
        {
            var v = GetRaw();
            v.x = value;
            Set(v);
        }
    }
    public float y
    {
        get => GetRaw().y;
        set
        {
            var v = GetRaw();
            v.y = value;
            Set(v);
        }
    }

    public float z
    {
        get => GetRaw().z;
        set
        {
            var v = GetRaw();
            v.z = value;
            Set(v);
        }
    }

    #endregion

    #region Quaternion methods

    public void Set(float newX, float newY, float newZ, float newW) => GetRaw().Set(newX, newY, newZ, newW);
    public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
        => GetRaw().SetFromToRotation(fromDirection, toDirection);
    public void SetLookRotation(Vector3 view, Vector3 up) => GetRaw().SetLookRotation(view, up);
    public void ToAngleAxis(out float angle, out Vector3 axis) => GetRaw().ToAngleAxis(out angle, out axis);

    #endregion
}