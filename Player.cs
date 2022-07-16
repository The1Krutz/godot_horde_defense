using Godot;

/// <summary>
/// template
/// </summary>
public class Player : KinematicBody2D {
  // Signals

  // Exports
  [Export]
  public float MoveSpeed = 500.0f;

  // Public Fields

  // Backing Fields

  // Private Fields

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    // movement
    float horizontalMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
    float verticalMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

    Vector2 movementVector = new Vector2(horizontalMovement, verticalMovement).Clamped(1.0f);
    // using Clamped here instead of Normalized becuase we want to be able to move slowly by nudging the stick

    MoveAndCollide(movementVector * MoveSpeed * delta);

    // mouse aiming
    LookAt(GetGlobalMousePosition());

    // controller aiming
    // @todo - it works okay, but it feels stick in the cardinal directions. Not really sure why, but it doesn't feel good to use
    // float horizontalAimAdjust = Input.GetActionStrength("aim_right") - Input.GetActionStrength("aim_left");
    // float verticalAimAdjust = Input.GetActionStrength("aim_down") - Input.GetActionStrength("aim_up");
    // Vector2 controllerAim = new Vector2(horizontalAimAdjust, verticalAimAdjust).Normalized();
    // if (controllerAim.Length() > 0) {
    //   LookAt(GlobalPosition + controllerAim);
    // }

    // @todo - do the shoot code here
  }

  // Public Functions

  // Private Functions

}