using Features.Player.Scripts.Base;
using Features.Services.EntityFactories;
using Features.Services.LevelScore;
using Features.Services.StaticData;
using Mirror;
using UnityEngine;

namespace Features.Services.Network
{
  public class CustomNetworkManager : NetworkManager, INetwork
  {
    private IHeroFactory heroFactory;
    private ILevelScoreService levelScoreService;
    private IStaticDataService staticDataService;

    public void Construct(IHeroFactory heroFactory, ILevelScoreService levelScoreService, IStaticDataService staticDataService)
    {
      this.staticDataService = staticDataService;
      this.levelScoreService = levelScoreService;
      this.heroFactory = heroFactory;
      
      Hero[] models = staticDataService.Models();
      for (int i = 0; i < models.Length; i++)
      {
        NetworkClient.RegisterPrefab(models[i].gameObject, heroFactory.SpawnHandler,  heroFactory.UnspawnHandler);
      }
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
