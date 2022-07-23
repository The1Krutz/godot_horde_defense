using Godot;

/// <summary>
/// template
/// </summary>
public class LMG : Node2D, IWeapon {
  // Signals

  // Exports
  [Export]
  private float damagePerShot = 100.0f;

  [Export]
  private DamageType damageType = DamageType.Normal;

  [Export(hintString: "Aim spread in degrees")]
  private float aimSpread {
    get { return _aimSpread; }
    set {
      _aimSpread = Mathf.Deg2Rad(value);
    }
  }

  // Public Fields

  // Backing Fields
  private float _aimSpread = 0.261799f; // default to 15 degrees

  // Private Fields
  private static RandomNumberGenerator _random;

  private Damage WeaponDamage { get; } // damage per bullet
  private Vector2 AimDirection;
  private Timer ShotTimer;
  private PackedScene Bullet;

  // Constructor
  public LMG() {
    _random = new RandomNumberGenerator();

    WeaponDamage = new Damage(damagePerShot, damageType);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    ShotTimer = GetNode<Timer>("ShotTimer");
    Bullet = ResourceLoader.Load<PackedScene>("res://components/projectiles/Bullet.tscn");
  }

  // Public Functions
  public void AimAt(Vector2 aimPoint) {
    AimDirection = aimPoint;
  }

  public void StartShooting() {
    GD.Print("todo - start shooting");
    SpawnBullet();
  }

  public void StopShooting() {
    GD.Print("todo - stop shooting");
  }

  // Private Functions
  private void SpawnBullet() {
    Area2D bullet = Bullet.Instance<Area2D>();
    Owner.AddChild(bullet);
    float spreadValue = _random.RandfRange(-aimSpread, aimSpread);
    bullet.Transform = new Transform2D(AimDirection.Angle() + spreadValue, Position);
  }
}