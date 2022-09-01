using System;
using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Player.Scripts.Base;
using Features.Services.Network;
using Features.Services.UI.Factory;
using Features.StaticData.HeroData.Models;
using Features.StaticData.Windows;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public Hero Model(int modelID) => 
      modelsData.Models[modelID];

    public int ModelID(Guid msgAssetId)
    {
      for (int i = 0; i < modelsData.Models.Length; i++)
      {
        if (modelsData.Models[i].GetComponent<NetworkIdentity>().assetId == msgAssetId)
          return i;
      }

      return -1;
    }

    public Hero[] Models() => 
      modelsData.Models;

    public int RandomModelID() => 
      Random.Range(0, modelsData.Models.Length);
  }
}