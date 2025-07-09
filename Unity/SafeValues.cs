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

    public static implicit operator bool(SafeBool safe) => safe?.GetRaw() ?? false;
    public static implicit operator SafeType<bool>(SafeBool safe) => safe?.Get() ?? new SafeType<bool>(false);
    public static implicit operator SafeBool(bool value) => new SafeBool(value);
    public static implicit operator SafeBool(SafeType<bool> safe) => new SafeBool(safe);

    // Boolean operators
    public static bool operator ==(SafeBool a, SafeBool b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.GetRaw() == b.GetRaw();
    }

    public static bool operator !=(SafeBool a, SafeBool b) => !(a == b);
    public static bool operator ==(SafeBool a, bool b) => a?.GetRaw() == b;
    public static bool operator !=(SafeBool a, bool b) => a?.GetRaw() != b;
    public static bool operator ==(bool a, SafeBool b) => a == b?.GetRaw();
    public static bool operator !=(bool a, SafeBool b) => a != b?.GetRaw();

    // Logical operators
    public static SafeBool operator &(SafeBool a, SafeBool b) => new SafeBool((a?.GetRaw() ?? false) & (b?.GetRaw() ?? false));
    public static SafeBool operator |(SafeBool a, SafeBool b) => new SafeBool((a?.GetRaw() ?? false) | (b?.GetRaw() ?? false));
    public static SafeBool operator ^(SafeBool a, SafeBool b) => new SafeBool((a?.GetRaw() ?? false) ^ (b?.GetRaw() ?? false));
    public static SafeBool operator !(SafeBool a) => new SafeBool(!(a?.GetRaw() ?? false));

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

    public static implicit operator int(SafeInt safe) => safe?.GetRaw() ?? 0;
    public static implicit operator SafeType<int>(SafeInt safe) => safe?.Get() ?? new SafeType<int>(0);
    public static implicit operator SafeInt(int value) => new SafeInt(value);
    public static implicit operator SafeInt(SafeType<int> safe) => new SafeInt(safe);

    // Arithmetic operators
    public static SafeInt operator +(SafeInt a, SafeInt b) => new SafeInt((a?.GetRaw() ?? 0) + (b?.GetRaw() ?? 0));
    public static SafeInt operator -(SafeInt a, SafeInt b) => new SafeInt((a?.GetRaw() ?? 0) - (b?.GetRaw() ?? 0));
    public static SafeInt operator *(SafeInt a, SafeInt b) => new SafeInt((a?.GetRaw() ?? 0) * (b?.GetRaw() ?? 0));
    public static SafeInt operator /(SafeInt a, SafeInt b) => new SafeInt((a?.GetRaw() ?? 0) / Math.Max(1, b?.GetRaw() ?? 1));

    // Comparison operators
    public static bool operator ==(SafeInt a, SafeInt b) => (a?.GetRaw() ?? 0) == (b?.GetRaw() ?? 0);
    public static bool operator !=(SafeInt a, SafeInt b) => !(a == b);
    public static bool operator <(SafeInt a, SafeInt b) => (a?.GetRaw() ?? 0) < (b?.GetRaw() ?? 0);
    public static bool operator >(SafeInt a, SafeInt b) => (a?.GetRaw() ?? 0) > (b?.GetRaw() ?? 0);
    public static bool operator <=(SafeInt a, SafeInt b) => (a?.GetRaw() ?? 0) <= (b?.GetRaw() ?? 0);
    public static bool operator >=(SafeInt a, SafeInt b) => (a?.GetRaw() ?? 0) >= (b?.GetRaw() ?? 0);

    public override bool Equals(object obj)
    {
        if (obj is SafeInt other) return this == other;
        if (obj is int i) return GetRaw() == i;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();
}

[Serializable]
public class SafeFloat : BaseSafeValue<float>
{
    public SafeFloat(float defaultValue = 0.0f) : base(defaultValue) { }

    public static implicit operator float(SafeFloat safe) => safe?.GetRaw() ?? 0.0f;
    public static implicit operator SafeType<float>(SafeFloat safe) => safe?.Get() ?? new SafeType<float>(0.0f);
    public static implicit operator SafeFloat(float value) => new SafeFloat(value);
    public static implicit operator SafeFloat(SafeType<float> safe) => new SafeFloat(safe);

