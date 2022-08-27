using Features.GameStates.States.Interfaces;
using Features.Services;

namespace Features.GameStates
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>;
    TState GetState<TState>() where TState : class, IExitableState;
  }
}