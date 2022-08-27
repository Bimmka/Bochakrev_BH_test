using UnityEngine;

namespace Features.StaticData.InputBindings
{
  [CreateAssetMenu(fileName = "InputBindingsStaticData", menuName = "StaticData/Input/Create Input Data", order = 52)]
  public class InputBindingsStaticData : ScriptableObject
  {
    public string HorizontalMove = "Horizontal";
    public string VerticalMove = "Vertical";
    public string VerticalCameraMove = "Mouse X";
    public string HorizontalCameraMove = "Mouse Y";
    public KeyCode SpecialActionKeyCode = KeyCode.Mouse0;
    public KeyCode CameraRotateButton = KeyCode.Mouse1;
  }
}