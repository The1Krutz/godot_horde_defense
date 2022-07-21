
namespace Helpers {
  public class EasyTimer {
    public delegate void RollOverCallback();

    public float CurrentValue { get; private set; }
    public float RollOverPeriod { get; }
    public RollOverCallback OnRollover;

    public EasyTimer(float rollOverPeriod, RollOverCallback onRollover) {
      RollOverPeriod = rollOverPeriod;
      CurrentValue = 0.0f;
      OnRollover = onRollover;
    }

    public void Update(float delta) {
      CurrentValue += delta;
      if (CurrentValue >= RollOverPeriod) {
        CurrentValue -= RollOverPeriod;
        OnRollover();
      }
    }
  }
}