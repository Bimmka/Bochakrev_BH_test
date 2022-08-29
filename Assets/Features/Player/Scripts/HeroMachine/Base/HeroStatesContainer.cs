using System;
using System.Collections.Generic;
using Features.Animatons;
using Features.Player.Scripts.HeroCamera;
using Features.Player.Scripts.HeroMachine.States;
using Features.Player.Scripts.Move;
using Features.Services.LevelScore;
using Features.StateMachine;
using Features.StaticData.HeroData.Dash;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.Base
{
  public class HeroStatesContainer
  {
    private readonly HeroStateMachineObserver hero;
    private readonly HeroMove move;
    private readonly HeroCameraObserver heroCamera;
    private readonly SimpleAnimator animator;
    private readonly HeroDashStaticData dashStaticData;
    private readonly CharacterController characterController;
    private readonly ILevelScoreService levelScoreService;
    private readonly string heroName;
    
    private readonly Dictionary<Type, BaseStateMachineState> states;
    public HeroStatesContainer(HeroStateMachineObserver hero, HeroMove move, HeroCameraObserver heroCamera,
      SimpleAnimator animator, HeroDashStaticData dashStaticData, CharacterController characterController, 
      ILevelScoreService levelScoreService, string heroName)
    {
      this.hero = hero;
      this.move = move;
      this.heroCamera = heroCamera;
      this.animator = animator;
      this.dashStaticData = dashStaticData;
      this.characterController = characterController;
      this.levelScoreService = levelScoreService;
      this.heroName = heroName;
      states = new Dictionary<Type, BaseStateMachineState>(5);
    }

    public void CreateStates()
    {
      CreateIdleState();
      CreateMoveState();
      CreateDashState();
    }

    public TState GetState<TState>() where TState : BaseStateMachineState
    {
      if (states.ContainsKey(typeof(TState)))
        return (TState)states[typeof(TState)];

      throw new ArgumentNullException();
    }

    private void CreateIdleState()
    {
      HeroIdleState state = new HeroIdleState(hero, heroCamera, animator, "IsIdle");
      SaveState(state);
    }

    private void CreateMoveState()
    {
      HeroMoveState state = new HeroMoveState(hero,move, heroCamera, animator, "IsMove");
      SaveState(state);
    }

    private void CreateDashState()
    {
      HeroDashState state = new HeroDashState(hero,move, heroCamera, animator, "IsDash", dashStaticData, 
        characterController.height, characterController.radius, levelScoreService, heroName);
      SaveState(state);
    }

    private void SaveState(BaseStateMachineState state) => 
      states.Add(state.GetType(), state);
  }
}