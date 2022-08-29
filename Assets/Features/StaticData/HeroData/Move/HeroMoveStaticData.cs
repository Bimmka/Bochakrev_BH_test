using UnityEngine;

namespace Features.StaticData.HeroData.Move
{
  [CreateAssetMenu(fileName = "HeroMoveStaticData", menuName = "StaticData/Hero/Create Hero Move Data", order = 52)]
  public class HeroMoveStaticData : ScriptableObject
  {
    public float WalkSpeed = 2f;
  }
}