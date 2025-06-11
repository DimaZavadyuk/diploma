using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class  Concrete_ANIM : MonoBehaviour
{
    public float _waveEffectStrength = 1f;
    public float _durration = 3f;
    public float _startDelay = 0f;
    public UnityEvent OnAnimationEnd;
    public bool isOnEnd = false;
    abstract public void StartAnim(int index);
    abstract public IEnumerator Animate(int index);
}
