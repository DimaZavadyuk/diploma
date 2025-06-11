using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FullRotate_ANIM : Concrete_ANIM
{
    override public  void StartAnim(int index)
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), _durration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    }

    override public  IEnumerator Animate(int index)
    {
        throw new System.NotImplementedException();
    }
}
