using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.Base;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroIdleState : HeroStateMachineState
  {
    private readonly CameraRotator cameraRotator;

    public HeroIdleState(HeroStateMachineObserver hero, CameraRotator cameraRotator, SimpleAnimator simpleAnimator) : base(hero, simpleAnimator)
    {
      this.cameraRotator = cameraRotator;
    }
  }
}