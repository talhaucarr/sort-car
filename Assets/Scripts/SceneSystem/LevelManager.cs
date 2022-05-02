using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using PoolSystem;

public class LevelManager : AutoSingleton<LevelManager>
{
    private SavedProps _savedPrefabs;
    private Object[] _gameObjects;
   

    public SavedProps SavedPrefabs => _savedPrefabs;
    public List<SavedColors> SavedColors { get; private set; } 

    private void Start()
    {
        OnFirstLevelLoad();
    }

    public void LevelLose()
    {
        ClearLevel();
        StartCoroutine(DelayedLoadLevel());
    }

    public void LevelWin()
    {
        ClearLevel();
        StartCoroutine(DelayedLoadLevel());
    }

    private IEnumerator DelayedLoadLevel()
    {
        yield return new WaitForSeconds(2);
        OnFirstLevelLoad();
    }

    public void OnFirstLevelLoad()
    {
        _savedPrefabs = Resources.Load<SavedProps>("ScriptableObjects/SceneSystem/" + "Level1");//Save sistemine baðlandýðý zaman burasý bulunduðu level'a göre yükleyecke þekilde düzenlenir.
        GenerateLevel();
    }

    public void OnLevelChanged()
    {
        _savedPrefabs = Resources.Load<SavedProps>("ScriptableObjects/SceneSystem/" + "Level1");//Save sistemine baðlandýðý zaman burasý bulunduðu level'a göre yükleyecke þekilde düzenlenir.
        ClearLevel();
        GenerateLevel();
    }

    private void ClearLevel()
    {
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        foreach (GameObject go in gameObjects)
        {
            if (go.TryGetComponent<PoolableProp>(out var poolableProp))
            {
                poolableProp.GoToPool();
            }
        }
        RemovePoints();
    }

    private void GenerateLevel()
    {
        
        foreach (var savedPrefab in _savedPrefabs.savedPrefabs)
        {
            foreach (var savedTransform in savedPrefab.savedTransforms)
            {
                GameObject newObject = ObjectPooler.Instance.Spawn(savedPrefab.propPrefab.name, savedTransform.propPosition, savedTransform.propRotation);
                newObject.transform.localScale = savedTransform.propScale;
            }
        }
        AddPoints();
        ChangeRenderSettings();
    }

    private void AddPoints()
    {
        int counter = 0;
        foreach (var savedNav in _savedPrefabs.savedNavAI)
        {          
            GameObject newProp = Instantiate(savedNav.pointPrefab) as GameObject;
            if (newProp.TryGetComponent<PoolableRoute>(out var pr)) counter = newProp.transform.childCount;
            newProp.transform.position = savedNav.pointPosition;
            newProp.transform.rotation = savedNav.pointRotation;
            newProp.transform.localScale = savedNav.pointScale;
        }

        SetGoals(counter);
    }

    private void SetGoals(int counter)
    {
        GoalManager.Instance.SetGoalCounter(counter);
     }
    private void RemovePoints()
    {
        GetObjectsInScene();

        foreach (GameObject go in _gameObjects)
        {
            if (go.TryGetComponent<PoolablePoints>(out var poolable))
            {
                Destroy(go);
            }
        }
    }

    private void GetObjectsInScene()
    {
        _gameObjects = FindObjectsOfType(typeof(GameObject));
    }

    private void ChangeRenderSettings()
    {
        RenderSettings.skybox = _savedPrefabs.savedRenders.skyboxMat;
        RenderSettings.fogColor = _savedPrefabs.savedRenders.fogColor;
        RenderSettings.fogMode = _savedPrefabs.savedRenders.fogMode;
        RenderSettings.fogDensity = _savedPrefabs.savedRenders.fogDensity;
    }
}
