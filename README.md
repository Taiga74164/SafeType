# SafeType
A memory protection system for games that prevents common cheating methods like memory scanning and value modification.

## ✨ Features
- **Encryption**: Values are XOR'ed with dynamic keys and bit rotation
- **Tamper Detection**: Runtime integrity verification with checksum validation
- **Zero Friction**: Replacement for standard types with implicit conversions
- **Unity Integration**: Fully serializable in Unity Inspector
- **Optimized**: Minimal overhead with lazy initialization

## 🚀 Usage
### Unity
```csharp
// Declaration
private SafeInt _score = new SafeInt(0);
private SafeFloat _health = new SafeFloat(100f);

// Use it like a normal int/float
_score = 100;
_score++;
_health -= 25.5f;

// Display in UI (implicit conversion)
textComponent.SetText($"Score: {_score}");
healthBar.fillAmount = _health / 100f;

// Serialize in Unity Inspector
[SerializeField] private SafeInt _playerLevel = new SafeInt(1);
[SerializeField] private SafeVector3 _spawnPoint = new SafeVector3(Vector3.zero);
```

<!-- ### Unreal
``` c++
``` -->

## 📦 Supported Types
- `SafeInt`
- `SafeFloat`
- `SafeDouble`
- `SafeVector2`
- `SafeVector3`
- `SafeQuaternion`
- `SafeBool`

## 🛠️ Installation
1. Clone or download the repo
2. Copy scripts to your Unity project
3. Replace critical variables with Safe variant
4. Build and test!

## ⚠️ Important Notes
- **Client side only** - Never trust the client for server-authoritative games
- **Defense in depth** - Use alongside server validation and other anti-cheat measures
- **Performance consideration** - Suitable for critical values, not massive arrays

## 🎯 TODO
- Additional type support (custom structs)
- OnValueChanged events
- C++ / Unreal Engine port
- Network synchronization helpers

## 📹 Demo
![demo](/asset/demo.gif)