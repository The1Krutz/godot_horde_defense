using System;
using Godot;

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
