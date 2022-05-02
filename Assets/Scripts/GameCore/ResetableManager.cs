using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class ResetableManager : AutoSingleton<ResetableManager>
{
    private readonly List<IResetable> _resetableList = new List<IResetable>();

    public void Subscribe(IResetable resetable)
    {
        _resetableList.Add(resetable);
    }

    public void UnSubscribe(IResetable resetable)
    {
        _resetableList.Remove(resetable);
    }

    public void ResetAllResetables()
    {
        foreach (var resetable in _resetableList)
        {
            resetable.ResetObject();
        }
    }
}
