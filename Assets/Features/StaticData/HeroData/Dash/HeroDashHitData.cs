using System;
using UnityEngine;

namespace Features.StaticData.HeroData.Dash
{
  [Serializable]
  public struct HeroDashHitData
  {
    public LayerMask HitLayer;
    public int MaxHitCount;
  }
}