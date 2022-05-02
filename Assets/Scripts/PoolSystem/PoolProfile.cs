using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
    [CreateAssetMenu(fileName = "NewPoolProfile", menuName = "ScriptableObjects/PoolProfile", order = 5)]
    public partial class PoolProfile : ScriptableObject
    {
        public List<string> poolPackNames;

        public List<PoolPack> LoadReferenceFromInGameResources()
        {
            List<PoolPack> poolPacks = new List<PoolPack>();
            int counter = 0;
            foreach (string poolPackName in poolPackNames)
            {
                foreach (PoolPack pack in InGameResources.Instance.poolPacks)
                {
                    if (pack.name == poolPackName) 
                    { 
                        poolPacks.Add(pack);
                        break;
                    }
                }

                counter++;
                if (counter != poolPacks.Count) Debug.LogError("Pool Pack Reference cant be found in InGameResources: "+poolPackName);
            }
            return poolPacks;
        }
    }
}
