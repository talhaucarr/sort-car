using System;
using System.Collections;
using System.Collections.Generic;
using PoolSystem;
using UnityEngine;
using Utilities;
public class InGameResources : PersistentAutoSingleton<InGameResources>
{
    [SerializeField] public List<PoolPack> poolPacks;
}
