using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPoint
{
    void SpawnCar();
    void SetColor(Color color);
    int GetIndex();
}
