
using Godot;

namespace Helpers {
  public class FastTimer {
    public delegate void RollOverCallback();

    public float CurrentValue { get; private set; }
    public float RollOverPeriod { get; } = 1.0f;
    private RollOverCallback OnRollover;
    public bool IsStopped { get; private set; }
    public bool OneShot { get; set; }

    public FastTimer(float rollOverPeriod, RollOverCallback onRollover, bool oneShot = false, bool activateImmediately = false) {
      RollOverPeriod = rollOverPeriod;
      CurrentValue = 0.0f;
      OnRollover = onRollover;
      OneShot = oneShot;
      IsStopped = !activateImmediately;
    }

    public int Update(float delta) {
      if (IsStopped) {
        return 0;
      }

      int shotCount = 0;

      CurrentValue += delta;
      while (CurrentValue >= RollOverPeriod) {
        CurrentValue -= RollOverPeriod;
        OnRollover();

        if (OneShot) {
          Stop();
          return 1;
        } else {
          shotCount++;
        }
      }

      if (shotCount > 1) {
        // GD.Print("multiple shot frame! ", shotCount);
      }
      return shotCount;
    }

    public void Start() {
      CurrentValue = 0.0f;
      IsStopped = false;
    }

    public void Stop() {
      IsStopped = true;
    }
  }
}