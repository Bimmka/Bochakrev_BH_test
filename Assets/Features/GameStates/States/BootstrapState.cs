using Features.GameStates.States.Interfaces;
using Features.Services;
using Features.Services.Assets;
using Features.Services.InputSystem;
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
    }

    private void RegisterStateMachine() => 
      services.RegisterSingle(gameStateMachine);

    private void RegisterInputService(InputBindingsStaticData bindingsData) => 
      services.RegisterSingle(new InputService(bindingsData));

    private void RegisterAssetsService() => 
      services.RegisterSingle(new AssetProvider());
  }
}