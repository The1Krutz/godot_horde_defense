using Godot;
using Helpers;

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
  private float shotCooldown = 0.1f;

  [Export]
  private float AimSpread {
    get { return _aimSpread; }
    set {
      _aimSpread = Mathf.Deg2Rad(value);
    }
  }

  // Public Fields

  // Backing Fields
  private float _aimSpread = 0.261799f / 2; // default to 15 degrees

  // Private Fields
  private static RandomNumberGenerator _random;

  private Damage WeaponDamage { get; } // damage per bullet
  private Vector2 AimDirection;
  private PackedScene BulletScene;
  private bool isAllowedToShoot = true;

  private FastTimer ShotTimer;
  private int queuedShots = 1;

  // Constructor
  public LMG() {
    _random = new RandomNumberGenerator();

    WeaponDamage = new Damage(damagePerShot, damageType);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    BulletScene = ResourceLoader.Load<PackedScene>("res://components/projectiles/Bullet.tscn");

    ShotTimer = new FastTimer(shotCooldown, ReadyToShoot);
  }

  public override void _Process(float delta) {
    queuedShots += ShotTimer.Update(delta);
  }

  // Public Functions
  public void AimAt(Vector2 aimPoint) {
    AimDirection = aimPoint;
  }

  public void StartShooting() {
    if (ShotTimer.IsStopped) {
      ShotTimer.OneShot = false;
      ShotTimer.Start();
    }

    if (isAllowedToShoot) {
      for (; queuedShots > 0; queuedShots--) {
        SpawnBullet();
      }
      isAllowedToShoot = false;
    }
  }

  public void StopShooting() {
    ShotTimer.OneShot = true;
  }

  public void DestroyIfBullet(Node body) {
    if (body is Bullet bullet) {
      bullet.QueueFree();
    }
  }

  // Private Functions
  private void SpawnBullet() {
    Bullet bullet = BulletScene.Instance<Bullet>();
    Owner.AddChild(bullet);
    float spreadValue = _random.RandfRange(-AimSpread, AimSpread);
    bullet.Transform = new Transform2D(AimDirection.Angle() + spreadValue, Position);
    bullet.WeaponDamage = WeaponDamage;
  }

  private void ReadyToShoot() {
    isAllowedToShoot = true;
  }
}