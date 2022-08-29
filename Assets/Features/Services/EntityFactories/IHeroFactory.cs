using Features.Player.Scripts.Base;
using UnityEngine;

namespace Features.Services.EntityFactories
{
  public interface IHeroFactory : IService
  {
    Hero Spawn(string nickName, Transform startPosition);
  }
}