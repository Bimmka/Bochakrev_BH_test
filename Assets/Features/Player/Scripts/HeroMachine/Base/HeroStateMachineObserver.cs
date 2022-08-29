using Features.Animatons;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Move;
using Features.Services.InputSystem;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStateMachineObserver : MonoBehaviour
  {
    private SimpleAnimator animator;
    
    private HeroStateMachine stateMachine;
    private HeroStatesContainer statesContainer;
    
    public void Construct(HeroStatesContainer container, SimpleAnimator animator)
    {
      stateMachine = new HeroStateMachine();
      statesContainer = container;

      this.animator = animator;
    }

    public void Subscribe() => 
      animator.Triggered += OnAnimationTriggered;

    public void Cleanup() => 
      animator.Triggered -= OnAnimationTriggered;

    public void CreateStates() => 
      statesContainer.CreateStates();

    public void SetDefaultState() => 
      stateMachine.SetState(GetState<HeroIdleState>());

    public void UpdateState(IInputCommand[] commands, int commandsCount, float deltaTime) => 
      stateMachine.UpdateState(commands, commandsCount, deltaTime);

    public void ChangeState<TState>() where TState : HeroStateMachineState => 
      stateMachine.ChangeState(GetState<TState>());
    
    public void ChangeState<TState>(TState state) where TState : HeroStateMachineState => 
      stateMachine.ChangeState(state);

    public TState GetState<TState>() where TState : HeroStateMachineState => 
      statesContainer.GetState<TState>();

    private void OnAnimationTriggered() => 
      stateMachine.State.TriggerAnimation();
  }
}