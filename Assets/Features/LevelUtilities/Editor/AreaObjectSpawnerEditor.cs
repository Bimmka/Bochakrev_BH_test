using System.Collections.Generic;
using Features.LevelUtilities.Scripts;
using UnityEditor;
using UnityEngine;

namespace Features.LevelUtilities.Editor
{
  [CustomEditor(typeof(AreaObjectSpawner))]
  public class AreaObjectSpawnerEditor : UnityEditor.Editor
  {
    private const int SpawnTryCount = 100;
    
    private Transform spawnPosition;
    private Transform spawnParent;
    private GameObject spawnObject;
    private Vector2 spawnArea;
    private Vector3 minObjectRotation;
    private Vector3 maxObjectRotation;
    private Vector3 objectSize;
    private float minDistance;
    private int count;
    private bool isNeedDeleteChild;

    public override void OnInspectorGUI()
    {
      spawnPosition = (Transform) serializedObject.FindProperty("startPosition").objectReferenceValue;
      spawnParent = (Transform) serializedObject.FindProperty("spawnParent").objectReferenceValue;
      spawnObject = (GameObject) serializedObject.FindProperty("spawnObject").objectReferenceValue;
      spawnArea = serializedObject.FindProperty("spawnArea").vector2Value;
      minObjectRotation = serializedObject.FindProperty("minObjectRotation").vector3Value;
      maxObjectRotation = serializedObject.FindProperty("maxObjectRotation").vector3Value;
      objectSize = serializedObject.FindProperty("objectSize").vector3Value;
      minDistance = serializedObject.FindProperty("minDistance").floatValue;
      count = serializedObject.FindProperty("count").intValue;
      isNeedDeleteChild = serializedObject.FindProperty("isDeleteAllChildBeforeSpawn").boolValue;
      
      base.OnInspectorGUI();
      
      if (GUILayout.Button("Spawn"))
      {
        Spawn();
      }
    }

    private void Spawn()
   {
      if (spawnParent.childCount > 0 && isNeedDeleteChild)
         DeleteChildren();

      Undo.IncrementCurrentGroup();
      int currentTryCount = 0;
      List<GameObject> spawnedObjects = new List<GameObject>(count);
      for (int i = 0; i < count; i++)
      {
         currentTryCount = 0;
         while (currentTryCount < SpawnTryCount)
         {
            Vector3 randomPosition = RandomPosition();
            if (IsValidPosition(spawnedObjects, randomPosition))
            {
               spawnedObjects.Add(SpawnObject(randomPosition));
               break;
            }

            currentTryCount++;
         }
      }
   }

   private Vector3 RandomPosition()
   {
      float randomXPosition = Random.Range(spawnPosition.position.x - spawnArea.x/2 * objectSize.x, spawnPosition.position.x + spawnArea.x/2 * objectSize.x);
      float randomZPosition = Random.Range(spawnPosition.position.z - spawnArea.y/2 * objectSize.z, spawnPosition.position.z + spawnArea.y/2 * objectSize.z);
      return new Vector3(randomXPosition, spawnPosition.position.y, randomZPosition);
   }

   private bool IsValidPosition(List<GameObject> spawnedObjects, Vector3 randomPosition)
   {
      float calculatedDistance;
      for (int i = 0; i < spawnedObjects.Count; i++)
      {
         calculatedDistance = Vector3.Distance(spawnedObjects[i].transform.position, randomPosition);
         if (calculatedDistance < minDistance)
            return false;
      }

      return true;
   }

   private GameObject SpawnObject(Vector3 randomPosition)
   {
      GameObject spawnedObject = PrefabUtility.InstantiatePrefab(spawnObject) as GameObject;
      spawnedObject.transform.position = randomPosition;
      spawnedObject.transform.rotation = Quaternion.Euler(RandomEulerAngle());
      spawnedObject.transform.SetParent(spawnParent);
      Undo.RegisterCreatedObjectUndo(spawnedObject, $"Create Object {spawnedObject.name+randomPosition.magnitude}");
      return spawnedObject;
   }
   
   private Vector3 RandomEulerAngle()
   {
      float xEuler = Random.Range(minObjectRotation.x, maxObjectRotation.x);
      float yEuler = Random.Range(minObjectRotation.y, maxObjectRotation.y);
      float zEuler = Random.Range(minObjectRotation.z, maxObjectRotation.z);
      return new Vector3(xEuler, yEuler, zEuler);
   }

   private void DeleteChildren()
   {
      Transform[] children = spawnParent.GetComponentsInChildren<Transform>(true);
      for (int i = 0; i < children.Length; i++)
      {
         if (children[i] != null && spawnParent != children[i].transform)
            DestroyImmediate(children[i].gameObject);
      }
   }
  }
}