using UnityEngine;

namespace Features.Player.Scripts.Damage
{
  public class DamageDisplayer
  {
    private readonly Renderer renderer;
    private readonly Color showColor;
    private readonly Color hideColor;
    private readonly MaterialPropertyBlock propertyBlock;
    
    private static readonly int Color = Shader.PropertyToID("_AlbedoTint");

    public DamageDisplayer(Renderer renderer, Color showColor, Color hideColor)
    {
      this.renderer = renderer;
      this.showColor = showColor;
      this.hideColor = hideColor;
      propertyBlock = new MaterialPropertyBlock();
      renderer.GetPropertyBlock(propertyBlock);
    }
    
    public void Show()
    {
      propertyBlock.SetColor(Color, showColor);
      renderer.SetPropertyBlock(propertyBlock);
    }

    public void Hide()
    {
      propertyBlock.SetColor(Color, hideColor);
      renderer.SetPropertyBlock(propertyBlock);
    }
  }
}