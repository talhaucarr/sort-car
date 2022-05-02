using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SpawnManager : AutoSingleton<SpawnManager>
{
    [SerializeField] private List<ISpawnPoint> spawnPoints = new List<ISpawnPoint>();

    public List<ISpawnPoint> GetSpawnPoints()
    {
        return spawnPoints;
    }

    public void Subscribe(ISpawnPoint spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
        spawnPoint.SetColor(LevelManager.Instance.SavedPrefabs.savedColors[spawnPoint.GetIndex()].color);
    }

    public void UnSubscribe(ISpawnPoint spawnPoint)
    {
        spawnPoints.Remove(spawnPoint);
    }

    public SpawnPoint GetClosestPoint(Transform spawnPoint)
    {
        float minDistance = Mathf.Infinity;
        SpawnPoint tempRoute = null;
        foreach (SpawnPoint rt in spawnPoints)
        {

            float distance = Vector3.Distance(spawnPoint.position, rt.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                tempRoute = rt;
            }
        }
        return tempRoute;
    }
}