    // Arithmetic operators
    public static SafeFloat operator +(SafeFloat a, SafeFloat b) => new SafeFloat((a?.GetRaw() ?? 0.0f) + (b?.GetRaw() ?? 0.0f));
    public static SafeFloat operator -(SafeFloat a, SafeFloat b) => new SafeFloat((a?.GetRaw() ?? 0.0f) - (b?.GetRaw() ?? 0.0f));
    public static SafeFloat operator *(SafeFloat a, SafeFloat b) => new SafeFloat((a?.GetRaw() ?? 0.0f) * (b?.GetRaw() ?? 0.0f));
    public static SafeFloat operator /(SafeFloat a, SafeFloat b)
    {
        var bVal = b?.GetRaw() ?? 1.0f;
        return new SafeFloat(!Mathf.Approximately(bVal, 0f) ? (a?.GetRaw() ?? 0.0f) / bVal : 0.0f);
    }

    // Comparison operators
    public static bool operator ==(SafeFloat a, SafeFloat b) => Mathf.Approximately(a?.GetRaw() ?? 0.0f, b?.GetRaw() ?? 0.0f);
    public static bool operator !=(SafeFloat a, SafeFloat b) => !(a == b);
    public static bool operator <(SafeFloat a, SafeFloat b) => (a?.GetRaw() ?? 0.0f) < (b?.GetRaw() ?? 0.0f);
    public static bool operator >(SafeFloat a, SafeFloat b) => (a?.GetRaw() ?? 0.0f) > (b?.GetRaw() ?? 0.0f);
    public static bool operator <=(SafeFloat a, SafeFloat b) => (a?.GetRaw() ?? 0.0f) <= (b?.GetRaw() ?? 0.0f);
    public static bool operator >=(SafeFloat a, SafeFloat b) => (a?.GetRaw() ?? 0.0f) >= (b?.GetRaw() ?? 0.0f);

    public override bool Equals(object obj)
    {
        if (obj is SafeFloat other) return this == other;
        if (obj is float f) return Mathf.Approximately(GetRaw(), f);
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();
}

[Serializable]
public class SafeDouble : BaseSafeValue<double>
{
    public SafeDouble(double defaultValue = 0.0d) : base(defaultValue) { }

    public static implicit operator double(SafeDouble safe) => safe?.GetRaw() ?? 0.0d;
    public static implicit operator SafeType<double>(SafeDouble safe) => safe?.Get() ?? new SafeType<double>(0.0d);
    public static implicit operator SafeDouble(double value) => new SafeDouble(value);
    public static implicit operator SafeDouble(SafeType<double> safe) => new SafeDouble(safe);

    // Arithmetic operators
    public static SafeDouble operator +(SafeDouble a, SafeDouble b) => new SafeDouble((a?.GetRaw() ?? 0.0d) + (b?.GetRaw() ?? 0.0d));
    public static SafeDouble operator -(SafeDouble a, SafeDouble b) => new SafeDouble((a?.GetRaw() ?? 0.0d) - (b?.GetRaw() ?? 0.0d));
    public static SafeDouble operator *(SafeDouble a, SafeDouble b) => new SafeDouble((a?.GetRaw() ?? 0.0d) * (b?.GetRaw() ?? 0.0d));
    public static SafeDouble operator /(SafeDouble a, SafeDouble b)
    {
        var bVal = b?.GetRaw() ?? 1.0d;
        return new SafeDouble(Math.Abs(bVal) > double.Epsilon ? (a?.GetRaw() ?? 0.0d) / bVal : 0.0d);
    }

    // Comparison operators
    public static bool operator ==(SafeDouble a, SafeDouble b)
        => Math.Abs((a?.GetRaw() ?? 0.0d) - (b?.GetRaw() ?? 0.0d)) < double.Epsilon;
    public static bool operator !=(SafeDouble a, SafeDouble b) => !(a == b);
    public static bool operator <(SafeDouble a, SafeDouble b) => (a?.GetRaw() ?? 0.0d) < (b?.GetRaw() ?? 0.0d);
    public static bool operator >(SafeDouble a, SafeDouble b) => (a?.GetRaw() ?? 0.0d) > (b?.GetRaw() ?? 0.0d);
    public static bool operator <=(SafeDouble a, SafeDouble b) => (a?.GetRaw() ?? 0.0d) <= (b?.GetRaw() ?? 0.0d);
    public static bool operator >=(SafeDouble a, SafeDouble b) => (a?.GetRaw() ?? 0.0d) >= (b?.GetRaw() ?? 0.0d);

    public override bool Equals(object obj)
    {
        if (obj is SafeDouble other) return this == other;
        if (obj is double d) return Math.Abs(GetRaw() - d) < double.Epsilon;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();
}

[Serializable]
public class SafeVector2 : BaseSafeValue<Vector2>
{
    public SafeVector2(Vector2 defaultValue = default) : base(defaultValue) { }

