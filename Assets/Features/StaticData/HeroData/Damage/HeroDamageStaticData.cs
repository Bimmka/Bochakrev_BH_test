using UnityEngine;

namespace Features.StaticData.HeroData.Damage
{
  [CreateAssetMenu(fileName = "HeroDamageStaticData", menuName = "StaticData/Hero/Create Damage Data", order = 52)]
  public class HeroDamageStaticData : ScriptableObject
  {
    public float InvincibleDuration = 3f;
    public Color DamagedColor;
    public Color DefaultColor = Color.white;
  }
}