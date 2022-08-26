using System;
using UnityEngine;

namespace Features.Extensions
{
  public static class VectorsExtension
  {
    public static bool IsEqualMoveDirection(this Vector3 vector, Vector2 comparedVector) => 
      Mathf.Approximately(vector.x, comparedVector.x) && Mathf.Approximately(vector.z, comparedVector.y);
  }
}