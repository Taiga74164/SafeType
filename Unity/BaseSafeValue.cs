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
    [NonSerialized] private readonly object _lock = new object();
    [NonSerialized] private volatile bool _initialized;
    [NonSerialized] private SafeType<T> _runtimeValue;

    protected BaseSafeValue(T defaultValue = default)
    {
        value = defaultValue;
        _initialized = false;
    }

    public T GetRaw()
    {
        DoInitialize();
        return value;
    }

    public SafeType<T> Get()
    {
        DoInitialize();
        return _runtimeValue;
    }

    public void Set(SafeType<T> newValue)
    {
        lock (_lock)
        {
            _runtimeValue = newValue;
            _initialized = true;
            value = newValue;
        }
    }

    public void Set(T newValue)
    {
        lock (_lock)
        {
            _runtimeValue = new SafeType<T>(newValue);
            _initialized = true;
            value = newValue;
        }
    }

    public void OnAfterDeserialize()
    {
        // Reset runtime value on deserialization
        lock (_lock)
        {
            _initialized = false;
        }
    }

    public void OnBeforeSerialize()
    {
        // Ensure value is synchronized before serialization
        if (_initialized)
        {
            value = _runtimeValue;
        }
    }

    private void DoInitialize()
    {
        if (_initialized) return;

        lock (_lock)
        {
            if (_initialized) return;

            try
            {
                _runtimeValue = new SafeType<T>(value);
                _initialized = true;
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.LogError($"Failed to initialize SafeValue<{typeof(T).Name}>: {ex.Message}");
#endif
                _runtimeValue = new SafeType<T>(default);
                _initialized = true;
            }
        }
    }

    public override bool Equals(object obj)
    {
        return obj switch
        {
            BaseSafeValue<T> other => value.Equals(other.value),
            T directValue => value.Equals(directValue),
            _ => false
        };

    }

    protected bool Equals(BaseSafeValue<T> other)
    {
        if (other == null) return false;
        return value.Equals(other.value) &&
               _initialized == other._initialized;
    }

    public override int GetHashCode() => value.GetHashCode();

    public override string ToString() => GetRaw().ToString();
}