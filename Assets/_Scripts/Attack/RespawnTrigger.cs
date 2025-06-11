using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnTrigger : MonoBehaviour
{
    private RespawnManager _respawnManager;
    [SerializeField] private DamageManager _damageManager;
    [SerializeField] private int _damage = 10;
    private void Awake()
    {
        _respawnManager = FindObjectOfType<RespawnManager>();
    }

    private string _playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_playerTag)) return;
        _damageManager.Damage(-_damage);
        _respawnManager.RespawnPlayer();
    }
}
