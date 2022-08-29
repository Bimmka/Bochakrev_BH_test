using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.Player.Scripts.Base;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.InputSystem;
using Features.Services.LevelScore;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using UnityEngine;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IAssetProvider assetProvider;
    private readonly IInputService inputService;
    private readonly Hero heroPrefab;
    private readonly ILevelScoreService levelScoreService;
    private readonly IWindowsService windowsService;

    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IAssetProvider assetProvider, IInputService inputService, Hero heroPrefab,
      ILevelScoreService levelScoreService, IWindowsService windowsService)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.assetProvider = assetProvider;
      this.inputService = inputService;
      this.heroPrefab = heroPrefab;
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
      CreatePlayer();
      levelScoreService.RegisterPlayer("Player");
      gameStateMachine.Enter<GameLoopState>();
    }

    private void CreateHUD() => 
      windowsService.Open(WindowId.LevelMenu);

    private void CreatePlayer()
    {
      Hero spawnedHero = assetProvider.Instantiate(heroPrefab, Vector3.zero);
      spawnedHero.Construct(levelScoreService, inputService, "Player");
    }
  }
}