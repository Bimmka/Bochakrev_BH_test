using UnityEngine;

namespace Features.StaticData.HeroData.Dash
{
  [CreateAssetMenu(fileName = "HeroDashStaticData", menuName = "StaticData/Hero/Create Hero Dash Data", order = 52)]
  public class HeroDashStaticData : ScriptableObject
  {
    public float MaxDuration = 3f;
    public float DashStepValue = 0.5f;

    public HeroDashHitData HitData;
  }
}