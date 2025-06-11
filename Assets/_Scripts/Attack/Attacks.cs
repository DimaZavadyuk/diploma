using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public abstract class ConcreteAttack : MonoBehaviour
{
    public abstract void Attack(float speed, float cooldownPerAttack, Transform posAttack);
    public UnityEvent OnAttackEnd;
}

