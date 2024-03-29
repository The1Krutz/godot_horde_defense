using Godot;

/// <summary>
/// template
/// </summary>
public class Laser : Node2D, IWeapon {
  // Signals

  // Exports
  [Export]
  private float damagePerSecond = 300.0f;

  [Export]
  private DamageType damageType = DamageType.Normal;

  // Public Fields

  // Backing Fields

  // Private Fields
  private RayCast2D aimCast;
  private Damage WeaponDamage { get; } // damage per second of continuous contact with the beam

  // Constructor
  public Laser() {
    WeaponDamage = new Damage(damagePerSecond, damageType);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    aimCast = GetNode<RayCast2D>("AimCast");
  }

  public override void _PhysicsProcess(float delta) {
    if (aimCast.IsColliding()) {
      CollisionObject2D target = (CollisionObject2D)aimCast.GetCollider();

      if (target.HasNode("Health")) {
        if (target is IHasHealth targetHealth) {
          // in case the script has overrides for the default IHasHealth implementations
          targetHealth.TakeDamage(WeaponDamage * delta);
        } else {
          target.GetNode<IHasHealth>("Health").TakeDamage(WeaponDamage * delta);
        }
      }
    } else {
      // TODO: should we do something if they miss?
    }
  }

  // Public Functions
  public void AimAt(Vector2 aimPoint) {
    aimCast.CastTo = aimPoint;
  }

  public void StartShooting() {
    aimCast.Enabled = true;
    // TODO - turn on some effect to show the laser beam
  }

  public void StopShooting() {
    aimCast.Enabled = false;
    // TODO - turn off the effect to show the laser beam
  }

  // Private Functions
}