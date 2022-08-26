using Features.Animatons;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Move;
using Features.Services.InputSystem;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStateMachineObserver : MonoBehaviour
  {
    [SerializeField] private SimpleAnimator animator;
    
    private HeroStateMachine stateMachine;
    private HeroStatesContainer statesContainer;
    
    public void Construct(HeroStatesContainer container)
    {
      stateMachine = new HeroStateMachine();
      statesContainer = container;
    }

    public void Subscribe() => 
      animator.Triggered += OnAnimationTriggered;

    public void Cleanup() => 
      animator.Triggered -= OnAnimationTriggered;

    public void CreateStates() => 
      statesContainer.CreateStates();

    public void SetDefaultState() => 
      stateMachine.SetState(statesContainer.GetState<HeroIdleState>());

    public void UpdateState(IInputCommand[] commands, int commandsCount, float deltaTime) => 
      stateMachine.UpdateState(commands, commandsCount, deltaTime);

    public void ChangeState<TState>() where TState : HeroStateMachineState => 
      stateMachine.ChangeState(statesContainer.GetState<TState>());

    private void OnAnimationTriggered() => 
      stateMachine.State.TriggerAnimation();
  }
}