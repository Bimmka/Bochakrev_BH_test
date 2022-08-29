using System;
using System.Collections.Generic;
using Features.Player.Scripts.Damage;
using Features.StaticData.HeroData.Dash;
using UnityEngine;

namespace Features.Player.Scripts.HeroMachine.States
{
  public class HeroDashHitter
  {
    private readonly HeroDashHitData hitData;
    private readonly Transform hero;
    private readonly float heroColliderHeight;
    private readonly float heroColliderRadius;
    private readonly Action actionOnHit;

    private readonly Collider[] hits;

    public int LastHitCount { get; private set; }
    public IReadOnlyCollection<Collider> Hits => hits;

    public HeroDashHitter(HeroDashHitData hitData, Transform hero, float heroColliderHeight, float heroColliderRadius,
      Action actionOnHit)
    {
      this.hitData = hitData;
      this.hero = hero;
      this.heroColliderHeight = heroColliderHeight;
      this.heroColliderRadius = heroColliderRadius;
      this.actionOnHit = actionOnHit;
      hits = new Collider[hitData.MaxHitCount];
    }

    public bool IsHit()
    {
      LastHitCount = Physics.OverlapCapsuleNonAlloc(
        HeroColliderDown(), 
        HeroColliderUp(), 
        heroColliderRadius, 
        hits, 
        hitData.HitLayer);

      return LastHitCount > 0;
    }

    public void Attack()
    {
      HeroDamageHandler damageHandler;
      for (int i = 0; i < LastHitCount; i++)
      {
        if (hits[i].TryGetComponent(out damageHandler) && damageHandler.IsDamaged == false)
        {
          damageHandler.Damage();
          actionOnHit?.Invoke();
        }
      }
    }

    private Vector3 HeroColliderDown() => 
      hero.position - Vector3.up * heroColliderHeight / 2;

    private Vector3 HeroColliderUp() => 
      hero.position + Vector3.up * heroColliderHeight / 2;
  }
}