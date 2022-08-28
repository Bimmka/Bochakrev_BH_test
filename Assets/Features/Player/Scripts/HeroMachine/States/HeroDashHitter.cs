using Features.Player.Scripts.Damage;
using Features.StaticData.Hero.Dash;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroDashHitter
  {
    private readonly HeroDashHitData hitData;
    private readonly Transform hero;
    private readonly float heroColliderHeight;
    private readonly float heroColliderRadius;
    
    private readonly Collider[] hits;

    private int lastHitCount;

    public HeroDashHitter(HeroDashHitData hitData, Transform hero, float heroColliderHeight, float heroColliderRadius)
    {
      this.hitData = hitData;
      this.hero = hero;
      this.heroColliderHeight = heroColliderHeight;
      this.heroColliderRadius = heroColliderRadius;
      hits = new Collider[hitData.MaxHitCount];
    }

    public bool IsHit()
    {
      lastHitCount = Physics.OverlapCapsuleNonAlloc(
        HeroColliderDown(), 
        HeroColliderUp(), 
        heroColliderRadius, 
        hits, 
        hitData.HitLayer);

      return lastHitCount > 0;
    }

    public void Attack()
    {
      HeroDamageHandler damageHandler;
      for (int i = 0; i < lastHitCount; i++)
      {
        if (hits[i].TryGetComponent(out damageHandler) && damageHandler.IsDamaged == false)
          damageHandler.Damage();
      }
    }

    private Vector3 HeroColliderDown() => 
      hero.position - Vector3.up * heroColliderHeight / 2;

    private Vector3 HeroColliderUp() => 
      hero.position + Vector3.up * heroColliderHeight / 2;
  }
}