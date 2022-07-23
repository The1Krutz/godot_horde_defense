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

  [Export]
  private float AimSpread {
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
  private bool isAllowedToShoot = true;

  // Constructor
  public LMG() {
    _random = new RandomNumberGenerator();

    WeaponDamage = new Damage(damagePerShot, damageType);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    ShotTimer = GetNode<Timer>("ShotTimer");
    ShotTimer.Connect("timeout", this, nameof(ReadyToShoot));
    Bullet = ResourceLoader.Load<PackedScene>("res://components/projectiles/Bullet.tscn");
  }

  // Public Functions
  public void AimAt(Vector2 aimPoint) {
    AimDirection = aimPoint;
  }

  public void StartShooting() {
    if (ShotTimer.IsStopped()) {
      ShotTimer.OneShot = false;
      ShotTimer.Start();
    }

    if (isAllowedToShoot) {
      SpawnBullet();
      isAllowedToShoot = false;
    }
  }

  public void StopShooting() {
    ShotTimer.OneShot = true;
  }

  // Private Functions
  private void SpawnBullet() {
    Area2D bullet = Bullet.Instance<Area2D>();
    Owner.AddChild(bullet);
    float spreadValue = _random.RandfRange(-AimSpread, AimSpread);
    bullet.Transform = new Transform2D(AimDirection.Angle() + spreadValue, Position);
  }

  private void ReadyToShoot() {
    isAllowedToShoot = true;
  }
}