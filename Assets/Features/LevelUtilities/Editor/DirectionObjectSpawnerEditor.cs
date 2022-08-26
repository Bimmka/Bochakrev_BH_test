using Features.LevelUtilities.Scripts;
using UnityEditor;
using UnityEngine;

namespace Features.LevelUtilities.Editor
{
  [CustomEditor(typeof(DirectionObjectSpawner))]
  public class DirectionObjectSpawnerEditor : UnityEditor.Editor
  {
    private DirectionObjectSpawner myTarget;

    private Transform spawnPosition;
    private Transform spawnParent;
    private GameObject spawnObject;
    private Vector3 spawnDirection;
    private Vector3 rotation;
    private float offset;
    private int count;
    
    public override void OnInspectorGUI()
    {
      myTarget = (DirectionObjectSpawner) target;
      
      spawnPosition = (Transform) serializedObject.FindProperty("startPosition").objectReferenceValue;
      spawnParent = (Transform) serializedObject.FindProperty("spawnParent").objectReferenceValue;
      spawnObject = (GameObject) serializedObject.FindProperty("spawnObject").objectReferenceValue;
      spawnDirection = serializedObject.FindProperty("spawnDirection").vector3Value;
      rotation = serializedObject.FindProperty("rotation").vector3Value;
      offset = serializedObject.FindProperty("offset").floatValue;
      count = serializedObject.FindProperty("count").intValue;
      
      base.OnInspectorGUI();
      
      if (GUILayout.Button("Spawn"))
      {
        Spawn();
      }
    }

    private void Spawn()
    {
      Vector3 currentSpawnPosition = spawnPosition.position;
      Vector3 shiftPosition = spawnDirection * offset;
      GameObject spawnedObject;
      
      Undo.IncrementCurrentGroup();
      for (int i = 0; i < count; i++)
      {
        currentSpawnPosition += shiftPosition;
        spawnedObject = PrefabUtility.InstantiatePrefab(spawnObject) as GameObject;
        spawnedObject.transform.position = currentSpawnPosition;
        spawnedObject.transform.rotation = Quaternion.Euler(rotation);
        spawnedObject.transform.SetParent(spawnParent);
        Undo.RegisterCreatedObjectUndo(spawnedObject, $"Create Object {spawnedObject.name+i}");
      }
    }
  }
}