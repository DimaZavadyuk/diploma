using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [HideInInspector] public Transform _lastRespawnPosition;
    [SerializeField] private GameObject _player;

    public void RespawnPlayer()
    {
        if(_lastRespawnPosition != null)
        _player.transform.position = _lastRespawnPosition.position;
    }
}
