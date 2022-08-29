using UnityEngine;

namespace Features.StaticData.HeroData.CameraRotate
{
  [CreateAssetMenu(fileName = "HeroCameraStaticData", menuName = "StaticData/Hero/Create Camera Data", order = 52)]
  public class HeroCameraStaticData : ScriptableObject
  {
    public float HorizontalSensitive = 100f;
    public float VerticalSensitive = 100f;
    public Vector2 StartAnglesValue = Vector2.zero;
    public float DistanceFromTarget = 10f;
    public float SmoothTime = 0.5f;
  }
}