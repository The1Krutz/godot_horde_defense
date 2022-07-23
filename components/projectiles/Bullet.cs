using Godot;

/// <summary>
/// template
/// </summary>
public class Bullet : Area2D {
  // Signals

  // Exports
  [Export]
  public float speed = 100.0f;

  // Public Fields
  public Damage WeaponDamage; // damage per bullet

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    Position += Transform.x * speed * delta;
  }

  public void OnBulletEntered(Node target) {
    if (target.HasNode("Health")) {
      if (target is IHasHealth targetHealth) {
        // in case the script has overrides for the default IHasHealth implementations
        targetHealth.TakeDamage(WeaponDamage);
      } else {
        target.GetNode<IHasHealth>("Health").TakeDamage(WeaponDamage);
      }
    }

    QueueFree();
  }

  // Public Functions

  // Private Functions
}