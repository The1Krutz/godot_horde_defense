using ExtensionMethods;
using Godot;

/// <summary>
/// template
/// </summary>
public class Skeleton : KinematicBody2D {
  // Signals

  // Exports
  [Export]
  public float MoveSpeed = 20.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private AnimationPlayer animationPlayer;
  private Vector2 movementVector = new Vector2().Randomize();
  private Timer movementUpdateTimer = new Timer() { WaitTime = 2.0f, OneShot = false, Autostart = true };

  // Constructor
  public Skeleton() {
    movementUpdateTimer.Connect("timeout", this, nameof(RandomizeMovementVector));
    AddChild(movementUpdateTimer);
  }

  // Lifecycle Hooks
  public override void _Ready() {
    animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    animationPlayer.Play("Idle");
  }

  public override void _PhysicsProcess(float delta) {
    UpdateMovement(delta);
  }

  // Public Functions

  // Private Functions
  private void UpdateMovement(float delta) {
    MoveAndCollide(movementVector * MoveSpeed * delta);
  }

  private void RandomizeMovementVector() {
    movementVector = new Vector2().Randomize();
  }
}
