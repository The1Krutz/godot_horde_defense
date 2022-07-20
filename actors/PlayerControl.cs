using System;
using Godot;

/// <summary>
/// template
/// </summary>
public class PlayerControl : Node2D {
  // Signals

  // Exports
  [Export]
  public float MoveSpeed = 200.0f;

  [Export]
  public float ZoomSpeed = 2.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private KinematicBody2D parent;
  private Camera2D camera;
  private float stickAimThreshold = 0.2f;
  private bool useGamepadInput = false;
  private float currentZoom = 1.0f;
  private float minZoom = 0.15f;
  private float maxZoom = 4.0f;

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    parent = GetParent<KinematicBody2D>();
    camera = GetNode<Camera2D>("Camera2D");
  }

  public override void _PhysicsProcess(float delta) {
    // movement
    Vector2 movementVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");

    parent.MoveAndCollide(movementVector * MoveSpeed * delta);

    if (useGamepadInput) {
      // controller aiming
      Vector2 controllerAim = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down");

      if (Mathf.Abs(controllerAim.x) > stickAimThreshold || Mathf.Abs(controllerAim.y) > stickAimThreshold) {
        parent.LookAt(parent.GlobalPosition + controllerAim);
      }
    } else {
      // mouse aiming
      parent.LookAt(parent.GetGlobalMousePosition());
    }

    if (Input.IsActionPressed("fire_primary")) {
      GD.Print("fire_primary pressed");
    }
    if (Input.IsActionPressed("fire_secondary")) {
      GD.Print("fire_secondary pressed");
    }
  }

  public override void _Process(float delta) {
    float scrollScalar = Input.GetAxis("zoom_in", "zoom_out");

    if (scrollScalar != 0) {
      // currentZoom += scrollScalar * ZoomSpeed * delta;
      // currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
      // camera.Zoom = new Vector2(currentZoom, currentZoom);

      UpdateZoom(scrollScalar * delta);
    }

    GD.Print(scrollScalar, camera.Zoom);
  }

  public override void _UnhandledInput(InputEvent @event) {
    if (@event is InputEventJoypadMotion || @event is InputEventJoypadButton) {
      useGamepadInput = true;
    } else if (@event is InputEventMouseMotion || @event is InputEventMouseButton) {
      useGamepadInput = false;
    }

    // mouse scroll wheel input workaround
    // or I guess technically any mouse button events, but I'd rather use Input.etc for most stuff
    if (@event is InputEventMouseButton emb && emb.IsPressed()) {
      switch (emb.ButtonIndex) {
        case (int)ButtonList.WheelUp:
          UpdateZoom(-0.0333f); // -1 for the GetAxis, divided by 30 for part of the delta
          GD.Print("scrolling up");
          break;
        case (int)ButtonList.WheelDown:
          UpdateZoom(0.0333f); // 1 for the GetAxis, divided by 30 for part of the delta
          GD.Print("scrolling down");
          break;
      }
    }
  }

  // Public Functions

  // Private Functions
  private void UpdateZoom(float scrollScalar) {
    currentZoom += scrollScalar * ZoomSpeed;
    currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    camera.Zoom = new Vector2(currentZoom, currentZoom);
  }
}