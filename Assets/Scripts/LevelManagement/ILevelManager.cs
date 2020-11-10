
using UnityEngine;

public interface ILevelManager
{
    bool IsGameStarted();
    void StartGame();
    GameObject GetRestartWindow();
    void GameOver();
}
