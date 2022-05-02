using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private SpawnPoint carSpawnPoint;
    [SerializeField] private MeshRenderer chamfer;
    private Animator _animator;

    public SpawnPoint CarSpawnPoint
    {
        get => carSpawnPoint;
        set => carSpawnPoint = value;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        ButtonManager.Instance.Subscribe(this);
    }

    private void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name.Equals(gameObject.name))
                    {
                        SpawnCar();
                        _animator.SetTrigger("Click");
                    }
                        
                }
                
            }
        }
    }

    private void SpawnCar()
    {
        carSpawnPoint.SpawnCar();
    }

    public void ChangeCarColor(Color color)
    {
        Material myNewMaterial = new Material(Shader.Find("Standard"));
        myNewMaterial.color = color;

        CopyMaterials(myNewMaterial);
    }

    private void CopyMaterials(Material myNewMaterial)
    {
        var intMaterials = new Material[chamfer.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {          
            intMaterials[i] = myNewMaterial;
        }
        chamfer.materials = intMaterials;
    }
}
