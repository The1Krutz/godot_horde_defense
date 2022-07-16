using System;
using Godot;

/// <summary>
/// Interface for anything that should have a Health Node.
/// The Health node will have default implementations, but the parent node can also implement this interface to provide overrides
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

/// <summary>
/// template
/// </summary>
public class Health : Node, IHasHealth {
  // Signals
  [Signal]
  public delegate void HealthChanged(float health);
  [Signal]
  public delegate void HealthDepleted();

  // Exports
  [Export]
  public float MaxHealth { get; set; } = 100.0f;

  // Public Fields
  public float CurrentHealth { get; set; }

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    CurrentHealth = MaxHealth;
    EmitSignal(nameof(HealthChanged), CurrentHealth);
  }

  // Public Functions
  public void TakeDamage(Damage incomingDamage) {
    // TODO: if you're going to do damage resistances by type, this is probably where it goes
    switch (incomingDamage.Type) {
      case DamageType.Healing:
        CurrentHealth = Math.Min(CurrentHealth + incomingDamage.Amount, MaxHealth);
        break;
      default:
        CurrentHealth = Math.Max(CurrentHealth - incomingDamage.Amount, 0);
        break;
    }
    if (CurrentHealth <= 0) {
      // TODO: add some visual for this; right now it just disappears
      EmitSignal(nameof(HealthDepleted));
      GetParent().QueueFree();
    }

    GD.Print("current health: ", CurrentHealth);
    EmitSignal(nameof(HealthChanged), CurrentHealth);
  }

  public void TakeDamageOverTime(DamageOverTime incomingDoT) {
    // TODO: add the incoming dot to a list of dots to process every tick
  }

  // Private Functions
}