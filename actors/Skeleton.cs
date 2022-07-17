using Godot;

/// <summary>
/// template
/// </summary>
public class Skeleton : KinematicBody2D {
  // Signals

  // Exports

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

  // Public Functions

  // Private Functions
}