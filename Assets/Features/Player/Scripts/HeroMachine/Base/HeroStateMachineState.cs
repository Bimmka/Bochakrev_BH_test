using Features.Animatons;
using Features.Services.InputSystem;
using Features.StateMachine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStateMachineState : BaseStateMachineState
  {
    protected readonly HeroStateMachineObserver hero;
    protected readonly SimpleAnimator animator;
    
    public HeroStateMachineState(HeroStateMachineObserver hero, SimpleAnimator animator)
    {
      this.hero = hero;
      this.animator = animator;
    }
    
    public virtual void Update(IInputCommand[] commands, int commandsCount, float deltaTime) {}

    protected void ChangeState<TState>() where TState : HeroStateMachineState => 
      hero.ChangeState<TState>();
      
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
          ApplyCameraRotateCommand((InputCommandAxis) command, deltaTime);
          break;
      }
    }

    protected virtual void ApplyMoveCommand(InputCommandVector command, float deltaTime) { }

    protected virtual void ApplySpecialCommand(InputCommandBool command, float deltaTime) { }

    protected virtual void ApplyCameraRotateCommand(InputCommandAxis command, float deltaTime) { }

    protected void SetBool(int hashName, bool isEnable) => 
      animator.SetBool(hashName, isEnable);
  }
}