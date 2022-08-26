using Features.StaticData.Hero.CameraRotate;
using UnityEngine;

namespace Features.Player.Scripts.HeroCamera
{
  public class CameraRotator
  {
    private readonly Transform camera;
    private readonly HeroCameraRotateStaticData cameraRotateData;

    public CameraRotator(Transform camera, HeroCameraRotateStaticData cameraRotateData)
    {
      this.camera = camera;
      this.cameraRotateData = cameraRotateData;
    }    
  }
}