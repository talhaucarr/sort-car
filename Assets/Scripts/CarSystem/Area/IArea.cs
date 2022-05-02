using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArea
{
    void Subscribe(IArea area);
    void UnSubscribe(IArea area);
}
