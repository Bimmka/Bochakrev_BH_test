namespace Features.StateMachine
{
  public class BaseStateMachine
  {
    private BaseStateMachineState currentState;

    public BaseStateMachineState State => currentState;
        
    private void ExitState()
    {
      currentState.Exit();
    }

    public void SetState(BaseStateMachineState state)
    {
      currentState = state;
      currentState.Enter();
    }

    public void ChangeState(BaseStateMachineState newState)
    {
      ExitState();
      SetState(newState);
    }
  }
}