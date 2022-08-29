using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.Player.Scripts.Base;
using Features.Player.Scripts.HeroInput;
using Features.SceneLoading;
using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.InputSystem;
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

    public GameLoadState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, IAssetProvider assetProvider, IInputService inputService, Hero heroPrefab)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.assetProvider = assetProvider;
      this.inputService = inputService;
      this.heroPrefab = heroPrefab;
    }

    public void Enter()
    {
        sceneLoader.Load(GameConstants.GameSceneName, OnLoad);
    }

    public void Exit()
    {
      
    }

    private void OnLoad()
    {
      Hero spawnedHero = assetProvider.Instantiate(heroPrefab, Vector3.zero);
      LevelScoreService levelScoreService = new LevelScoreService();
      spawnedHero.Construct(levelScoreService, inputService, "Player");
      
      gameStateMachine.Enter<GameLoopState>();
    }
  }
}