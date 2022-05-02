using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public class GoalManager : AutoSingleton<GoalManager>
{
    [SerializeField] private int goalCounter;

    public int GoalCounter => goalCounter;

    public UnityEvent OnAllGoalCompleted = new UnityEvent();

    public void SetGoalCounter(int counter)
    {
        goalCounter = counter;
    }

    public void IncreaseGoalCounter()
    {
        goalCounter++;
    }

    public void DecreaseGoalCounter()
    {
        goalCounter--;
        if (goalCounter == 0) OnAllGoalCompleted?.Invoke();
    }
}
