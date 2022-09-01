using System;
using Features.Constants;
using Features.GameStates;
using Features.Services.Assets;
using Features.Services.LevelScore;
using Features.Services.Network;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.StaticData.Windows;
using Features.UI.Windows.Base;
using Features.UI.Windows.GameMenu;
using Features.UI.Windows.MainMenu;
using UnityEngine;

namespace Features.Services.UI.Factory.BaseUI
{
  public class UIFactory : IUIFactory
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly ILevelScoreService levelScoreService;
    private readonly INetwork network;

    private Transform uiRoot;

    private Camera mainCamera;

    public event Action<WindowId,BaseWindow> Spawned;
    public bool IsCleanedUp { get; private set; }
    public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assets, IStaticDataService staticData,
      ILevelScoreService levelScoreService, INetwork network)
    {
      this.gameStateMachine = gameStateMachine;
      this.assets = assets;
      this.staticData = staticData;
      this.levelScoreService = levelScoreService;
      this.network = network;
    }
    
    public void Cleanup()
    {
      IsCleanedUp = true;
    }


    public void CreateWindow(WindowId id, IWindowsService windowsService)
    {
      WindowInstantiateData config = LoadWindowInstantiateData(id);
      
      if (uiRoot == null)
        CreateUIRoot();
      
      switch (id)
      {
        case WindowId.MainMenu:
          CreateMainMenu(config, network, gameStateMachine);
          break;
        case WindowId.LevelMenu:
          CreateLevelMenu(config, levelScoreService);
          break;
        default:
          CreateWindow(config, id);
          break;
      }
    }

    private void CreateMainMenu(WindowInstantiateData config, INetwork network, IGameStateMachine gameStateMachine)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIMainMenu)window).Construct(network, gameStateMachine);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreateLevelMenu(WindowInstantiateData config, ILevelScoreService levelScoreService)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIHUD)window).Construct(levelScoreService);
      NotifyAboutCreateWindow(config.ID, window);
    }

    public void CreateUIRoot()
    {
        if (uiRoot != null)
            return;

        UIRoot prefab = assets.Instantiate(GameConstants.UIRootPath).GetComponent<UIRoot>();

        prefab.SetCamera(GetCamera());
        uiRoot = prefab.transform;
    }

    private void CreateWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      NotifyAboutCreateWindow(id, window);
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config)
    {
      BaseWindow window = assets.Instantiate(config.Window, uiRoot);
      window.SetID(config.ID);
      return window;
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config, Transform parent)
    {
      BaseWindow window = assets.Instantiate(config.Window, parent);
      window.SetID(config.ID);
      return window;
    }

    private void NotifyAboutCreateWindow(WindowId id, BaseWindow window) => 
      Spawned?.Invoke(id, window);

    private WindowInstantiateData LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);

    private Camera GetCamera()
    {
      if (mainCamera == null)
        mainCamera = Camera.main;
      return mainCamera;
    }
  }
}