using UnityEngine;
using UnityEngine.Events;

public class ItemUnlocker : MonoBehaviour,IInteractable
{
    [SerializeField] private int _cellToUnlock;
    [SerializeField] private WeaponChooser _weaponChooser;
    [SerializeField] private UnityEvent OnPickup;
    public void Interact()
    {
        OnPickup.Invoke();
        _weaponChooser.WhatUnlocked[_cellToUnlock] = true;
        _weaponChooser.ForceSelect(_cellToUnlock);
        Destroy(gameObject);
    }
}
