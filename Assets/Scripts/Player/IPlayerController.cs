

using UnityEngine;

public interface IPlayerController
{
    void Init();
    void Move(Vector3 vector);
    void StartShoting();
    void StopShoting();
    Vector3 GetPlayerPosition();
}
