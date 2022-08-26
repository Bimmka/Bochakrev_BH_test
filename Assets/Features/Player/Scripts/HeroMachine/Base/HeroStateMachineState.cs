using Features.Animatons;
using Features.Services.InputSystem;
using Features.StateMachine;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStateMachineState : BaseStateMachineState
  {
    private readonly HeroStateMachineObserver hero;
    private readonly SimpleAnimator animator;

    private readonly int parameterName;
    
    public HeroStateMachineState(HeroStateMachineObserver hero, SimpleAnimator animator, string animationParameterName)
    {
      this.hero = hero;
      this.animator = animator;

      parameterName = Animator.StringToHash(animationParameterName);
    }

    public override void Enter()
    {
      base.Enter();
      animator.SetBool(parameterName, true);
    }

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(parameterName, false);
    }

    public virtual void Update(IInputCommand[] commands, int commandsCount, float deltaTime) {}

    protected void ChangeState<TState>() where TState : HeroStateMachineState => 
      hero.ChangeState<TState>();
    
    protected void ChangeState<TState>(TState state) where TState : HeroStateMachineState => 
      hero.ChangeState<TState>(state);

    protected TState GetState<TState>() where TState : HeroStateMachineState => 
      hero.GetState<TState>();

    protected void ApplyCommand(IInputCommand command, float deltaTime)
    {
      
      switch (command.Type)
      {
        case InputCommandType.Move:
          ApplyMoveCommand((InputCommandVector) command, deltaTime);
          break;
        case InputCommandType.SpecialAction:
          ApplySpecialCommand((InputCommandBool) command, deltaTime);
          break;
        case InputCommandType.CameraRotate:
          ApplyCameraRotateCommand((InputCommandVector) command, deltaTime);
          break;
      }
    }

    protected virtual void ApplyMoveCommand(InputCommandVector command, float deltaTime) { }

    protected virtual void ApplySpecialCommand(InputCommandBool command, float deltaTime) { }

    protected virtual void ApplyCameraRotateCommand(InputCommandVector command, float deltaTime) { }

    protected void SetBool(int hashName, bool isEnable) => 
      animator.SetBool(hashName, isEnable);
  }
}