using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    public void SubscribeToResetable();
    public void ResetObject();
}
