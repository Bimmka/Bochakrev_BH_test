using Features.Services.InputSystem;
using Features.StateMachine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStateMachine : BaseStateMachine
  {
    public void UpdateState(IInputCommand[] commands, int commandsCount, float deltaTime)
    {
      ((HeroStateMachineState) State).Update(commands, commandsCount, deltaTime);
    }
  }
}