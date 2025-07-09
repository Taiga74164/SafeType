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

    public override bool Equals(object obj)
    {
        if (obj is SafeVector2 other) return this == other;
        if (obj is Vector2 v) return this == v;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

    #region SafeVector2 + SafeVector2 operators

    public static SafeVector2 operator +(SafeVector2 a, SafeVector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) + (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator -(SafeVector2 a, SafeVector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) - (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator *(SafeVector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Scale(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.one));
    public static SafeVector2 operator /(SafeVector2 a, SafeVector2 b)
    {
        var vecA = a?.GetRaw() ?? Vector2.zero;
        var vecB = b?.GetRaw() ?? Vector2.one;
        return new SafeVector2(new Vector2(
            !Mathf.Approximately(vecB.x, 0f) ? vecA.x / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? vecA.y / vecB.y : 0f
        ));
    }

    #endregion

    #region SafeVector2 + Vector2 operators

    public static SafeVector2 operator +(SafeVector2 a, Vector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) + b);
    public static SafeVector2 operator -(SafeVector2 a, Vector2 b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) - b);
    public static SafeVector2 operator *(SafeVector2 a, Vector2 b)
        => new SafeVector2(Vector2.Scale(a?.GetRaw() ?? Vector2.zero, b));
    public static SafeVector2 operator /(SafeVector2 a, Vector2 b)
    {
        var vecA = a?.GetRaw() ?? Vector2.zero;
        return new SafeVector2(new Vector2(
            !Mathf.Approximately(b.x, 0f) ? vecA.x / b.x : 0f,
            !Mathf.Approximately(b.y, 0f) ? vecA.y / b.y : 0f
        ));
    }

    #endregion

    #region Vector2 + SafeVector2 operators

    public static SafeVector2 operator +(Vector2 a, SafeVector2 b)
        => new SafeVector2(a + (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator -(Vector2 a, SafeVector2 b)
        => new SafeVector2(a - (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator *(Vector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Scale(a, b?.GetRaw() ?? Vector2.one));
    public static SafeVector2 operator /(Vector2 a, SafeVector2 b)
    {
        var vecB = b?.GetRaw() ?? Vector2.one;
        return new SafeVector2(new Vector2(
            !Mathf.Approximately(vecB.x, 0f) ? a.x / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? a.y / vecB.y : 0f
        ));
    }

    #endregion

    #region SafeVector2 + float operators

    public static SafeVector2 operator +(SafeVector2 a, float b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) + Vector2.one * b);
    public static SafeVector2 operator -(SafeVector2 a, float b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) - Vector2.one * b);
    public static SafeVector2 operator *(SafeVector2 a, float b)
        => new SafeVector2((a?.GetRaw() ?? Vector2.zero) * b);
    public static SafeVector2 operator /(SafeVector2 a, float b)
        => new SafeVector2(!Mathf.Approximately(b, 0f) ? (a?.GetRaw() ?? Vector2.zero) / b : Vector2.zero);

    #endregion

    #region float + SafeVector2 operators

    public static SafeVector2 operator +(float a, SafeVector2 b)
        => new SafeVector2(Vector2.one * a + (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator -(float a, SafeVector2 b)
        => new SafeVector2(Vector2.one * a - (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator *(float a, SafeVector2 b)
        => new SafeVector2(a * (b?.GetRaw() ?? Vector2.zero));
    public static SafeVector2 operator /(float a, SafeVector2 b)
    {
        var vecB = b?.GetRaw() ?? Vector2.one;
        return new SafeVector2(new Vector2(
            !Mathf.Approximately(vecB.x, 0f) ? a / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? a / vecB.y : 0f
        ));
    }

    #endregion

    #region Comparison operators

    public static bool operator ==(SafeVector2 a, SafeVector2 b)
        => Vector2.Distance(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero) < Mathf.Epsilon;
    public static bool operator !=(SafeVector2 a, SafeVector2 b) => !(a == b);

    public static bool operator ==(SafeVector2 a, Vector2 b)
        => Vector2.Distance(a?.GetRaw() ?? Vector2.zero, b) < Mathf.Epsilon;
    public static bool operator !=(SafeVector2 a, Vector2 b) => !(a == b);

    public static bool operator ==(Vector2 a, SafeVector2 b)
        => Vector2.Distance(a, b?.GetRaw() ?? Vector2.zero) < Mathf.Epsilon;
    public static bool operator !=(Vector2 a, SafeVector2 b) => !(a == b);

    #endregion

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

    public float Distance(SafeVector2 other)
        => Vector2.Distance(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    public float Distance(Vector2 other)
        => Vector2.Distance(GetRaw(), other);

    public float Dot(SafeVector2 other)
        => Vector2.Dot(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    public float Dot(Vector2 other)
        => Vector2.Dot(GetRaw(), other);

    public float Angle(SafeVector2 other)
        => Vector2.Angle(GetRaw(), other?.GetRaw() ?? Vector2.zero);

    public float Angle(Vector2 other)
        => Vector2.Angle(GetRaw(), other);

    // Additional useful methods
    public SafeVector2 Lerp(SafeVector2 target, float t)
        => new SafeVector2(Vector2.Lerp(GetRaw(), target?.GetRaw() ?? Vector2.zero, t));

    public SafeVector2 Lerp(Vector2 target, float t)
        => new SafeVector2(Vector2.Lerp(GetRaw(), target, t));

    public SafeVector2 MoveTowards(SafeVector2 target, float maxDistanceDelta)
        => new SafeVector2(Vector2.MoveTowards(GetRaw(), target?.GetRaw() ?? Vector2.zero, maxDistanceDelta));

    public SafeVector2 MoveTowards(Vector2 target, float maxDistanceDelta)
        => new SafeVector2(Vector2.MoveTowards(GetRaw(), target, maxDistanceDelta));

    public SafeVector2 Perpendicular()
        => new SafeVector2(Vector2.Perpendicular(GetRaw()));

    public SafeVector2 Reflect(Vector2 inNormal)
        => new SafeVector2(Vector2.Reflect(GetRaw(), inNormal));

    public SafeVector2 Reflect(SafeVector2 inNormal)
        => new SafeVector2(Vector2.Reflect(GetRaw(), inNormal?.GetRaw() ?? Vector2.up));

    #endregion

    #region Static helper methods

    public static SafeVector2 Zero => new SafeVector2(Vector2.zero);
    public static SafeVector2 One => new SafeVector2(Vector2.one);
    public static SafeVector2 Up => new SafeVector2(Vector2.up);
    public static SafeVector2 Down => new SafeVector2(Vector2.down);
    public static SafeVector2 Left => new SafeVector2(Vector2.left);
    public static SafeVector2 Right => new SafeVector2(Vector2.right);

    public static SafeVector2 Lerp(SafeVector2 a, SafeVector2 b, float t)
        => new SafeVector2(Vector2.Lerp(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero, t));

    public static float Distance(SafeVector2 a, SafeVector2 b)
        => Vector2.Distance(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero);

    public static float Dot(SafeVector2 a, SafeVector2 b)
        => Vector2.Dot(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero);

    public static float Angle(SafeVector2 from, SafeVector2 to)
        => Vector2.Angle(from?.GetRaw() ?? Vector2.zero, to?.GetRaw() ?? Vector2.zero);

    public static SafeVector2 Scale(SafeVector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Scale(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.one));

    public static SafeVector2 Min(SafeVector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Min(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero));

    public static SafeVector2 Max(SafeVector2 a, SafeVector2 b)
        => new SafeVector2(Vector2.Max(a?.GetRaw() ?? Vector2.zero, b?.GetRaw() ?? Vector2.zero));

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

    #region Negation operator

    public static SafeVector3 operator -(SafeVector3 a)
        => new SafeVector3(-(a?.GetRaw() ?? Vector3.zero));

    #endregion

    public override bool Equals(object obj)
    {
        if (obj is SafeVector3 other) return this == other;
        if (obj is Vector3 v) return this == v;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

    #region SafeVector3 + SafeVector3 operators

    public static SafeVector3 operator +(SafeVector3 a, SafeVector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) + (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator -(SafeVector3 a, SafeVector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) - (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator *(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Scale(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.one));
    public static SafeVector3 operator /(SafeVector3 a, SafeVector3 b)
    {
        var vecA = a?.GetRaw() ?? Vector3.zero;
        var vecB = b?.GetRaw() ?? Vector3.one;
        return new SafeVector3(new Vector3(
            !Mathf.Approximately(vecB.x, 0f) ? vecA.x / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? vecA.y / vecB.y : 0f,
            !Mathf.Approximately(vecB.z, 0f) ? vecA.z / vecB.z : 0f
        ));
    }

    #endregion

    #region SafeVector3 + Vector3 operators

    public static SafeVector3 operator +(SafeVector3 a, Vector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) + b);
    public static SafeVector3 operator -(SafeVector3 a, Vector3 b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) - b);
    public static SafeVector3 operator *(SafeVector3 a, Vector3 b)
        => new SafeVector3(Vector3.Scale(a?.GetRaw() ?? Vector3.zero, b));
    public static SafeVector3 operator /(SafeVector3 a, Vector3 b)
    {
        var vecA = a?.GetRaw() ?? Vector3.zero;
        return new SafeVector3(new Vector3(
            !Mathf.Approximately(b.x, 0f) ? vecA.x / b.x : 0f,
            !Mathf.Approximately(b.y, 0f) ? vecA.y / b.y : 0f,
            !Mathf.Approximately(b.z, 0f) ? vecA.z / b.z : 0f
        ));
    }

    #endregion

    #region Vector3 + SafeVector3 operators

    public static SafeVector3 operator +(Vector3 a, SafeVector3 b)
        => new SafeVector3(a + (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator -(Vector3 a, SafeVector3 b)
        => new SafeVector3(a - (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator *(Vector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Scale(a, b?.GetRaw() ?? Vector3.one));
    public static SafeVector3 operator /(Vector3 a, SafeVector3 b)
    {
        var vecB = b?.GetRaw() ?? Vector3.one;
        return new SafeVector3(new Vector3(
            !Mathf.Approximately(vecB.x, 0f) ? a.x / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? a.y / vecB.y : 0f,
            !Mathf.Approximately(vecB.z, 0f) ? a.z / vecB.z : 0f
        ));
    }

    #endregion

    #region SafeVector3 + float operators

    public static SafeVector3 operator +(SafeVector3 a, float b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) + Vector3.one * b);
    public static SafeVector3 operator -(SafeVector3 a, float b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) - Vector3.one * b);
    public static SafeVector3 operator *(SafeVector3 a, float b)
        => new SafeVector3((a?.GetRaw() ?? Vector3.zero) * b);
    public static SafeVector3 operator /(SafeVector3 a, float b)
        => new SafeVector3(!Mathf.Approximately(b, 0f) ? (a?.GetRaw() ?? Vector3.zero) / b : Vector3.zero);

    #endregion

    #region float + SafeVector3 operators

    public static SafeVector3 operator +(float a, SafeVector3 b)
        => new SafeVector3(Vector3.one * a + (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator -(float a, SafeVector3 b)
        => new SafeVector3(Vector3.one * a - (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator *(float a, SafeVector3 b)
        => new SafeVector3(a * (b?.GetRaw() ?? Vector3.zero));
    public static SafeVector3 operator /(float a, SafeVector3 b)
    {
        var vecB = b?.GetRaw() ?? Vector3.one;
        return new SafeVector3(new Vector3(
            !Mathf.Approximately(vecB.x, 0f) ? a / vecB.x : 0f,
            !Mathf.Approximately(vecB.y, 0f) ? a / vecB.y : 0f,
            !Mathf.Approximately(vecB.z, 0f) ? a / vecB.z : 0f
        ));
    }

    #endregion

    #region Comparison operators

    public static bool operator ==(SafeVector3 a, SafeVector3 b)
        => Vector3.Distance(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero) < Mathf.Epsilon;
    public static bool operator !=(SafeVector3 a, SafeVector3 b) => !(a == b);

    public static bool operator ==(SafeVector3 a, Vector3 b)
        => Vector3.Distance(a?.GetRaw() ?? Vector3.zero, b) < Mathf.Epsilon;
    public static bool operator !=(SafeVector3 a, Vector3 b) => !(a == b);

    public static bool operator ==(Vector3 a, SafeVector3 b)
        => Vector3.Distance(a, b?.GetRaw() ?? Vector3.zero) < Mathf.Epsilon;
    public static bool operator !=(Vector3 a, SafeVector3 b) => !(a == b);

    #endregion

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

    public void Scale(SafeVector3 scale)
    {
        Set(Vector3.Scale(GetRaw(), scale?.GetRaw() ?? Vector3.one));
    }

    public void Normalize()
    {
        Set(GetRaw().normalized);
    }

    public float Distance(SafeVector3 other)
        => Vector3.Distance(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public float Distance(Vector3 other)
        => Vector3.Distance(GetRaw(), other);

    public float Dot(SafeVector3 other)
        => Vector3.Dot(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public float Dot(Vector3 other)
        => Vector3.Dot(GetRaw(), other);

    public SafeVector3 Cross(SafeVector3 other)
        => new SafeVector3(Vector3.Cross(GetRaw(), other?.GetRaw() ?? Vector3.zero));

    public SafeVector3 Cross(Vector3 other)
        => new SafeVector3(Vector3.Cross(GetRaw(), other));

    public float Angle(SafeVector3 other)
        => Vector3.Angle(GetRaw(), other?.GetRaw() ?? Vector3.zero);

    public float Angle(Vector3 other)
        => Vector3.Angle(GetRaw(), other);

    public SafeVector3 Project(SafeVector3 onNormal)
        => new SafeVector3(Vector3.Project(GetRaw(), onNormal?.GetRaw() ?? Vector3.up));

    public SafeVector3 Project(Vector3 onNormal)
        => new SafeVector3(Vector3.Project(GetRaw(), onNormal));

    public SafeVector3 ProjectOnPlane(SafeVector3 planeNormal)
        => new SafeVector3(Vector3.ProjectOnPlane(GetRaw(), planeNormal?.GetRaw() ?? Vector3.up));

    public SafeVector3 ProjectOnPlane(Vector3 planeNormal)
        => new SafeVector3(Vector3.ProjectOnPlane(GetRaw(), planeNormal));

    public SafeVector3 Reflect(SafeVector3 inNormal)
        => new SafeVector3(Vector3.Reflect(GetRaw(), inNormal?.GetRaw() ?? Vector3.up));

    public SafeVector3 Reflect(Vector3 inNormal)
        => new SafeVector3(Vector3.Reflect(GetRaw(), inNormal));

    // Additional useful methods
    public SafeVector3 Lerp(SafeVector3 target, float t)
        => new SafeVector3(Vector3.Lerp(GetRaw(), target?.GetRaw() ?? Vector3.zero, t));

    public SafeVector3 Lerp(Vector3 target, float t)
        => new SafeVector3(Vector3.Lerp(GetRaw(), target, t));

    public SafeVector3 Slerp(SafeVector3 target, float t)
        => new SafeVector3(Vector3.Slerp(GetRaw(), target?.GetRaw() ?? Vector3.zero, t));

    public SafeVector3 Slerp(Vector3 target, float t)
        => new SafeVector3(Vector3.Slerp(GetRaw(), target, t));

    public SafeVector3 MoveTowards(SafeVector3 target, float maxDistanceDelta)
        => new SafeVector3(Vector3.MoveTowards(GetRaw(), target?.GetRaw() ?? Vector3.zero, maxDistanceDelta));

    public SafeVector3 MoveTowards(Vector3 target, float maxDistanceDelta)
        => new SafeVector3(Vector3.MoveTowards(GetRaw(), target, maxDistanceDelta));

    public SafeVector3 RotateTowards(SafeVector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
        => new SafeVector3(Vector3.RotateTowards(GetRaw(), target?.GetRaw() ?? Vector3.zero, maxRadiansDelta, maxMagnitudeDelta));

    public SafeVector3 RotateTowards(Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
        => new SafeVector3(Vector3.RotateTowards(GetRaw(), target, maxRadiansDelta, maxMagnitudeDelta));

    #endregion

    #region Static helper methods

    public static SafeVector3 Zero => new SafeVector3(Vector3.zero);
    public static SafeVector3 One => new SafeVector3(Vector3.one);
    public static SafeVector3 Up => new SafeVector3(Vector3.up);
    public static SafeVector3 Down => new SafeVector3(Vector3.down);
    public static SafeVector3 Left => new SafeVector3(Vector3.left);
    public static SafeVector3 Right => new SafeVector3(Vector3.right);
    public static SafeVector3 Forward => new SafeVector3(Vector3.forward);
    public static SafeVector3 Back => new SafeVector3(Vector3.back);

    public static SafeVector3 Lerp(SafeVector3 a, SafeVector3 b, float t)
        => new SafeVector3(Vector3.Lerp(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero, t));

    public static SafeVector3 LerpUnclamped(SafeVector3 a, SafeVector3 b, float t)
        => new SafeVector3(Vector3.LerpUnclamped(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero, t));

    public static SafeVector3 Slerp(SafeVector3 a, SafeVector3 b, float t)
        => new SafeVector3(Vector3.Slerp(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero, t));

    public static SafeVector3 SlerpUnclamped(SafeVector3 a, SafeVector3 b, float t)
        => new SafeVector3(Vector3.SlerpUnclamped(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero, t));

    public static float Distance(SafeVector3 a, SafeVector3 b)
        => Vector3.Distance(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero);

    public static float Dot(SafeVector3 a, SafeVector3 b)
        => Vector3.Dot(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero);

    public static SafeVector3 Cross(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Cross(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero));

    public static float Angle(SafeVector3 from, SafeVector3 to)
        => Vector3.Angle(from?.GetRaw() ?? Vector3.zero, to?.GetRaw() ?? Vector3.zero);

    public static float SignedAngle(SafeVector3 from, SafeVector3 to, SafeVector3 axis)
        => Vector3.SignedAngle(from?.GetRaw() ?? Vector3.zero, to?.GetRaw() ?? Vector3.zero, axis?.GetRaw() ?? Vector3.up);

    public static SafeVector3 Scale(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Scale(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.one));

    public static SafeVector3 Min(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Min(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero));

    public static SafeVector3 Max(SafeVector3 a, SafeVector3 b)
        => new SafeVector3(Vector3.Max(a?.GetRaw() ?? Vector3.zero, b?.GetRaw() ?? Vector3.zero));

    public static SafeVector3 Project(SafeVector3 vector, SafeVector3 onNormal)
        => new SafeVector3(Vector3.Project(vector?.GetRaw() ?? Vector3.zero, onNormal?.GetRaw() ?? Vector3.up));

    public static SafeVector3 ProjectOnPlane(SafeVector3 vector, SafeVector3 planeNormal)
        => new SafeVector3(Vector3.ProjectOnPlane(vector?.GetRaw() ?? Vector3.zero, planeNormal?.GetRaw() ?? Vector3.up));

    public static SafeVector3 Reflect(SafeVector3 inDirection, SafeVector3 inNormal)
        => new SafeVector3(Vector3.Reflect(inDirection?.GetRaw() ?? Vector3.zero, inNormal?.GetRaw() ?? Vector3.up));

    public static SafeVector3 MoveTowards(SafeVector3 current, SafeVector3 target, float maxDistanceDelta)
        => new SafeVector3(Vector3.MoveTowards(current?.GetRaw() ?? Vector3.zero, target?.GetRaw() ?? Vector3.zero,
            maxDistanceDelta));

    public static SafeVector3 RotateTowards(SafeVector3 current, SafeVector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
        => new SafeVector3(Vector3.RotateTowards(current?.GetRaw() ?? Vector3.zero, target?.GetRaw() ?? Vector3.zero, maxRadiansDelta,
            maxMagnitudeDelta));

    public static SafeVector3 SmoothDamp(SafeVector3 current, SafeVector3 target, ref Vector3 currentVelocity, float smoothTime)
        => new SafeVector3(Vector3.SmoothDamp(current?.GetRaw() ?? Vector3.zero, target?.GetRaw() ?? Vector3.zero,
            ref currentVelocity, smoothTime));

    public static SafeVector3 SmoothDamp(SafeVector3 current, SafeVector3 target, ref Vector3 currentVelocity, float smoothTime,
        float maxSpeed)
        => new SafeVector3(Vector3.SmoothDamp(current?.GetRaw() ?? Vector3.zero, target?.GetRaw() ?? Vector3.zero,
            ref currentVelocity, smoothTime, maxSpeed));

    public static SafeVector3 SmoothDamp(SafeVector3 current, SafeVector3 target, ref Vector3 currentVelocity, float smoothTime,
        float maxSpeed, float deltaTime)
        => new SafeVector3(Vector3.SmoothDamp(current?.GetRaw() ?? Vector3.zero, target?.GetRaw() ?? Vector3.zero,
            ref currentVelocity, smoothTime, maxSpeed, deltaTime));

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

    #region float + SafeQuaternion operators

    public static SafeQuaternion operator *(float a, SafeQuaternion b)
    {
        var quatB = b?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(a * quatB.x, a * quatB.y, a * quatB.z, a * quatB.w));
    }

    #endregion

    public override bool Equals(object obj)
    {
        if (obj is SafeQuaternion other) return this == other;
        if (obj is Quaternion q) return this == q;
        return false;
    }

    public override int GetHashCode() => Get().GetHashCode();

    #region SafeQuaternion + SafeQuaternion operators

    public static SafeQuaternion operator +(SafeQuaternion a, SafeQuaternion b)
    {
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        var quatB = b?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x + quatB.x, quatA.y + quatB.y, quatA.z + quatB.z, quatA.w + quatB.w));
    }

    public static SafeQuaternion operator -(SafeQuaternion a, SafeQuaternion b)
    {
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        var quatB = b?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x - quatB.x, quatA.y - quatB.y, quatA.z - quatB.z, quatA.w - quatB.w));
    }

    public static SafeQuaternion operator *(SafeQuaternion a, SafeQuaternion b)
        => new SafeQuaternion((a?.GetRaw() ?? Quaternion.identity) * (b?.GetRaw() ?? Quaternion.identity));

    #endregion

    #region SafeQuaternion + Quaternion operators

    public static SafeQuaternion operator +(SafeQuaternion a, Quaternion b)
    {
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x + b.x, quatA.y + b.y, quatA.z + b.z, quatA.w + b.w));
    }

    public static SafeQuaternion operator -(SafeQuaternion a, Quaternion b)
    {
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x - b.x, quatA.y - b.y, quatA.z - b.z, quatA.w - b.w));
    }

    public static SafeQuaternion operator *(SafeQuaternion a, Quaternion b)
        => new SafeQuaternion((a?.GetRaw() ?? Quaternion.identity) * b);

    #endregion

    #region Quaternion + SafeQuaternion operators

    public static SafeQuaternion operator +(Quaternion a, SafeQuaternion b)
    {
        var quatB = b?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(a.x + quatB.x, a.y + quatB.y, a.z + quatB.z, a.w + quatB.w));
    }

    public static SafeQuaternion operator -(Quaternion a, SafeQuaternion b)
    {
        var quatB = b?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(a.x - quatB.x, a.y - quatB.y, a.z - quatB.z, a.w - quatB.w));
    }

    public static SafeQuaternion operator *(Quaternion a, SafeQuaternion b)
        => new SafeQuaternion(a * (b?.GetRaw() ?? Quaternion.identity));

    #endregion

    #region SafeQuaternion + float operators

    public static SafeQuaternion operator *(SafeQuaternion a, float b)
    {
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x * b, quatA.y * b, quatA.z * b, quatA.w * b));
    }

    public static SafeQuaternion operator /(SafeQuaternion a, float b)
    {
        if (Mathf.Approximately(b, 0f)) return new SafeQuaternion(Quaternion.identity);
        var quatA = a?.GetRaw() ?? Quaternion.identity;
        return new SafeQuaternion(new Quaternion(quatA.x / b, quatA.y / b, quatA.z / b, quatA.w / b));
    }

    #endregion

    #region Vector rotation operators

    public static SafeVector3 operator *(SafeQuaternion rotation, SafeVector3 point)
        => new SafeVector3((rotation?.GetRaw() ?? Quaternion.identity) * (point?.GetRaw() ?? Vector3.zero));

    public static SafeVector3 operator *(SafeQuaternion rotation, Vector3 point)
        => new SafeVector3((rotation?.GetRaw() ?? Quaternion.identity) * point);

    #endregion

    #region Comparison operators

    public static bool operator ==(SafeQuaternion a, SafeQuaternion b)
        => Quaternion.Angle(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity) < Mathf.Epsilon;
    public static bool operator !=(SafeQuaternion a, SafeQuaternion b) => !(a == b);

    public static bool operator ==(SafeQuaternion a, Quaternion b)
        => Quaternion.Angle(a?.GetRaw() ?? Quaternion.identity, b) < Mathf.Epsilon;
    public static bool operator !=(SafeQuaternion a, Quaternion b) => !(a == b);

    public static bool operator ==(Quaternion a, SafeQuaternion b)
        => Quaternion.Angle(a, b?.GetRaw() ?? Quaternion.identity) < Mathf.Epsilon;
    public static bool operator !=(Quaternion a, SafeQuaternion b) => !(a == b);

    #endregion

    #region Quaternion properties

    public Vector3 eulerAngles
    {
        get => GetRaw().eulerAngles;
        set => Set(Quaternion.Euler(value));
    }

    public SafeVector3 safeEulerAngles
    {
        get => new SafeVector3(GetRaw().eulerAngles);
        set => Set(Quaternion.Euler(value?.GetRaw() ?? Vector3.zero));
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

    public void SetFromToRotation(SafeVector3 fromDirection, SafeVector3 toDirection)
    {
        Set(Quaternion.FromToRotation(fromDirection?.GetRaw() ?? Vector3.forward, toDirection?.GetRaw() ?? Vector3.forward));
    }

    public void SetLookRotation(Vector3 view)
    {
        Set(Quaternion.LookRotation(view));
    }

    public void SetLookRotation(Vector3 view, Vector3 up)
    {
        Set(Quaternion.LookRotation(view, up));
    }

    public void SetLookRotation(SafeVector3 view)
    {
        Set(Quaternion.LookRotation(view?.GetRaw() ?? Vector3.forward));
    }

    public void SetLookRotation(SafeVector3 view, SafeVector3 up)
    {
        Set(Quaternion.LookRotation(view?.GetRaw() ?? Vector3.forward, up?.GetRaw() ?? Vector3.up));
    }

    public void ToAngleAxis(out float angle, out Vector3 axis)
    {
        GetRaw().ToAngleAxis(out angle, out axis);
    }

    public void ToAngleAxis(out float angle, out SafeVector3 axis)
    {
        GetRaw().ToAngleAxis(out angle, out var tempAxis);
        axis = new SafeVector3(tempAxis);
    }

    public void Normalize()
    {
        Set(GetRaw().normalized);
    }

    public SafeQuaternion Inverse() => new SafeQuaternion(Quaternion.Inverse(GetRaw()));

    public float Angle(SafeQuaternion other)
        => Quaternion.Angle(GetRaw(), other?.GetRaw() ?? Quaternion.identity);

    public float Angle(Quaternion other)
        => Quaternion.Angle(GetRaw(), other);

    public float Dot(SafeQuaternion other)
        => Quaternion.Dot(GetRaw(), other?.GetRaw() ?? Quaternion.identity);

    public float Dot(Quaternion other)
        => Quaternion.Dot(GetRaw(), other);

    public SafeQuaternion Slerp(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.Slerp(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion Slerp(Quaternion target, float t)
        => new SafeQuaternion(Quaternion.Slerp(GetRaw(), target, t));

    public SafeQuaternion Lerp(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.Lerp(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion Lerp(Quaternion target, float t)
        => new SafeQuaternion(Quaternion.Lerp(GetRaw(), target, t));

    public SafeQuaternion SlerpUnclamped(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.SlerpUnclamped(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion SlerpUnclamped(Quaternion target, float t)
        => new SafeQuaternion(Quaternion.SlerpUnclamped(GetRaw(), target, t));

    public SafeQuaternion LerpUnclamped(SafeQuaternion target, float t)
        => new SafeQuaternion(Quaternion.LerpUnclamped(GetRaw(), target?.GetRaw() ?? Quaternion.identity, t));

    public SafeQuaternion LerpUnclamped(Quaternion target, float t)
        => new SafeQuaternion(Quaternion.LerpUnclamped(GetRaw(), target, t));

    public SafeQuaternion RotateTowards(SafeQuaternion target, float maxDegreesDelta)
        => new SafeQuaternion(Quaternion.RotateTowards(GetRaw(), target?.GetRaw() ?? Quaternion.identity, maxDegreesDelta));

    public SafeQuaternion RotateTowards(Quaternion target, float maxDegreesDelta)
        => new SafeQuaternion(Quaternion.RotateTowards(GetRaw(), target, maxDegreesDelta));

    #endregion

    #region Static helper methods

    public static SafeQuaternion Identity => new SafeQuaternion(Quaternion.identity);

    public static SafeQuaternion Euler(float x, float y, float z)
        => new SafeQuaternion(Quaternion.Euler(x, y, z));

    public static SafeQuaternion Euler(Vector3 euler)
        => new SafeQuaternion(Quaternion.Euler(euler));

    public static SafeQuaternion Euler(SafeVector3 euler)
        => new SafeQuaternion(Quaternion.Euler(euler?.GetRaw() ?? Vector3.zero));

    public static SafeQuaternion AngleAxis(float angle, Vector3 axis)
        => new SafeQuaternion(Quaternion.AngleAxis(angle, axis));

    public static SafeQuaternion AngleAxis(float angle, SafeVector3 axis)
        => new SafeQuaternion(Quaternion.AngleAxis(angle, axis?.GetRaw() ?? Vector3.up));

    public static SafeQuaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        => new SafeQuaternion(Quaternion.FromToRotation(fromDirection, toDirection));

    public static SafeQuaternion FromToRotation(SafeVector3 fromDirection, SafeVector3 toDirection)
        => new SafeQuaternion(Quaternion.FromToRotation(fromDirection?.GetRaw() ?? Vector3.forward,
            toDirection?.GetRaw() ?? Vector3.forward));

    public static SafeQuaternion LookRotation(Vector3 forward)
        => new SafeQuaternion(Quaternion.LookRotation(forward));

    public static SafeQuaternion LookRotation(Vector3 forward, Vector3 upwards)
        => new SafeQuaternion(Quaternion.LookRotation(forward, upwards));

    public static SafeQuaternion LookRotation(SafeVector3 forward)
        => new SafeQuaternion(Quaternion.LookRotation(forward?.GetRaw() ?? Vector3.forward));

    public static SafeQuaternion LookRotation(SafeVector3 forward, SafeVector3 upwards)
        => new SafeQuaternion(Quaternion.LookRotation(forward?.GetRaw() ?? Vector3.forward, upwards?.GetRaw() ?? Vector3.up));

    public static SafeQuaternion Lerp(SafeQuaternion a, SafeQuaternion b, float t)
        => new SafeQuaternion(Quaternion.Lerp(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity, t));

    public static SafeQuaternion LerpUnclamped(SafeQuaternion a, SafeQuaternion b, float t)
        => new SafeQuaternion(Quaternion.LerpUnclamped(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity, t));

    public static SafeQuaternion Slerp(SafeQuaternion a, SafeQuaternion b, float t)
        => new SafeQuaternion(Quaternion.Slerp(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity, t));

    public static SafeQuaternion SlerpUnclamped(SafeQuaternion a, SafeQuaternion b, float t)
        => new SafeQuaternion(Quaternion.SlerpUnclamped(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity, t));

    public static float Angle(SafeQuaternion a, SafeQuaternion b)
        => Quaternion.Angle(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity);

    public static float Dot(SafeQuaternion a, SafeQuaternion b)
        => Quaternion.Dot(a?.GetRaw() ?? Quaternion.identity, b?.GetRaw() ?? Quaternion.identity);

    public static SafeQuaternion Inverse(SafeQuaternion rotation)
        => new SafeQuaternion(Quaternion.Inverse(rotation?.GetRaw() ?? Quaternion.identity));

    public static SafeQuaternion RotateTowards(SafeQuaternion from, SafeQuaternion to, float maxDegreesDelta)
        => new SafeQuaternion(Quaternion.RotateTowards(from?.GetRaw() ?? Quaternion.identity, to?.GetRaw() ?? Quaternion.identity,
            maxDegreesDelta));

    public static SafeQuaternion Normalize(SafeQuaternion q)
        => new SafeQuaternion(Quaternion.Normalize(q?.GetRaw() ?? Quaternion.identity));

    #endregion
}