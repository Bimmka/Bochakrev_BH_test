using Features.GameStates;
using Features.GameStates.States;
using Features.SceneLoading.Scripts;
using Features.Services;
using Features.Services.CoroutineRunner;
using Features.StaticData.InputBindings;
using UnityEngine;

namespace Features.Bootstrapp
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain curtain;
    [SerializeField] private InputBindingsStaticData bindingsData;

    private Game game;

    private AllServices allServices;

    private void Awake()
    {
      allServices = new AllServices();
      game = new Game(this, Instantiate(curtain), ref allServices, bindingsData);
      
      DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
      game.StateMachine.Enter<BootstrapState>();
      game.StateMachine.Enter<MainMenuState>();
    }
  }
}