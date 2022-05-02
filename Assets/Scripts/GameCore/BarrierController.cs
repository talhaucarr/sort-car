using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Start()
    {
        var spawnPoint = SpawnManager.Instance.GetClosestPoint(transform);
        spawnPoint.BarrierAnimator = GetComponent<Animator>();
        _renderer = GetComponent<MeshRenderer>();
        ChangeCarColor(spawnPoint.SpawnColor);
    }

    public void ChangeCarColor(Color color)
    {
        Material myNewMaterial = new Material(Shader.Find("Standard"));
        myNewMaterial.color = color;

        CopyMaterials(myNewMaterial);
    }

    private void CopyMaterials(Material myNewMaterial)
    {
        var intMaterials = new Material[_renderer.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {
            intMaterials[i] = myNewMaterial;
        }
        _renderer.materials = intMaterials;
    }
}
