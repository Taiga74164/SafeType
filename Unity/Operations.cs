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
using UnityEngine;

public interface INumeric<T> where T : struct
{
    T Add(T a, T b);
    T Subtract(T a, T b);
    T Multiply(T a, T b);
    T Divide(T a, T b);
    bool LessThan(T a, T b);
    bool GreaterThan(T a, T b);
}

public class IntOperations : INumeric<int>
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public int Divide(int a, int b) => a / b;
    public bool LessThan(int a, int b) => a < b;
    public bool GreaterThan(int a, int b) => a > b;
}

public class FloatOperations : INumeric<float>
{
    public float Add(float a, float b) => a + b;
    public float Subtract(float a, float b) => a - b;
    public float Multiply(float a, float b) => a * b;
    public float Divide(float a, float b) => a / b;
    public bool LessThan(float a, float b) => a < b;
    public bool GreaterThan(float a, float b) => a > b;
}

public class DoubleOperations : INumeric<double>
{
    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    public double Divide(double a, double b) => a / b;
    public bool LessThan(double a, double b) => a < b;
    public bool GreaterThan(double a, double b) => a > b;
}

public class Vector2Operations : INumeric<Vector2>
{
    public Vector2 Add(Vector2 a, Vector2 b) => a + b;
    public Vector2 Subtract(Vector2 a, Vector2 b) => a - b;
    public Vector2 Multiply(Vector2 a, Vector2 b) => Vector2.Scale(a, b);
    public Vector2 Divide(Vector2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);
    public bool LessThan(Vector2 a, Vector2 b) => a.magnitude < b.magnitude;
    public bool GreaterThan(Vector2 a, Vector2 b) => a.magnitude > b.magnitude;
}

public class Vector3Operations : INumeric<Vector3>
{
    public Vector3 Add(Vector3 a, Vector3 b) => a + b;
    public Vector3 Subtract(Vector3 a, Vector3 b) => a - b;
    public Vector3 Multiply(Vector3 a, Vector3 b) => Vector3.Scale(a, b);
    public Vector3 Divide(Vector3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public bool LessThan(Vector3 a, Vector3 b) => a.magnitude < b.magnitude;
    public bool GreaterThan(Vector3 a, Vector3 b) => a.magnitude > b.magnitude;
}

public class QuaternionOperations : INumeric<Quaternion>
{
    public Quaternion Add(Quaternion a, Quaternion b) => a * b;
    public Quaternion Subtract(Quaternion a, Quaternion b) => a * Quaternion.Inverse(b);
    public Quaternion Multiply(Quaternion a, Quaternion b) => a * b;
    public Quaternion Divide(Quaternion a, Quaternion b) => a * Quaternion.Inverse(b);
    public bool LessThan(Quaternion a, Quaternion b) => a.eulerAngles.magnitude < b.eulerAngles.magnitude;
    public bool GreaterThan(Quaternion a, Quaternion b) => a.eulerAngles.magnitude > b.eulerAngles.magnitude;
}