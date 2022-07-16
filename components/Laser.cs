using Godot;

/// <summary>
/// template
/// </summary>
public class Laser : Node2D {
  // Signals

  // Exports
  [Export]
  private float damagePerSecond = 20.0f;

  [Export]
  private DamageType damageType = DamageType.Normal;

  // Public Fields

  // Backing Fields

  // Private Fields
  private RayCast aimCast;
  private Damage WeaponDamage { get; } // damage per second of continuous contact with the beam

  // Constructor
  public Laser() {
    WeaponDamage = new Damage(damagePerSecond, damageType);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    aimCast = GetNode<RayCast>("RayCast");
  }

  public override void _PhysicsProcess(float delta) {
    if (Input.IsActionPressed("fire_primary")) {
      if (aimCast.IsColliding()) {
        CollisionObject target = (CollisionObject)aimCast.GetCollider();

        if (target.HasNode("Health")) {
          IHasHealth targetHealth = target.GetNode<IHasHealth>("Health");

          targetHealth.TakeDamage(WeaponDamage * delta);
        }
      } else {
        // TODO: should we do something if they miss?
      }
    }

    if (Input.IsActionJustPressed("fire_primary")) {
      ToggleBeam(true);
    }
    if (Input.IsActionJustReleased("fire_primary")) {
      ToggleBeam(false);
    }
  }

  // Public Functions

  // Private Functions
  private void ToggleBeam(bool value) {
    aimCast.Enabled = value;
    // TODO: toggle something to show the beam
  }
}