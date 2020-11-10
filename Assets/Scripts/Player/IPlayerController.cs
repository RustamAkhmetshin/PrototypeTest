

using UnityEngine;

public interface IPlayerController
{
    void Init();
    void StopShoting();
    Vector3 GetPlayerPosition();
    Transform GetPlayerTranssform();
}
