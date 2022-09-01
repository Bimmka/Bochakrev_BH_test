using Features.Player.Scripts.Base;
using Mirror;
using UnityEngine;

namespace Features.Services.EntityFactories
{
  public interface IHeroFactory : IService
  {
    Hero Spawn(int messageModelID, Vector3 startPosition);
    
    GameObject SpawnHandler(SpawnMessage msg);
    void UnspawnHandler(GameObject spawned);
  }
}