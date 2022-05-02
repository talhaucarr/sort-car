using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolSystem;

public class CarController : MonoBehaviour, IPoolable, IResetable
{
    private CarMovement _carMovement;
    private ColorController _colorController;
    private bool _isCompared;

    private void Start()
    {
        SubscribeToResetable();
    }

    void Update()
    {
        if (!_carMovement.IsMoving)
        {
            CheckColors();
            return;
        }

        _carMovement.Move(_carMovement.NextDir);
        _carMovement.CheckTargetReached();
    }

    private void CheckColors()
    {
        if (!_isCompared)
        {
            _isCompared = true;         
            _colorController = GetComponent<ColorController>();

            if (CompareColors(_colorController.CarColor, _carMovement.CurrentRoute.Area.AreaColor))
            {
                Debug.Log($"Color area {_carMovement.CurrentRoute.Area.name}");
                GoalManager.Instance.DecreaseGoalCounter();
            }

            else
            {
                Debug.Log($"Color area {_carMovement.CurrentRoute.Area.name}");
                GameManager.Instance.ChangeGameState(GameState.Lose);
            }
        }
    }

    public bool CompareColors(Color c1, Color c2)
    {
        return Mathf.Abs(c1.r - c2.r) < 0.01f && Mathf.Abs(c1.g - c2.g) < 0.01f && Mathf.Abs(c1.b - c2.b) < 0.01f;
    }

    public void OnGoToPool()
    {
        Debug.Log($"OnGoToPool");
    }

    public void OnPoolSpawn()
    {
        Debug.Log($"OnPoolSpawn");
        _carMovement = GetComponent<CarMovement>();
        _carMovement.GetStartPoint();
        _carMovement.IsMoving = true;
        _isCompared = false;
    }

    public void SubscribeToResetable()
    {
        ResetableManager.Instance.Subscribe(this);
    }

    public void ResetObject()
    {
        GetComponent<PoolObject>().GoToPool();
    }
}
