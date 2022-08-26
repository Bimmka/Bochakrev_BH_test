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

    public HeroDashState(HeroStateMachineObserver hero, HeroMove move, CameraRotator cameraRotator, SimpleAnimator animator, string parameterName) : 
      base(hero, animator, parameterName)
    {
      this.move = move;
      this.cameraRotator = cameraRotator;
    }

    public bool IsCanDash()
    {
      return true;
    }
  }
}