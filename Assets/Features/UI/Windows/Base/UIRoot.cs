using UnityEngine;

namespace Features.UI.Windows.Base
{
  public class UIRoot : MonoBehaviour
  {
    [SerializeField] private Canvas canvas;
  
    public void SetCamera(Camera camera) => 
      canvas.worldCamera = camera;
  }
}
