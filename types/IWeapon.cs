using Godot;

/// <summary>
/// Interface for anything that should be useable as a weapon.
/// Should be useable by player and AI controlled weapons.
/// </summary>
interface IWeapon {
  void AimAt(Vector2 aimPoint);
  void StartShooting();
  void StopShooting();
}
