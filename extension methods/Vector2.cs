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
  }
}