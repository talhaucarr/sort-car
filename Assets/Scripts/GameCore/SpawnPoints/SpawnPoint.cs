using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolSystem;

public class SpawnPoint : MonoBehaviour, ISpawnPoint
{
    [SerializeField] private Animator barrierAnimator;
    [SerializeField] private Color spawnColor;
    [SerializeField] private int spawnID;

    public Color SpawnColor => spawnColor;

    public Animator BarrierAnimator
    {
        get => barrierAnimator;
        set => barrierAnimator = value;
    }

    private ColorController _colorController;

    private void Start()
    {
        SpawnManager.Instance.Subscribe(this);       
    }

    private void GetClosestButton()
    {
        var button = ButtonManager.Instance.GetClosestButton(transform);
        button.CarSpawnPoint = this;
        button.ChangeCarColor(spawnColor);
    }

    private void OnDestroy()
    {
        SpawnManager.Instance.UnSubscribe(this);
    }

    public void SpawnCar()
    {
        barrierAnimator.SetTrigger("Barrier");
        GameObject newCar = ObjectPooler.Instance.Spawn("Car", transform.position);
        _colorController = newCar.GetComponent<ColorController>();
        _colorController.CarColor = spawnColor;
        _colorController.ChangeCarColor(spawnColor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }

    public void SetColor(Color color)
    {
        spawnColor = color;
        GetClosestButton();
    }

    public int GetIndex()
    {
        return spawnID;
    }
}
