using System.Collections;
using UnityEngine;

namespace Features.Services.CoroutineRunner
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopAllCoroutines();
    void StopCoroutine(Coroutine coroutineRunner);
  }
}