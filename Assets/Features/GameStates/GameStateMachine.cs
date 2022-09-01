﻿using System;
using System.Collections.Generic;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services;
using Features.Services.LevelScore;
using Features.Services.Network;
using Features.Services.UI.Windows;
using Features.StaticData.InputBindings;

namespace Features.GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    private readonly AllServices services;

    public GameStateMachine(ISceneLoader sceneLoader, ref AllServices services, InputBindingsStaticData bindingsData)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(
          this, 
          ref services,
          bindingsData
          ),
        
        [typeof(MainMenuState)] = new MainMenuState(
            sceneLoader,
            services.Single<IWindowsService>()
            ),
        
        [typeof(GameLoadState)] = new GameLoadState(
          this,
          sceneLoader,
          services.Single<ILevelScoreService>(),
          services.Single<IWindowsService>(),
          services.Single<INetwork>()
          ),
        
        [typeof(GameLoopState)] = new GameLoopState()
      };

      this.services = services;
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    public TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;


    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    public void Cleanup()
    {
      services.Cleanup();
    }
  }
}