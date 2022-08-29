using Features.Player.Scripts.Base;
using Features.Player.Scripts.Markers;
using Features.Services.Assets;
using Features.Services.InputSystem;
using Features.Services.LevelScore;
using Features.Services.StaticData;
using Features.StaticData.HeroData.Models;
using UnityEngine;

namespace Features.Services.EntityFactories
{
  public class HeroFactory : IHeroFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly ILevelScoreService levelScoreService;
    private readonly IInputService inputService;

    public HeroFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, ILevelScoreService levelScoreService, IInputService inputService)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.levelScoreService = levelScoreService;
      this.inputService = inputService;
    }

    public Hero Spawn(string nickName, Transform startPosition)
    {
      Hero spawnedHero = assetProvider.Instantiate(staticDataService.HeroPrefab(), startPosition);

      HeroModelSpawnMarker modelSpawnMarker = spawnedHero.GetComponentInChildren<HeroModelSpawnMarker>();
      HeroModel spawnedModel = SpawnModel(modelSpawnMarker.transform);
      
      spawnedHero.Construct(levelScoreService, inputService, nickName,spawnedModel);
      return spawnedHero;
    }

    private HeroModel SpawnModel(Transform transform)
    {
      HeroModel model = RandomModel();
      HeroModel spawnedModel = assetProvider.Instantiate(model, transform);
      return spawnedModel;
    }

    private HeroModel RandomModel() => 
      staticDataService.RandomModel();

  }
}