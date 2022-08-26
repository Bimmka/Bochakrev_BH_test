using UnityEngine;

namespace Features.LevelUtilities.Scripts
{
  public class DirectionObjectSpawner : MonoBehaviour
  {
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Vector3 spawnDirection;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private int count;
    [SerializeField] private float offset;
  }
}
