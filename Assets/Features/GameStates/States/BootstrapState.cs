using Features.GameStates.States.Interfaces;
using Features.Services;
using Features.Services.Assets;
using Features.Services.EntityFactories;
using Features.Services.InputSystem;
using Features.Services.LevelScore;
using Features.Services.Network;
using Features.Services.StaticData;
using Features.Services.UI.Factory.BaseUI;
using Features.Services.UI.Windows;
using Features.StaticData.InputBindings;

namespace Features.GameStates.States
{
  public class BootstrapState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly AllServices services;


    public BootstrapState(IGameStateMachine gameStateMachine, ref AllServices services, InputBindingsStaticData bindingsData)
    {
      this.gameStateMachine = gameStateMachine;
      this.services = services;
      RegisterServices(bindingsData);
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }

    private void RegisterServices(InputBindingsStaticData bindingsData)
    {
      RegisterStateMachine();
      RegisterInputService(bindingsData);
      RegisterAssetsService();
      RegisterStaticDataService();
      RegisterLevelScoreService();
      RegisterHeroFactory();
      RegisterNetworkManagerService();
      RegisterUIFactory();
      RegisterWindowsService();
    }

    private void RegisterStateMachine() => 
      services.RegisterSingle(gameStateMachine);

    private void RegisterInputService(InputBindingsStaticData bindingsData) => 
      services.RegisterSingle(new InputService(bindingsData));

    private void RegisterAssetsService() => 
      services.RegisterSingle(new AssetProvider());

    private void RegisterStaticDataService()
    {
      IStaticDataService dataService = new StaticDataService();
      dataService.Load();
      services.RegisterSingle(dataService);
    }

    private void RegisterLevelScoreService() => 
      services.RegisterSingle(new LevelScoreService());

    private void RegisterUIFactory()
    {
      services.RegisterSingle(new UIFactory(
        services.Single<IGameStateMachine>(),
        services.Single<IAssetProvider>(),
        services.Single<IStaticDataService>(),
        services.Single<ILevelScoreService>(),
        services.Single<INetwork>()));
    }

    private void RegisterWindowsService() => 
      services.RegisterSingle(new WindowsService(services.Single<IUIFactory>()));

    private void RegisterHeroFactory()
    {
      services.RegisterSingle(new HeroFactory(
        services.Single<IAssetProvider>(), 
        services.Single<IStaticDataService>(), 
        services.Single<ILevelScoreService>(),
        services.Single<IInputService>())
      );
    }

    private void RegisterNetworkManagerService()
    {
      INetwork network = services.Single<IAssetProvider>().Instantiate(services.Single<IStaticDataService>().NetworkManagerPrefab());
      
      network.Construct(
        services.Single<IHeroFactory>(), 
        services.Single<ILevelScoreService>(),
        services.Single<IStaticDataService>()
        );
      
      services.RegisterSingle(network);
    }
  }
}