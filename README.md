# SafeType

## Features
- Uses XOR and Rol64
- Runtime value verification
- Serializable in Unity Inspector

## Usage (Unity)

```csharp
// Declaration
private SafeInt _score = new SafeInt(0);

// Use it like a normal int
_score = 100;
_score++;

// Display in UI
textComponent.SetText($"{_score}");

// Serialize in Unity Inspector
[SerializeField] private SafeInt _health = new SafeInt(100);
```

## Supported Types
- `SafeInt`
- `SafeFloat`
- `SafeDouble`
- `SafeVector2`
- `SafeVector3`
- `SafeQuaternion`
- `SafeBool`

## Notes
- Values are encrypted in memory
- Client side only

## TODO
- Add more supported types (optional)
- OnValueChanged
- C++ / Unreal engine port
- Change encryption (optional)