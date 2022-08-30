using Features.SceneLoading.Scripts;
using Features.Services;
using Features.Services.CoroutineRunner;
using Features.StaticData.InputBindings;

namespace Features.GameStates
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, ref AllServices services,
      InputBindingsStaticData bindingsData)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner,curtain), ref services, bindingsData);
    }

    public void Cleanup()
    {
      StateMachine.Cleanup();
    }
  }
}