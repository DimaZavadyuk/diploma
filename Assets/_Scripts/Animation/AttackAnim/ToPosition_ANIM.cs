using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ToPosition_ANIM : Concrete_ANIM
{
    [SerializeField] private Transform[] _toPoint;
    public override void StartAnim(int index)
    {
        StartCoroutine(Animate(index));
    }

    public override IEnumerator Animate(int index)
    {
        yield return new WaitForSeconds(_startDelay);
        transform.DOMove(_toPoint[index].position, _durration).SetEase(Ease.InOutQuad)
            .OnComplete (()=> 
            { 
                OnAnimationEnd.Invoke();
            });;
    }
}
