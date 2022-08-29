using Features.StaticData.HeroData.Rotate;
using UnityEngine;

namespace Features.Player.Scripts.Rotate
{
  public class HeroRotate
  {
    private readonly Transform hero;
    private readonly HeroRotateStaticData rotateData;

    public HeroRotate(Transform hero, HeroRotateStaticData rotateData)
    {
      this.hero = hero;
      this.rotateData = rotateData;
    }

    public void WalkRotate(Vector3 to) => 
      Rotate(to, rotateData.WalkLerpRotateValue);

    public void DashRotate(Vector3 to) => 
      Rotate(to, rotateData.DashLerpRotateValue);

    private void Rotate(Vector3 to, float lerp)
    {
      Quaternion Rotation = Quaternion.LookRotation(to);
      hero.rotation = Quaternion.Slerp(hero.rotation, Rotation, lerp);
    }
  }
}