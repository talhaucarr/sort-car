using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SaveSceneProps : EditorWindow
{
    [MenuItem("Tools/Save Scene SO")]
    private static void SaveScene()
    {
        string prefabPath = "Assets/Prefabs/Environment/";
        string pointPath = "Assets/Prefabs/Routes/";
        
        var Options = AssetDatabase.LoadAssetAtPath<SavedProps>("Assets/Resources/ScriptableObjects/SceneSystem/" + SceneManager.GetActiveScene().name + ".asset");

        if (Options == null)
        {
            Debug.Log("Couldn't find Options. Creating options");
            Options = CreateInstance<SavedProps>();
            AssetDatabase.CreateAsset(Options, ("Assets/Resources/ScriptableObjects/SceneSystem/" + SceneManager.GetActiveScene().name + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        else
        {
            Options.savedPrefabs.Clear();
        }

        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        List<GameObject> goList = new List<GameObject>();
        List<GameObject> pointList = new List<GameObject>();
        List<GameObject> lightList = new List<GameObject>();


        foreach (GameObject go in gameObjects)
        {
            if (go.TryGetComponent<Waypoint>(out var wy) || go.TryGetComponent<PoolablePoints>(out var tp))
            {
                pointList.Add(go);
            }
            
            else if (go.TryGetComponent<PoolableProp>(out var poolableProp))
            {
                goList.Add(go);
            }
            
        }
        
        SavePoints(Options, pointList, pointPath);
        SaveSkybox(Options);
        SaveEnvironment(goList, Options, prefabPath);
    }


    private static void SaveEnvironment(List<GameObject> goList, SavedProps Options, string prefabPath)
    {
        var groupedList = goList.GroupBy(u => u.name.Split(' ')[0]).Select(grp => grp.ToList()).ToList();

        for (int i = 0; i < groupedList.Count; i++)
        {
            for (int j = 0; j < groupedList[i].Count; j++)
            {
                int counter = 0;

                if (Options.savedPrefabs.Count == 0)
                {
                    Options.savedPrefabs.Add(new SavedPrefabs
                    {
                        propPrefab =
                            AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath + groupedList[i][0].name.Split(' ')[0] +
                                                                      ".prefab")
                    });
                    Options.savedPrefabs[counter].savedTransforms.Add(new SavedTransform
                    {
                        propRotation = groupedList[i][0].transform.rotation,
                        propPosition = groupedList[i][0].transform.position,
                        propScale = groupedList[i][0].transform.lossyScale
                    });
                }
                else
                {
                    if (j == 0)
                    {
                        Options.savedPrefabs.Add(new SavedPrefabs
                        {
                            propPrefab =
                                AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath +
                                                                          groupedList[i][0].name.Split(' ')[0] + ".prefab")
                        });
                        Options.savedPrefabs[i].savedTransforms.Add(new SavedTransform
                        {
                            propRotation = groupedList[i][0].transform.rotation,
                            propPosition = groupedList[i][0].transform.position,
                            propScale = groupedList[i][0].transform.lossyScale
                        });
                    }
                    else
                    {
                        Options.savedPrefabs[i].savedTransforms.Add(new SavedTransform
                        {
                            propRotation = groupedList[i][j].transform.rotation,
                            propPosition = groupedList[i][j].transform.position,
                            propScale = groupedList[i][j].transform.lossyScale
                        });
                    }

                    counter++;
                }
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.SaveAssetIfDirty(Options);
    }

    private static void SavePoints(SavedProps options, List<GameObject> pointList, string path)
    {
        AssetDatabase.CreateFolder("Assets/Prefabs/Routes", SceneManager.GetActiveScene().name);
        foreach (var point in pointList)
        {
            PrefabUtility.SaveAsPrefabAsset(point, path + SceneManager.GetActiveScene().name + "/" + point.name + ".prefab");
            options.savedNavAI.Add(new SavedRoutes{pointPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path + SceneManager.GetActiveScene().name + "/" + point.name + ".prefab"), pointPosition = point.transform.position, pointRotation = point.transform.rotation, pointScale = point.transform.localScale});
        }
    }
    private static void SaveSkybox(SavedProps Options)
    {
        Options.savedRenders.skyboxMat = RenderSettings.skybox;
        Options.savedRenders.fogColor = RenderSettings.fogColor;
        Options.savedRenders.fogMode = RenderSettings.fogMode;
        Options.savedRenders.fogDensity = RenderSettings.fogDensity;
    }
}
