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
    private readonly CharacterController characterController;
    private readonly Action actionOnHit;

    private readonly Collider[] hits;

    private readonly float colliderRadius;
    private readonly float colliderHeight;

    public int LastHitCount { get; private set; }

    public HeroDashHitter(HeroDashHitData hitData, Transform hero, CharacterController characterController,
      Action actionOnHit)
    {
      this.hitData = hitData;
      this.hero = hero;
      this.characterController = characterController;
      this.actionOnHit = actionOnHit;
      hits = new Collider[hitData.MaxHitCount];

      colliderRadius = HeroColliderRadius(characterController);
      colliderHeight = HeroColliderHeight(characterController);
    }

    public bool IsHit()
    {
      LastHitCount = Physics.OverlapCapsuleNonAlloc(
        HeroColliderDown(), 
        HeroColliderUp(), 
        colliderRadius, 
        hits, 
        hitData.HitLayer);

      return LastHitCount > 0;
    }

    public void Attack()
    {
      HeroDamageHandler damageHandler;
      for (int i = 0; i < LastHitCount; i++)
      {
        if (hits[i] != characterController && hits[i].TryGetComponent(out damageHandler) && damageHandler.IsDamaged == false)
        {
          damageHandler.Damage();
          actionOnHit?.Invoke();
        }
      }
    }

    private Vector3 HeroColliderDown() => 
      hero.position - Vector3.up * colliderHeight / 2;

    private Vector3 HeroColliderUp() => 
      hero.position + Vector3.up * colliderHeight / 2;

    private float HeroColliderRadius(CharacterController characterController) => 
      characterController.radius + characterController.radius * hitData.RadiusOffsetCoefficientFromCollider;
    
    private float HeroColliderHeight(CharacterController characterController) => 
      characterController.height + characterController.height * hitData.RadiusOffsetCoefficientFromCollider;
  }
}