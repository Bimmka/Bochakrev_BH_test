using UnityEngine;

namespace Features.StaticData.HeroData.Rotate
{
  [CreateAssetMenu(fileName = "HeroRotateStaticData", menuName = "StaticData/Hero/Create Hero Rotate Data", order = 52)]
  public class HeroRotateStaticData : ScriptableObject
  {
    [Range(0,1f)]
    public float WalkLerpRotateValue = 0.4f;
    
    [Range(0,1f)]
    public float DashLerpRotateValue = 0.5f;
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
      WalkLerpRotateValue = Mathf.Clamp01(WalkLerpRotateValue);
      DashLerpRotateValue = Mathf.Clamp01(DashLerpRotateValue);
    }
#endif
  }
}