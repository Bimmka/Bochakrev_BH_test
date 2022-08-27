using UnityEngine;

namespace Features.Player.Scripts.Damage
{
  public class HeroDamageHandler : MonoBehaviour
  {
    public bool IsCanBeDamageable => true;

    public void Damage()
    {
      Debug.Log($"Damage To Object {transform.name}");
    }
  }
}