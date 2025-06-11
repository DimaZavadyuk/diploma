using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private RespawnManager _respawnManager;

    private void Awake()
    {
        _respawnManager = FindObjectOfType<RespawnManager>();
    }
    private string _playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_playerTag)) return;
        _respawnManager._lastRespawnPosition = transform;
    }
}
