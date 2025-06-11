using System;
using UnityEngine;
using UnityEngine.Events;

public class StatsData : MonoBehaviour
{

    [SerializeField] private int _hp = 100;
    public int _shield = 0;
    public int _maxShield = 0;
    [HideInInspector] public int MaxHp;
    [SerializeField] private UnityEvent<int, int> _OnHpChange;
    [SerializeField] private UnityEvent<int> _OnDamage;
    [SerializeField] private UnityEvent<int, int> _OnShieldDamage;
    [SerializeField] private UnityEvent _OnShieldZero;
    [SerializeField] private UnityEvent<int> _OnHeal;
    [SerializeField] private UnityEvent _OnDeath;

    private void Awake()
    {
        MaxHp = _hp;
    }

    public void RenewShield()
    {
        _shield = _maxShield;
        _OnShieldDamage.Invoke(_shield, _maxShield);
    }

    public int Hp
    {
        get => _hp;
        set
        {
            if (_shield > 0)
            {
                _shield += value;
                if(_shield <= 0 ) _OnShieldZero.Invoke();
                _OnShieldDamage.Invoke(_shield, _maxShield);
                return;
            }
            _hp += value;

            if (_hp <= 0)
            {
                _hp = 0;
                _OnHpChange.Invoke(_hp, MaxHp);
                _OnDeath.Invoke();
                return;
            }
            _OnHpChange.Invoke(_hp, MaxHp);
            if (value < 0 )
            {
                _OnDamage.Invoke(_hp);
            }
            if (value > 0)
            {
                _OnHeal.Invoke(value);
            }
            
        } 
    }
}
