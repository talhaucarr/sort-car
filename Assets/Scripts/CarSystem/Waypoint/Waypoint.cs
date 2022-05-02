using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Waypoint : AWaypoint
{
    [SerializeField] public List<Transform> points = new List<Transform>();

    private int _currentIndex = 0;
    
    public override List<Transform> GetPointList()
    {
        return points;
    }

    public override Transform GetClosestPoint(Transform enemyPosition)
    {
        float minDistance = Mathf.Infinity;
        Transform tempTransform = null;
        float tempDistance;
        
        foreach (Transform pointTransform in points)
        {
            tempDistance = Vector3.Distance(pointTransform.position, enemyPosition.position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                tempTransform = pointTransform;
            }
        }
        return tempTransform;
    }


    public override bool CheckPathIsEmpty()
    {
        return IsPathFull;
    }

    public override Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
            return points[0];
        
        if (_currentIndex == points.Count)
            _currentIndex = 0;
        
        return points[_currentIndex++];
    }

    public override void SetPointIndex(Transform pointTransform)
    {
        int i = 0;
        foreach (var point in points)
        {
            if (pointTransform.gameObject.name == point.name)
                _currentIndex = i;
            i++;
        }

        _currentIndex++;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Waypoint))]
[Serializable]
class PointProviderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle s = new GUIStyle(EditorStyles.boldLabel);
        s.normal.textColor = Color.cyan;
        GUILayout.Label("Point",s);
        
        Waypoint script = (Waypoint)target;
        if (GUILayout.Button("Create Waypoint"))
        {
            GameObject point = new GameObject();
            point.transform.parent = script.transform;
            point.transform.position = script.transform.position;
            point.name = script.transform.childCount.ToString();
            script.points.Add(point.transform);
        }
        GUILayout.Space(15);
        if (GUILayout.Button("Clear Waypoint List"))
        {
            
            /*foreach (Transform child in script.transform) {
            
                Destroy(child);
            }*/
            script.points.Clear();
        }
        GUILayout.Space(15);
        base.OnInspectorGUI();
    }
    
    
}
#endif
