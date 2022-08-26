namespace Features.StateMachine
{
  public abstract class BaseStateMachineState
  {
    public virtual void Enter() => 
      Check();

    public virtual void Check() {}

    public virtual void PhysicsUpdate() => 
      Check();

    public virtual void Exit() { }

    public virtual void TriggerAnimation() { }
  }
}