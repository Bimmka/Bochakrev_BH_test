using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.LevelScore;
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

    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, ILevelScoreService levelScoreService, IWindowsService windowsService)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.levelScoreService = levelScoreService;
      this.windowsService = windowsService;
    }

    public void Enter()
    {
      levelScoreService.ResetScore();
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad);
    }

    public void Exit()
    {
      
    }

    private void OnLoad()
    {
      CreateHUD();
      levelScoreService.RegisterPlayer("Player");
      gameStateMachine.Enter<GameLoopState>();
    }

    private void CreateHUD() => 
      windowsService.Open(WindowId.LevelMenu);
    
  }
  
  public class MainMenuState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    public MainMenuState(ISceneLoader sceneLoader, IWindowsService windowsService)
    {
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      sceneLoader.Load(GameConstants.MainMenuScene, OnLoaded);
    }

    public void Exit()
    {
      
    }

    private void OnLoaded()
    {
      windowsService.Open(WindowId.MainMenu);  
    }
  }
}