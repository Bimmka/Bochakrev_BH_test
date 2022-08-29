using Features.Player.Scripts.Base;
using Features.Services.Network;
using Features.Services.UI.Factory;
using Features.StaticData.HeroData.Models;
using Features.StaticData.Windows;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    CustomNetworkManager NetworkManagerPrefab();
    HeroModel RandomModel();
    Hero HeroPrefab();
  }
}