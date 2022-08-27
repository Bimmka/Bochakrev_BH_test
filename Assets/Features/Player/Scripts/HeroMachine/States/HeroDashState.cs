using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.Base;
using Features.Player.Scripts.Move;
using Features.Services.InputSystem;
using Features.StaticData.Hero.Dash;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroDashState : HeroStateMachineState
  {
    private static readonly Vector2 dashDirection = Vector2.up;
    
    private readonly HeroMove move;
    private readonly HeroCameraObserver cameraRotator;
    private readonly HeroDashStaticData dashData;

    private float dashDuration;
    private bool isDashing;

    public HeroDashState(HeroStateMachineObserver hero, HeroMove move, HeroCameraObserver cameraRotator, SimpleAnimator animator, string parameterName, HeroDashStaticData dashData) : 
      base(hero, animator, parameterName)
    {
      this.move = move;
      this.cameraRotator = cameraRotator;
      this.dashData = dashData;
    }

    public override void Enter()
    {
      base.Enter();
      ChangeDashingState(true);
      ResetDashDuration();
    }

    public override void Update(IInputCommand[] commands, int commandsCount, float deltaTime)
    {
      Move(deltaTime);
      UpdateDashDuration(deltaTime);
      base.Update(commands, commandsCount, deltaTime);

      if (IsDashEnd())
      {
        if (IsHeroMove(commands, commandsCount))
          ChangeState<HeroMoveState>();
        else
          ChangeState<HeroIdleState>();
      }
    }

    private bool IsHeroMove(IInputCommand[] commands, int commandsCount)
    {
      if (commandsCount == 0)
        return false;

      for (int i = 0; i < commandsCount; i++)
      {
        if (commands[i].Type == InputCommandType.Move)
          return true;
      }

      return false;
    }

    public override void Exit()
    {
      base.Exit();
      ChangeDashingState(false);
    }

    public bool IsCanDash() => 
      isDashing == false;

    protected override void ApplyCameraRotateCommand(InputCommandVector command, float deltaTime)
    {
      base.ApplyCameraRotateCommand(command, deltaTime);
      
      cameraRotator.Rotate(command.Vector, deltaTime);
    }

    private void UpdateDashDuration(float deltaTime) => 
      dashDuration += deltaTime;

    private void ResetDashDuration() => 
      dashDuration = 0;

    private void Move(float deltaTime) => 
      move.Dash(dashDirection, deltaTime, dashData.DashStepValue);

    private void ChangeDashingState(bool isEnable) => 
      isDashing = isEnable;

    private bool IsDashEnd() => 
      dashDuration >= dashData.MaxDuration;
  }
}