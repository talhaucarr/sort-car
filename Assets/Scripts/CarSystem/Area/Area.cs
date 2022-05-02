using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour, IArea
{
    [SerializeField] private Color areaColor = Color.red;
    [SerializeField] private SpriteRenderer grid;
    public Color AreaColor => areaColor;

    private void Start()
    {
        Subscribe(this);
        int random = Random.Range(0, 2);
        grid.color = LevelManager.Instance.SavedPrefabs.savedColors[random].color;
        areaColor = LevelManager.Instance.SavedPrefabs.savedColors[random].color;
    }

    public void Subscribe(IArea area)
    {
        AreaManager.Instance.areaList.Add(area);
    }

    public void UnSubscribe(IArea area)
    {
        AreaManager.Instance.areaList.Remove(area);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(10 ,0.1f, 20));
    }
}
