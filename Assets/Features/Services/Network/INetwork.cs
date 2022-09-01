using Features.Services.Assets;
using Features.Services.EntityFactories;
using Features.Services.LevelScore;
using Features.Services.StaticData;

namespace Features.Services.Network
{
  public interface INetwork : IService
  {
    void Construct(IHeroFactory heroFactory, ILevelScoreService levelScoreService, IStaticDataService staticDataService, IAssetProvider assetProvider);
    void CreateHost();
    void SetLobbyID(string id);
    void JoinLobby();
  }
}