using Godot;

interface IWeapon {
  void AimAt(Vector2 aimPoint);
  void StartShooting();
  void StopShooting();
}