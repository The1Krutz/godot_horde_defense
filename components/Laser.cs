using Godot;

/// <summary>
/// template
/// </summary>
public class Laser : Node2D, IWeapon {
  // Signals

  // Exports
  [Export]
  private float damagePerSecond = 100.0f;

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
        GD.Print("dealing damage!", target);

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
    ToggleBeam(true);
  }

  public void StopShooting() {
    ToggleBeam(false);
  }

  // Private Functions
  private void ToggleBeam(bool value) {
    aimCast.Enabled = value;
    // TODO: toggle something to show the beam
  }
}