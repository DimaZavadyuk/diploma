using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AttackPatternData
{
    public ConcreteAttack _attack;
    public int amount = 1;
    public float _speed = 100f;
    public float _cooldownPerAttack = 0.2f;
    public float _cooldownToNextAttack = 1f;
    public Transform _position;
    public UnityEvent OnEnd;
    
}
