using Features.StaticData.HeroData.CameraRotate;
using UnityEngine;

namespace Features.Player.Scripts.HeroCamera
{
  public class HeroCameraObserver 
  {
    private readonly Transform heroCamera;
    private readonly HeroCameraStaticData cameraStaticData;
    private readonly Transform cameraTarget;

    private Vector2 rotationAngles;
    private Vector3 currentRotation;
    private Vector3 nextRotation;
    
    private Vector3 smoothVelocity;

    public HeroCameraObserver(Transform heroCamera, HeroCameraStaticData cameraStaticData, Transform cameraTarget)
    {
      this.heroCamera = heroCamera;
      this.cameraStaticData = cameraStaticData;
      this.cameraTarget = cameraTarget;
    }

    public void InitializeCamera()
    {
      SetPositionFromTarget();
      SetStartRotation();
    }

    public void Update(float deltaTime)
    {
      SetPositionFromTarget();
    }

    public void Rotate(Vector2 rotateVector, float deltaTime)
    {
      float horizontalRotation = rotateVector.y * deltaTime * cameraStaticData.HorizontalSensitive;
      AddHorizontalRotation(horizontalRotation);
      UpdateNextRotationHorizontalAngle(rotationAngles.y);
      
      currentRotation = CurrentRotation();

      SetCameraRotation(currentRotation);
      SetPositionFromTarget();
    }

    private void SetStartRotation()
    {
      SetCameraRotation(new Vector3(cameraStaticData.StartAnglesValue.x, cameraStaticData.StartAnglesValue.y, 0));
      currentRotation = heroCamera.localEulerAngles;
      nextRotation = heroCamera.localEulerAngles;
      SaveRotationAngles(cameraStaticData.StartAnglesValue);
    }

    private void SaveRotationAngles(Vector2 rotationAngles) => 
      this.rotationAngles = rotationAngles;

    private void AddHorizontalRotation(float horizontalRotation) => 
      rotationAngles.y += horizontalRotation;

    private void UpdateNextRotationHorizontalAngle(float horizontalRotation) => 
      nextRotation.y = horizontalRotation;

    private Vector3 CurrentRotation() => 
      Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, cameraStaticData.SmoothTime);

    private void SetPositionFromTarget() => 
      heroCamera.position = cameraTarget.position - heroCamera.forward * cameraStaticData.DistanceFromTarget;

    private void SetCameraRotation(Vector3 eulerAngle) => 
      heroCamera.localEulerAngles = eulerAngle;
  }
}