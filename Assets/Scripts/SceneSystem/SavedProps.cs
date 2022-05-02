using System;
using System.Collections.Generic;
using PoolSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneData")]
public class SavedProps: ScriptableObject
{
    /*
#if UNITY_EDITOR
    [EasyButtons.Button]
    public void ClearLevel()
    {
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        foreach (GameObject go in gameObjects)
        {
            if (go.TryGetComponent<PoolableProp>(out var poolableProp))
            {
                if(Application.isPlaying)
                    poolableProp.GoToPool();
                else
                    DestroyImmediate(go);
            }
        }
        RemovePoints();
        RemoveNavMesh();
    }

    [EasyButtons.Button]
    public void GenerateLevel()
    {
        foreach (var savedPrefab in savedPrefabs)
        {
            foreach (var savedTransform in savedPrefab.savedTransforms)
            {
                if(Application.isPlaying)
                    ObjectPooler.Instance.Spawn(savedPrefab.propPrefab.name, savedTransform.propPosition, savedTransform.propRotation);
                else
                {
                    GameObject newProp = PrefabUtility.InstantiatePrefab(savedPrefab.propPrefab) as GameObject;
                    newProp.transform.position = savedTransform.propPosition;
                    newProp.transform.rotation = savedTransform.propRotation;
                    newProp.transform.localScale = savedTransform.propScale;
                }
            }
        }
        AddPoints();
        AddNavmesh();
    }
    
    [EasyButtons.Button]
    public void AddPoints()
    {
        foreach (var savedNav in savedNavAI)
        {
            GameObject newProp = PrefabUtility.InstantiatePrefab(savedNav.pointPrefab) as GameObject;
            newProp.transform.position = savedNav.pointPosition;
            newProp.transform.rotation = savedNav.pointRotation;
            newProp.transform.localScale = savedNav.pointScale;
        }
    }
    
    [EasyButtons.Button]
    public void RemovePoints()
    {
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        foreach (GameObject go in gameObjects)
        {
            if (go.TryGetComponent<PoolablePoints>(out var poolable))
            {
                if(Application.isPlaying)
                    Destroy(go);
                else
                    DestroyImmediate(go);
            }
        }
    }
    
    */
    [Header("Colors")]
    public List<SavedColors> savedColors = new List<SavedColors>();

    [Header("NavAI")] 
    public List<SavedRoutes> savedNavAI = new List<SavedRoutes>();

    [Header("Environment")]
    public List<SavedPrefabs> savedPrefabs = new List<SavedPrefabs>();

    [Header("Render")] 
    public SavedRenders savedRenders;
}

[Serializable]
public class SavedColors
{
    public Color color;
}
