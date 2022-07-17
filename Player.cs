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
  private float stickAimThreshold = 0.2f;
  private bool useGamepadInput = false;

  // Constructor

  // Lifecycle Hooks
  public override void _PhysicsProcess(float delta) {
    // movement
    Vector2 movementVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");

    MoveAndCollide(movementVector * MoveSpeed * delta);

    if (useGamepadInput) {
      // controller aiming
      Vector2 controllerAim = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down");

      if (Mathf.Abs(controllerAim.x) > stickAimThreshold || Mathf.Abs(controllerAim.y) > stickAimThreshold) {
        LookAt(GlobalPosition + controllerAim);
      }
    } else {
      // mouse aiming
      LookAt(GetGlobalMousePosition());
    }
    // @todo - do the shoot code here
  }

  public override void _UnhandledInput(InputEvent @event) {
    if (@event is InputEventJoypadMotion || @event is InputEventJoypadButton) {
      useGamepadInput = true;
    } else if (@event is InputEventMouseMotion || @event is InputEventMouseButton) {
      useGamepadInput = false;
    }
  }

  // Public Functions

  // Private Functions

}