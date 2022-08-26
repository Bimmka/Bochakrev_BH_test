using System;
using UnityEngine;

namespace Features.Animatons
{
  public class SimpleAnimator : MonoBehaviour
  {
    [SerializeField] protected Animator animator;

    public event Action Triggered;

    public virtual void Initialize() { }

    public void SetBool(int animationHash, bool isSet) => 
      animator.SetBool(animationHash, isSet);

    public void SetFloat(int animationHash, float value) => 
      animator.SetFloat(animationHash, value);

    public void AnimationTriggered() => 
      Triggered?.Invoke();
  }
}