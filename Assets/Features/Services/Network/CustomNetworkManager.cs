using Features.Player.Scripts.Base;
using Features.Services.Assets;
using Features.Services.EntityFactories;
using Features.Services.LevelScore;
using Features.Services.StaticData;
using Mirror;
using UnityEngine;

namespace Features.Services.Network
{
  public class CustomNetworkManager : NetworkManager, INetwork
  {
    [SerializeField] private ScoreNetwork scorePrefab;
    
    private IHeroFactory heroFactory;
    private ILevelScoreService levelScoreService;
    private IStaticDataService staticDataService;
    private IAssetProvider assetProvider;

    public void Construct(IHeroFactory heroFactory, ILevelScoreService levelScoreService, IStaticDataService staticDataService, IAssetProvider assetProvider)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.levelScoreService = levelScoreService;
      this.heroFactory = heroFactory;
      
      Hero[] models = staticDataService.Models();
      for (int i = 0; i < models.Length; i++)
      {
        NetworkClient.RegisterPrefab(models[i].gameObject, heroFactory.SpawnHandler,  heroFactory.UnspawnHandler);
      }
      
      NetworkClient.RegisterPrefab(scorePrefab.gameObject, SpawnScore, UnspawnScore);
    }

    private GameObject SpawnScore(SpawnMessage msg)
    {
      ScoreNetwork score = assetProvider.Instantiate(scorePrefab);
      score.Construct(levelScoreService);
      return score.gameObject;
    }

    private void UnspawnScore(GameObject spawned)
    {
      
    }

    public void CreateHost() => 
      StartHost();

    public void SetLobbyID(string id) => 
      networkAddress = id;

    public void JoinLobby() => 
      StartClient();

    public override void OnStartServer()
    {
      base.OnStartServer();
      
      NetworkServer.RegisterHandler<NetworkHeroModel>(CreateCharacter);

      ScoreNetwork scoreNetwork = assetProvider.Instantiate(scorePrefab);
      scoreNetwork.Construct(levelScoreService);
      NetworkServer.Spawn(scoreNetwork.gameObject);
    }

    public override void ServerChangeScene(string newSceneName)
    {
      base.ServerChangeScene(newSceneName);
     
    }

    public override void OnClientConnect()
    {
      base.OnClientConnect();
      NetworkClient.Send(new NetworkHeroModel(staticDataService.RandomModelID()));
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) { }

    private void CreateCharacter(NetworkConnectionToClient conn, NetworkHeroModel message)
    {
      Hero spawnedHero = heroFactory.Spawn(message.ModelID, RandomPosition().position);
      NetworkServer.AddPlayerForConnection(conn, spawnedHero.gameObject);
     
      levelScoreService.RegisterPlayer(spawnedHero.Nickname);
    }

    private Transform RandomPosition() => 
      startPositions[Random.Range(0, startPositions.Count)];
  }
}
