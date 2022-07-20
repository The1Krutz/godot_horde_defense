using Godot;
using ExtensionMethods;

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

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    animationPlayer.Play("Idle");
  }

  public override void _PhysicsProcess(float delta) {
    // movement
    Vector2 movementVector = new Vector2().Randomize();

    MoveAndCollide(movementVector * MoveSpeed * delta);
  }

  // Public Functions

  // Private Functions
}
