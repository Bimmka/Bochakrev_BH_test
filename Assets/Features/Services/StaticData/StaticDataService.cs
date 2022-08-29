using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Player.Scripts.Base;
using Features.Services.Network;
using Features.Services.UI.Factory;
using Features.StaticData.HeroData.Models;
using Features.StaticData.Windows;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;

    private CustomNetworkManager networkManager;

    private HeroModelsStaticData modelsData;
    
    public void Load()
    {
      windows = Resources
        .Load<WindowsStaticData>(GameConstants.WindowsDataPath)
        .InstantiateData
        .ToDictionary(x => x.ID, x => x);

      networkManager = Resources.Load<CustomNetworkManager>(GameConstants.NetworkManagerPath);
      modelsData = Resources.Load<HeroModelsStaticData>(GameConstants.HeroModelsPath);
      Resources.UnloadUnusedAssets();
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();

    public CustomNetworkManager NetworkManagerPrefab() => 
      networkManager;

    public HeroModel RandomModel() => 
      modelsData.Models[Random.Range(0, modelsData.Models.Length)];

    public Hero HeroPrefab() => 
      modelsData.HeroPrefab;
  }
}