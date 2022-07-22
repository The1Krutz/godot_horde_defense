
/// <summary>
/// Interface for anything that should have a Health Node.
/// The Health node will have default implementations, but the parent node can also implement this interface to provide overrides.
/// </summary>
public interface IHasHealth {
  float MaxHealth { get; set; }
  float CurrentHealth { get; set; }
  void TakeDamage(Damage incoming);
  void TakeDamageOverTime(DamageOverTime incoming);
}

public enum DamageType {
  Healing,
  Normal,
  ArmorPiercing,
  Fire,
}

public struct Damage {
  public float Amount;
  public DamageType Type;

  public Damage(float amount, DamageType type = DamageType.Normal) {
    Amount = amount;
    Type = type;
  }

  public static Damage operator *(Damage dmg, float delta) {
    dmg.Amount *= delta;
    return dmg;
  }
}

public struct DamageOverTime {
  public float Amount;
  public DamageType Type;
  public float Duration; // total duration of the effect

  public DamageOverTime(float amount, float duration, DamageType type = DamageType.Normal) {
    Amount = amount;
    Duration = duration;
    Type = type;
  }
}
