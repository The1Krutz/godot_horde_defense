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

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    Position += Transform.x * speed * delta;
  }

  public void OnBulletEntered(Node target) {
    GD.Print("OnBulletEntered", target);

    QueueFree();

    // if (target.HasNode("Health")) {
    //   GD.Print("dealing damage!", target);

    //   if (target is IHasHealth targetHealth) {
    //     // in case the script has overrides for the default IHasHealth implementations
    //     targetHealth.TakeDamage(WeaponDamage * delta);
    //   } else {
    //     target.GetNode<IHasHealth>("Health").TakeDamage(WeaponDamage * delta);
    //   }
    // }
  }

  // Public Functions

  // Private Functions
}