using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponChooser : MonoBehaviour
{
    [SerializeField] private GameObject[] _weapons;
    public bool[] WhatUnlocked;
    [SerializeField] private FireExtinguisher _fireExtinguisher;
    [SerializeField] private VacuumCleaner _vacuumCleaner;
    [SerializeField] private PlayerParry _playerParry;
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _handAnimator;
    private int currentIndex;

    private void Awake()
    {
        WhatUnlocked[0] = true;
        _handAnimator =_weapons[0].GetComponent<Animator>();
        currentIndex = 0;
    }

    public void ForceSelect(int index)
    {
        if (_playerMovement.canMove == false) return;
        if (WhatUnlocked[index] == true)
        {
            Reset();
            currentIndex = index;
            TurnoffWeapons(index);

        }
    }

    private readonly string  _nameAnim = "Idle";
    private void Update()
    {
        if (_playerMovement.canMove == false) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentIndex == 0) return;
            _handAnimator.StopPlayback();
            _handAnimator.Play(_nameAnim);
            Reset();
            TurnoffWeapons(0);

            currentIndex = 0;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (WhatUnlocked[1] == true)
            {
                Reset(); 
                TurnoffWeapons(1);

                currentIndex = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (WhatUnlocked[2] == true)
            {
                Reset(); 
                TurnoffWeapons(2);

                currentIndex = 2;
            }
        }
    }

    private void Reset()
    {
        if(_fireExtinguisher != null)
            _fireExtinguisher.Stop();
        if(_vacuumCleaner != null)
            _vacuumCleaner.StopVacuuming();
    }
    private void TurnoffWeapons(int except)
    {
        if (except == 0)
        {
            _playerParry.isActive = true;
        }
        else
        {
            _playerParry.isActive = false;
        }
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i == except)
            {
                _weapons[i].SetActive(true);
                continue;
            }
            _weapons[i].SetActive(false);
        }
    }
}
