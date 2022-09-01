using System;
using Features.Player.Scripts.Base;
using Features.Services.Network;
using Features.Services.UI.Factory;
using Features.StaticData.HeroData.Models;
using Features.StaticData.Windows;
using UnityEngine;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    CustomNetworkManager NetworkManagerPrefab();
    Hero Model(int modelID);
    int ModelID(Guid msgAssetId);
    int RandomModelID();
    Hero[] Models();
  }
}