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

public interface ISafeValue<T> where T : struct
{
    T GetRaw();
    SafeType<T> Get();
    void Set(SafeType<T> value);
    void Set(T value);
    // event Action<T> OnValueChanged;
}

[Serializable]
public abstract class BaseSafeValue<T> : ISafeValue<T>, ISerializationCallbackReceiver where T : struct
{
    [SerializeField] protected T value;
    [NonSerialized] protected bool Initialized;
    [NonSerialized] protected SafeType<T> RuntimeValue;

    protected BaseSafeValue(T defaultValue = default)
    {
        value = defaultValue;
    }

    public T GetRaw()
    {
        DoInitialize();
        return value;
    }

    public SafeType<T> Get()
    {
        DoInitialize();
        return RuntimeValue;
    }

    public void Set(SafeType<T> newValue)
    {
        RuntimeValue = newValue;
        Initialized = true;
        value = newValue;
    }

    public void Set(T newValue)
    {
        RuntimeValue = new SafeType<T>(newValue);
        Initialized = true;
        value = newValue;
    }

    public void OnAfterDeserialize()
    {
        Initialized = false;
    }

    public void OnBeforeSerialize() { }

    private void DoInitialize()
    {
        if (Initialized) return;

        RuntimeValue = new SafeType<T>(value);
        Initialized = true;
    }

    public override bool Equals(object obj)
    {
        if (obj is BaseSafeValue<T> other) return value.Equals(other.value);
        return false;
    }

    protected bool Equals(BaseSafeValue<T> other)
        => value.Equals(other.value) && Equals(RuntimeValue, other.RuntimeValue) && Initialized == other.Initialized;

    public override int GetHashCode() => value.GetHashCode();

    public override string ToString() => GetRaw().ToString();
}