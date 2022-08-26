using UnityEngine;

namespace Features.StaticData.Hero.CameraRotate
{
  [CreateAssetMenu(fileName = "HeroCameraRotateStaticData", menuName = "StaticData/Hero/Create Camera Rotate Data", order = 52)]
  public class HeroCameraRotateStaticData : ScriptableObject
  {
    public float HorizontalSensitive = 100f;
    public float VerticalSensitive = 100f;
  }
}