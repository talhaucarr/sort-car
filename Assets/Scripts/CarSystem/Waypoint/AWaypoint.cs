using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWaypoint : MonoBehaviour
{
    [Header("Point Settings")]
    [Range(0.1f, 2f)]
    [SerializeField] private float waypointRadius = 1f;
    
    [Header("Route Settings")]
    [Range(0f, 8f)] 
    [SerializeField] private float maxRandomDelay;
    [SerializeField] private int maxEnemyCount;

    #region PROPERTIES

    public bool IsPathFull { get; set; }
    public int CurrentEnemyCount { get; set; }
    public int MaxEnemyCount => maxEnemyCount;
    public float MaxRandomDelay => maxRandomDelay;

    #endregion

    #region UNITY FUNCTIONS

    private void Awake()
    {
        WaypointsManager.Instance.Subscribe(this);
    }

    private void OnDestroy()
    {
        WaypointsManager.Instance.UnSubscribe(this);
    }

    #endregion
    
    public abstract List<Transform> GetPointList();
    public abstract Transform GetClosestPoint(Transform enemyPosition);
    public abstract bool CheckPathIsEmpty();
    public abstract Transform GetNextWaypoint(Transform currentWaypoint);
    public abstract void SetPointIndex(Transform pointTransform);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(t.position, waypointRadius);
        }

        Gizmos.color = Color.green;
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            //Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        //if(transform.childCount != 0) Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
#endif
}
