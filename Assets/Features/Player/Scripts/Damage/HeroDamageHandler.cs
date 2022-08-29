using System.Collections;
using Features.StaticData.HeroData.Damage;
using UnityEngine;

namespace Features.Player.Scripts.Damage
{
  public class HeroDamageHandler : MonoBehaviour
  {
    [SerializeField] private HeroDamageStaticData damageData;

    private Renderer heroRender;
    private DamageDisplayer displayer;
    public bool IsDamaged { get; private set; }

    public void Construct(Renderer modelRenderer)
    {
      heroRender = modelRenderer;
      displayer = new DamageDisplayer(heroRender, damageData.DamagedColor, damageData.DefaultColor);
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