using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Player.Scripts.Move;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroDashState : HeroStateMachineState
  {
    private readonly HeroMove move;
    private readonly CameraRotator cameraRotator;

    public HeroDashState(HeroStateMachineObserver hero, HeroMove move, CameraRotator cameraRotator, SimpleAnimator animator) : base(hero, animator)
    {
      this.move = move;
      this.cameraRotator = cameraRotator;
    }
  }
}