using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Services.InputSystem;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroIdleState : HeroStateMachineState
  {
    private readonly HeroCameraObserver cameraRotator;

    public HeroIdleState(HeroStateMachineObserver hero, HeroCameraObserver cameraRotator, SimpleAnimator animator, string parameterName) : 
      base(hero, animator, parameterName)
    {
      this.cameraRotator = cameraRotator;
    }

    protected override void ApplyMoveCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyMoveCommand(command, deltaTime);
      
      if (command.Vector == Vector2.zero) 
        return;
      
      ChangeState<HeroMoveState>();
    }

    protected override void ApplyCameraRotateCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyCameraRotateCommand(command, deltaTime);
      
      cameraRotator.Rotate(command.Vector, deltaTime);
    }

    protected override void ApplySpecialCommand(InputCommandBool command, float deltaTime)
    {
      base.ApplySpecialCommand(command, deltaTime);

      HeroDashState state = GetState<HeroDashState>();
      
      if (state.IsCanDash())
        ChangeState<HeroDashState>();
    }
  }
}