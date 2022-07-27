using Godot;

/// <summary>
/// template
/// </summary>
public class Hitbox : Area2D, IHasHealth {
  // Signals

  // Exports

  // Public Fields
  public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
  public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

  // Backing Fields

  // Private Fields
  IHasHealth parentHealth;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    parentHealth = GetParent<Node2D>().GetNode<IHasHealth>("Health");
  }

  // Public Functions
  public void TakeDamage(Damage incoming) {
    parentHealth.TakeDamage(incoming);
  }

  public void TakeDamageOverTime(DamageOverTime incoming) {
    parentHealth.TakeDamageOverTime(incoming);
  }

  // Private Functions
}