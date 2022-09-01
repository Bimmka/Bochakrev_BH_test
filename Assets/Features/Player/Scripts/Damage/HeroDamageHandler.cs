using System.Collections;
using Features.StaticData.HeroData.Damage;
using UnityEngine;

namespace Features.Player.Scripts.Damage
{
  public class HeroDamageHandler : MonoBehaviour
  {
    [SerializeField] private HeroDamageStaticData damageData;
    [SerializeField] private Renderer modelRenderer;
    
    private DamageDisplayer displayer;
    public bool IsDamaged { get; private set; }

    public void Initialize()
    {
      displayer = new DamageDisplayer(modelRenderer, damageData.DamagedColor, damageData.DefaultColor);
    }

    public void Damage()
    {
      IsDamaged = true;
      displayer.Show();
      StartCoroutine(WaitInvincibleTime());
    }

    private IEnumerator WaitInvincibleTime()
    {
      yield return new WaitForSeconds(damageData.InvincibleDuration);
      ResetHandler();
    }

    private void ResetHandler()
    {
      displayer.Hide();
      IsDamaged = false;
    }
  }
}