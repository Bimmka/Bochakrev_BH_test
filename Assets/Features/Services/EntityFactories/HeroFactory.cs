using Features.Player.Scripts.Base;
using Features.Player.Scripts.Markers;
using Features.Services.Assets;
using Features.Services.InputSystem;
using Features.Services.LevelScore;
using Features.Services.StaticData;
using Features.StaticData.HeroData.Models;
using Mirror;
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

    public Hero Spawn(int messageModelID, Vector3 startPosition)
    {
      Hero spawnedHero = assetProvider.Instantiate(staticDataService.Model(messageModelID), startPosition);

      spawnedHero.Construct(levelScoreService, inputService);
      return spawnedHero;
    }

    public GameObject SpawnHandler(SpawnMessage msg) => 
      Spawn(staticDataService.ModelID(msg.assetId), msg.position).gameObject;

    public void UnspawnHandler(GameObject spawned) => 
      Object.Destroy(spawned);
  }
}