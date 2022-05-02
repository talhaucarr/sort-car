using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using TMPro;

public class GameManager : AutoSingleton<GameManager>
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    public UnityEvent OnStateChanged = new UnityEvent();

    private void Start()
    {
        GoalManager.Instance.OnAllGoalCompleted.AddListener(Win);
    }

    private void Win()
    {
        ChangeGameState(GameState.Win);
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Level:
                //LevelManager.Instance.GenerateLevel();
                break;
            case GameState.Win:
                winText.SetActive(true);
                LevelManager.Instance.LevelWin();
                break;
            case GameState.Lose:
                loseText.SetActive(true);
                LevelManager.Instance.LevelLose();
                break;
        }
        StartCoroutine(DelayedChangedSetActiveTexts());
        Debug.Log($"Current Game State {state}");
        ResetableManager.Instance.ResetAllResetables();
        OnStateChanged?.Invoke();
    }

    private IEnumerator DelayedChangedSetActiveTexts()
    {
        yield return new WaitForSeconds(2);
        winText.SetActive(false);
        loseText.SetActive(false);
    }
}
