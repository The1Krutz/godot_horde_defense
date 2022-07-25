using System.Collections.Generic;
using Godot;

/// <summary>
/// template
/// </summary>
public class Player : KinematicBody2D {
  // Signals

  // Exports
  [Export]
  public float MoveSpeed = 200.0f;

  [Export]
  public float ZoomSpeed = 2.0f;

  // Public Fields

  // Backing Fields

  // Private Fields
  private Camera2D camera;
  private float stickAimThreshold = 0.2f;
  private bool useGamepadInput = false;
  private float currentZoom = 1.0f;
  private float minZoom = 0.15f;
  private float maxZoom = 4.0f;

  private List<IWeapon> weapons = new List<IWeapon>();

  // Constructor

  // Lifecycle Hooks
  public override void _Ready() {
    camera = GetNode<Camera2D>("Camera2D");

    foreach (IWeapon temp in GetNode("Weapons").GetChildren()) {
      weapons.Add(temp);
    }

    GD.Print("weapons count: ", weapons.Count);
  }

  public override void _PhysicsProcess(float delta) {
    UpdateMovement(delta);
    UpdateAim();
    UpdatePrimaryFire(delta);
    UpdateSecondaryFire(delta);
  }

  public override void _Process(float delta) {
    // any zoom control except the scroll wheel. That has a separate workaround in _UnhandledInput
    UpdateZoom(Input.GetAxis("zoom_in", "zoom_out"), delta);
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
          UpdateZoom(-1, 0.0333f);
          break;
        case (int)ButtonList.WheelDown:
          UpdateZoom(1, 0.0333f);
          break;
      }
    }
  }

  // Public Functions

  // Private Functions
  private void UpdateMovement(float delta) {
#if GODOT_HTML5
    // need to work around a bug where Input.GetVector doesn't work for html builds
    // @todo - remove this workaround if this bug ever gets fixed: https://github.com/godotengine/godot/issues/58168
    Vector2 movementVector = Vector2.FromAxes("move_left", "move_right", "move_up", "move_down");
#else
    Vector2 movementVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");
#endif
    MoveAndCollide(movementVector * MoveSpeed * delta);
  }

  private void UpdateAim() {
    if (useGamepadInput) {
      // controller aiming
#if GODOT_HTML5
      // need to work around a bug where Input.GetVector doesn't work for html builds
      // @todo - remove this workaround if this bug ever gets fixed: https://github.com/godotengine/godot/issues/58168
      Vector2 controllerAim2 = Vector2.FromAxes("aim_left", "aim_right", "aim_up", "aim_down").Normalized();
#else
      Vector2 controllerAim = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down").Normalized();
#endif
      if (Mathf.Abs(controllerAim.x) > stickAimThreshold || Mathf.Abs(controllerAim.y) > stickAimThreshold) {
        weapons.ForEach((weapon) => weapon.AimAt(controllerAim * 1000));
      }
    } else {
      // mouse aiming
      weapons.ForEach((weapon) => weapon.AimAt(GetLocalMousePosition()));
    }
  }

  /// <summary>
  /// Updates zoom level
  /// </summary>
  /// <param name="scrollScalar">Amount to adjust zoom by. Positive (1) to zoom out, negative (-1) to zoom in</param>
  /// <param name="delta">frame delta</param>
  private void UpdateZoom(float scrollScalar, float delta) {
    if (scrollScalar != 0) {
      currentZoom += scrollScalar * ZoomSpeed * delta;
      currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
      camera.Zoom = new Vector2(currentZoom, currentZoom);
    }
  }

  private void UpdatePrimaryFire(float delta) {
    if (Input.IsActionPressed("fire_primary")) {
      weapons.ForEach((weapon) => weapon.StartShooting());
    } else if (Input.IsActionJustReleased("fire_primary")) {
      weapons.ForEach((weapon) => weapon.StopShooting());
    }
  }

  private void UpdateSecondaryFire(float delta) {
    if (Input.IsActionPressed("fire_secondary")) {
      GD.Print("fire_secondary ", delta);
    }
  }
}