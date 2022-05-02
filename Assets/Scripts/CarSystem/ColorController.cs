using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolSystem;
public class ColorController : MonoBehaviour
{
    [SerializeField] private Color carColor;
    [SerializeField] private MeshRenderer carRenderer;
    public Color CarColor
    {
        get => carColor;
        set => carColor = value;
    }

    public void ChangeCarColor(Color color)
    {
        Material myNewMaterial = new Material(Shader.Find("Standard"));
        myNewMaterial.color = color;

        CopyMaterials(myNewMaterial);
    }

    private void CopyMaterials(Material myNewMaterial)
    {
        var intMaterials = new Material[carRenderer.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {
            if (i == 0)
            {
                intMaterials[i] = myNewMaterial;
            }
            else { intMaterials[i] = carRenderer.materials[i]; }

        }
        carRenderer.materials = intMaterials;
    }
}
