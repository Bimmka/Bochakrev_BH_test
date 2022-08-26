using System;
using UnityEngine;

namespace Features.LevelUtilities.Scripts
{
  public class AreaObjectSpawner : MonoBehaviour
  {
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private Vector3 minObjectRotation;
    [SerializeField] private Vector3 maxObjectRotation;
    [SerializeField] private int count;
    [SerializeField] private float minDistance;
    [SerializeField] private Vector3 objectSize;
    [SerializeField] private bool isDeleteAllChildBeforeSpawn;

    private void OnDrawGizmos()
    {
      if (startPosition != null)
        Gizmos.DrawWireCube(startPosition.position, new Vector3(spawnArea.x,0.1f, spawnArea.y));
    }
  }
}