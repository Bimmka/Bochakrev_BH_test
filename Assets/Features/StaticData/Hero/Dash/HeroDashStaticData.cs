using UnityEngine;

namespace Features.StaticData.Hero.Dash
{
  [CreateAssetMenu(fileName = "HeroDashStaticData", menuName = "StaticData/Hero/Create Hero Dash Data", order = 52)]
  public class HeroDashStaticData : ScriptableObject
  {
    public float MaxDuration = 3f;
    public float DashStepValue = 0.5f;
  }
}