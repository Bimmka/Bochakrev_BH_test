using System.Collections;
using Features.StaticData.Hero.Damage;
using UnityEngine;

namespace Features.Player.Scripts.Damage
{
  public class HeroDamageHandler : MonoBehaviour
  {
    [SerializeField] private HeroDamageStaticData damageData;
    [SerializeField] private Renderer heroRender;

    private DamageDisplayer displayer;
    public bool IsDamaged { get; private set; }

    private void Awake() => 
      displayer = new DamageDisplayer(heroRender, damageData.DamagedColor, damageData.DefaultColor);

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