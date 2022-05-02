using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class WaypointsManager : AutoSingleton<WaypointsManager>
{
    [SerializeField] private List<AWaypoint> waypointList = new List<AWaypoint>();

    public void Subscribe(AWaypoint waypoint)
    {
        waypointList.Add(waypoint);
    }

    public void UnSubscribe(AWaypoint waypoint)
    {
        waypointList.Remove(waypoint);
    }

    public List<AWaypoint> GetWaypoints()
    {
        return waypointList;
    }
    
    public AWaypoint  GetClosestWaypoint(Transform playerPosition)
    {
        float minDistance = Mathf.Infinity;
        AWaypoint tempWaypoint = null;
        foreach (AWaypoint waypoint in waypointList)
        {
            //if (waypoint.IsPathFull) continue;
            float currentDistance = Vector3.Distance(waypoint.GetClosestPoint(transform).position, playerPosition.position);
            if (currentDistance < minDistance /*&& !CheckWaypointIsEmpty(waypoint)*/)
            {
                minDistance = currentDistance;
                tempWaypoint= waypoint;
            }
        }

        if (tempWaypoint != null)
        {
            tempWaypoint.CurrentEnemyCount++;
            if(tempWaypoint.CurrentEnemyCount == tempWaypoint.MaxEnemyCount) tempWaypoint.IsPathFull = true;
        }
        
        return tempWaypoint;
    }

    public void ClearWaypointList()
    {
        foreach (var aWaypoint in waypointList)
        {
            Destroy(aWaypoint.gameObject);
        }
        waypointList.Clear();
    }

    public void ClearCurrentEnemyCount()
    {
        foreach (var waypoint in waypointList)
        {
            waypoint.CurrentEnemyCount = 0;
            waypoint.IsPathFull = false;
        }
    }
    
    private bool CheckWaypointIsEmpty(AWaypoint waypoint)
    {
        return waypoint.CheckPathIsEmpty();
    }
}
