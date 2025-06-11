using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public bool isOnce = true;
    private bool wasPlayed = false;
    public UnityEvent OnTrigger;
    private string _playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if(isOnce && wasPlayed) return;
        if(!other.CompareTag(_playerTag)) return;
        wasPlayed = true;   
        OnTrigger.Invoke();
    }
}
