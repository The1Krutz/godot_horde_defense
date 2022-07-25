using Godot;

namespace ExtensionMethods {
  /// <summary>
  /// template
  /// </summary>
  public static class MyExtensionMethods {
    private static RandomNumberGenerator _random;

    static MyExtensionMethods() {
      _random = new RandomNumberGenerator();
    }

    public static Vector2 Randomize(this Vector2 v, float max = 1.0f, float min = -1.0f) {
      v.x = _random.RandfRange(min, max);
      v.y = _random.RandfRange(min, max);

      return v;
    }

    /// <summary>
    ///   Gets an input vector by specifying four actions for the positive and negative X and Y axes.
    ///   This method is useful when getting vector input, such as from a joystick, directional pad,
    ///   arrows, or WASD. The vector has its length limited to 1 and has a circular deadzone, which
    ///   is useful for using vector input as movement. By default, the deadzone is automatically
    ///   calculated from the average of the action deadzones. However, you can override the
    ///   deadzone to be whatever you want (on the range of 0 to 1).
    ///   (copied from Godot)
    /// </summary>
    /// <param name="negativeX">action label for the negative X-axis</param>
    /// <param name="positiveX">action label for the positive X-axis</param>
    /// <param name="negativeY">action label for the negative Y-axis</param>
    /// <param name="positiveY">action label for the positive Y-axis</param>
    public static Vector2 FromAxes(string negativeX, string positiveX, string negativeY, string positiveY) {
      float lateralMovement = Input.GetActionStrength(positiveX) - Input.GetActionStrength(negativeX);
      float verticalMovement = Input.GetActionStrength(positiveY) - Input.GetActionStrength(negativeY);
      return new Vector2(lateralMovement, verticalMovement).Clamped(1);
    }
  }
}