using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.LevelScore;
using Features.Services.Network;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly ILevelScoreService levelScoreService;
    private readonly IWindowsService windowsService;
    private readonly INetwork network;

    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, ILevelScoreService levelScoreService, IWindowsService windowsService, INetwork network)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.levelScoreService = levelScoreService;
      this.windowsService = windowsService;
      this.network = network;
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }

    public void LoadAsHost() => 
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad, StartHost);

    public void LoadAsClient(string lobbyId)
    {
      network.SetLobbyID(lobbyId);
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad, JoinLobby);
    }

    private void OnLoad()
    {
      CreateHUD();
      gameStateMachine.Enter<GameLoopState>();
    }

    private void StartHost()
    {
      network.CreateHost();
    }

    private void JoinLobby()
    {
      network.JoinLobby();
    }

    private void CreateHUD() => 
      windowsService.Open(WindowId.LevelMenu);
    
  }
}