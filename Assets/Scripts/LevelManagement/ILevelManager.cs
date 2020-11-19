
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    bool IsGameStarted();
    void StartGame();
    GameObject GetRestartWindow();
    void GameOver();

    List<Transform> GetSpawnedEnemiesList();
}
