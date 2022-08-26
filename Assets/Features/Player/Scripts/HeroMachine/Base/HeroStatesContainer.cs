using System;
using System.Collections.Generic;
using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Move;
using Features.StateMachine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStatesContainer
  {
    private readonly HeroStateMachineObserver hero;
    private readonly HeroMove move;
    private readonly CameraRotator cameraRotator;
    private readonly SimpleAnimator animator;
    private readonly Dictionary<Type, BaseStateMachineState> states;
    public HeroStatesContainer(HeroStateMachineObserver hero, HeroMove move, CameraRotator cameraRotator, SimpleAnimator animator)
    {
      this.hero = hero;
      this.move = move;
      this.cameraRotator = cameraRotator;
      this.animator = animator;
      states = new Dictionary<Type, BaseStateMachineState>(5);
    }

    public void CreateStates()
    {
      CreateIdleState();
      CreateRunState();
      CreateWalkState();
    }

    public TState GetState<TState>() where TState : BaseStateMachineState
    {
      if (states.ContainsKey(typeof(TState)))
        return (TState)states[typeof(TState)];

      throw new ArgumentNullException();
    }

    private void CreateIdleState()
    {
      HeroIdleState state = new HeroIdleState(hero, cameraRotator, animator);
      SaveState(state);
    }

    private void CreateRunState()
    {
      HeroMoveState state = new HeroMoveState(hero,move, cameraRotator, animator);
      SaveState(state);
    }

    private void CreateWalkState()
    {
      HeroDashState state = new HeroDashState(hero,move, cameraRotator, animator);
      SaveState(state);
    }

    private void SaveState(BaseStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}