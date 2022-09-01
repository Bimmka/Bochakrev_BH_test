using Features.Player.Scripts.Base;
using UnityEngine;

namespace Features.StaticData.HeroData.Models
{
  [CreateAssetMenu(fileName = "HeroModelsStaticData", menuName = "StaticData/Hero/Create Models Data", order = 52)]
  public class HeroModelsStaticData : ScriptableObject
  {
    public Hero[] Models;
  }
}