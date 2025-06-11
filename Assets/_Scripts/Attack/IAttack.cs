using System;
using UnityEngine.Events;
public interface IAttack
{
    public void Attack();
    public UnityEvent OnAttackEnd { get; set; }
}
   