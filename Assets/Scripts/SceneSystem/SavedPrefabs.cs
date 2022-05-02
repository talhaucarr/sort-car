using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SavedPrefabs
{
    [SerializeField] public GameObject propPrefab;
    public List<SavedTransform> savedTransforms = new List<SavedTransform>();
}
