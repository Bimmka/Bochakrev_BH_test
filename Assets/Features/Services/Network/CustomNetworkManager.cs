using Features.Player.Scripts.Base;
using Features.Services.EntityFactories;
using Features.Services.UI.Windows;
using Mirror;
using UnityEngine;

namespace Features.Services.Network
{
  public class CustomNetworkManager : NetworkManager, INetwork
  {
    private IHeroFactory heroFactory;

    public void Construct(IHeroFactory heroFactory)
    {
      this.heroFactory = heroFactory;
    }

    public override void OnStartServer()
    {
      base.OnStartServer();
      
      Hero hero = heroFactory.Spawn($"Player{Random.Range(0, 20)}", RandomPosition());
    }

    public override void OnClientConnect()
    {
      base.OnClientConnect();
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
      base.OnServerAddPlayer(conn);

      Hero hero = heroFactory.Spawn($"Player{Random.Range(0, 20)}", RandomPosition());

      NetworkServer.AddPlayerForConnection(conn, hero.gameObject);
    }

    private Transform RandomPosition() => 
      startPositions[Random.Range(0, startPositions.Count)];
  }
}