    public static implicit operator Vector2(SafeVector2 safe) => safe?.GetRaw() ?? Vector2.zero;
    public static implicit operator SafeType<Vector2>(SafeVector2 safe) => safe?.Get() ?? new SafeType<Vector2>(Vector2.zero);
    public static implicit operator SafeVector2(Vector2 value) => new SafeVector2(value);
    public static implicit operator SafeVector2(SafeType<Vector2> safe) => new SafeVector2(safe);

    // Arithmetic operators
    public static SafeVector2 operator +(SafeVector2 a, SafeVector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) + (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator -(SafeVector2 a, SafeVector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) - (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator *(SafeVector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Scale(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.one));
    public static SafeVector2 operator *(SafeVector2 a, float b) => new SafeVector2((a?.GetRaw() ?? Vector2.zero) * b);
    public static SafeVector2 operator *(float a, SafeVector2 b) => new SafeVector2(a * (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator /(SafeVector2 a, float b)
        => new SafeVector2(!Mathf.Approximately(b, 0f) ? (a?.GetRaw() ?? Vector2.zero) / b : Vector2.zero);

    // Comparison operators
    public static bool operator ==(SafeVector2 a, SafeVector2 b)
        => Vector2.Distance(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero) < Mathf.Epsilon;
    public static bool operator !=(SafeVector2 a, SafeVector2 b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is SafeVector2 other) return this == other;
        if (obj is Vector2 v) return Vector2.Distance(GetRaw(), v) < Mathf.Epsilon;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

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

    #region Vector2 methods

    public void Set(float newX, float newY)
    {
        Set(new Vector2(newX, newY));
    }

    public void Scale(Vector2 scale)
    {
        Set(Vector2.Scale(GetRaw(), scale));
    }

    public void Normalize()
    {
        Set(GetRaw().normalized);
    }

    public float Distance(SafeVector2 other) => Vector2.Distance(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    public float Dot(SafeVector2 other) => Vector2.Dot(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    public float Angle(SafeVector2 other) => Vector2.Angle(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    #endregion
}

[Serializable]
public class SafeVector3 : BaseSafeValue<Vector3>
{
    public SafeVector3(Vector3 defaultValue = default) : base(defaultValue) { }

    public static implicit operator Vector3(SafeVector3 safe) => safe?.GetRaw() ?? Vector3.zero;
    public static implicit operator SafeType<Vector3>(SafeVector3 safe) => safe?.Get() ?? new SafeType<Vector3>(Vector3.zero);
    public static implicit operator SafeVector3(Vector3 value) => new SafeVector3(value);
    public static implicit operator SafeVector3(SafeType<Vector3> safe) => new SafeVector3(safe);

    // Arithmetic operators
    public static SafeVector3 operator +(SafeVector3 a, SafeVector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) + (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator -(SafeVector3 a, SafeVector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) - (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator *(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Scale(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.one));
    public static SafeVector3 operator *(SafeVector3 a, float b) => new SafeVector3((a?.GetRaw() ?? Vector3.zero) * b);
    public static SafeVector3 operator *(float a, SafeVector3 b) => new SafeVector3(a * (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator /(SafeVector3 a, float b)
        => new SafeVector3(!Mathf.Approximately(b, 0f) ? (a?.GetRaw() ?? Vector3.zero) / b : Vector3.zero);
    public static SafeVector3 operator -(SafeVector3 a) => new SafeVector3(-(a?.GetRaw() ?? Vector3.zero));

    // Comparison operators
    public static bool operator ==(SafeVector3 a, SafeVector3 b)
        => Vector3.Distance(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero) < Mathf.Epsilon;
    public static bool operator !=(SafeVector3 a, SafeVector3 b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is SafeVector3 other) return this == other;
        if (obj is Vector3 v) return Vector3.Distance(GetRaw(), v) < Mathf.Epsilon;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

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

    #region Vector3 methods

    public void Set(float newX, float newY, float newZ)
    {
        Set(new Vector3(newX, newY, newZ));
    }

    public void Scale(Vector3 scale)
    {
        Set(Vector3.Scale(GetRaw(), scale));
    }

    public void Normalize()
    {
        Set(GetRaw().normalized);
    }

    public float Distance(SafeVector3 other) => Vector3.Distance(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public float Dot(SafeVector3 other) => Vector3.Dot(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public SafeVector3 Cross(SafeVector3 other) => new SafeVector3(Vector3.Cross(GetRaw(), other?.GetRaw() ?? Vector3.zero));

    public float Angle(SafeVector3 other) => Vector3.Angle(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public SafeVector3 Project(SafeVector3 onNormal) => new SafeVector3(Vector3.Project(GetRaw(), onNormal?.GetRaw() ?? Vector3.up));

    public SafeVector3 Reflect(SafeVector3 inNormal) => new SafeVector3(Vector3.Reflect(GetRaw(), inNormal?.GetRaw() ?? Vector3.up));

    #endregion
}

[Serializable]
public class SafeQuaternion : BaseSafeValue<Quaternion>
{
    public SafeQuaternion(Quaternion defaultValue = default) : base(defaultValue) { }

    public static implicit operator Quaternion(SafeQuaternion safe) => safe?.GetRaw() ?? Quaternion.identity;
    public static implicit operator SafeType<Quaternion>(SafeQuaternion safe)
        => safe?.Get() ?? new SafeType<Quaternion>(Quaternion.identity);
    public static implicit operator SafeQuaternion(Quaternion value) => new SafeQuaternion(value);
    public static implicit operator SafeQuaternion(SafeType<Quaternion> safe) => new SafeQuaternion(safe);

    // Quaternion operators
    public static SafeQuaternion operator *(SafeQuaternion a, SafeQuaternion b)
        => new SafeQuaternion((a?.GetRaw() ?? Quaternion.identity) * (b?.GetRaw() ?? Quaternion.identity));
    public static SafeVector3 operator *(SafeQuaternion rotation, SafeVector3 point)
        => new SafeVector3((rotation?.GetRaw() ?? Quaternion.identity) * (point?.GetRaw() ?? Vector3.zero));

    // Comparison operators
    public static bool operator ==(SafeQuaternion a, SafeQuaternion b)
        => Quaternion.Angle(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity) < Mathf.Epsilon;
    public static bool operator !=(SafeQuaternion a, SafeQuaternion b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is SafeQuaternion other) return this == other;
        if (obj is Quaternion q) return Quaternion.Angle(GetRaw(), q) < Mathf.Epsilon;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

    #region Quaternion properties

    public Vector3 eulerAngles
    {
        get => GetRaw().eulerAngles;
        set => Set(Quaternion.Euler(value));
    }

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

    public void Set(float newX, float newY, float newZ, float newW)
    {
        Set(new Quaternion(newX, newY, newZ, newW));
    }

    public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
    {
        Set(Quaternion.FromToRotation(fromDirection, toDirection));
    }

    public void SetLookRotation(Vector3 view)
    {
        Set(Quaternion.LookRotation(view));
    }

    public void SetLookRotation(Vector3 view, Vector3 up)
    {
        Set(Quaternion.LookRotation(view, up));
    }

    public void ToAngleAxis(out float angle, out Vector3 axis)
    {
        GetRaw().ToAngleAxis(out angle, out axis);
    }

    public void Normalize()
    {
        Set(GetRaw().normalized);
    }

    public SafeQuaternion Inverse() => new SafeQuaternion(Quaternion.Inverse(GetRaw()));

    public float Angle(SafeQuaternion other) => Quaternion.Angle(GetRaw(), other?.GetRaw() ?? Quaternion.identity);

    public float Dot(SafeQuaternion other) => Quaternion.Dot(GetRaw(), other?.GetRaw() ?? Quaternion.identity);

    public SafeQuaternion Slerp(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.Slerp(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion Lerp(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.Lerp(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion SlerpUnclamped(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.SlerpUnclamped(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion LerpUnclamped(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.LerpUnclamped(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion RotateTowards(SafeQuaternion target, float maxDegreesDelta)
        => new SafeQuaternion(Quaternion.RotateTowards(GetRaw(), target?.GetRaw() ?? Quaternion.identity, maxDegreesDelta));

    #endregion

    #region Static helper methods

    public static SafeQuaternion Euler(float x, float y, float z) => new SafeQuaternion(Quaternion.Euler(x, y, z));

    public static SafeQuaternion Euler(Vector3 euler) => new SafeQuaternion(Quaternion.Euler(euler));

    public static SafeQuaternion AngleAxis(float angle, Vector3 axis) => new SafeQuaternion(Quaternion.AngleAxis(angle, axis));

    public static SafeQuaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        => new SafeQuaternion(Quaternion.FromToRotation(fromDirection, toDirection));

    public static SafeQuaternion LookRotation(Vector3 forward) => new SafeQuaternion(Quaternion.LookRotation(forward));

    public static SafeQuaternion LookRotation(Vector3 forward, Vector3 upwards)
        => new SafeQuaternion(Quaternion.LookRotation(forward, upwards));

    public static SafeQuaternion Identity => new SafeQuaternion(Quaternion.identity);

    #endregion
}