using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AreaManager : PersistentAutoSingleton<AreaManager>
{
    public List<IArea> areaList = new List<IArea>();
}
