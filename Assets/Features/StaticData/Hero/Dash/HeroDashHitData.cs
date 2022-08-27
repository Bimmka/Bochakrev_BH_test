using System;
using UnityEngine;

namespace Features.StaticData.Hero.Dash
{
  [Serializable]
  public struct HeroDashHitData
  {
    public LayerMask HitLayer;
    public int MaxHitCount;
  }
}