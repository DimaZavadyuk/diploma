using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Rotate_ANIM : Concrete_ANIM
{
    [SerializeField] private Transform _rotateTo;

    override public void StartAnim(int index)
    {
        StartCoroutine(Animate(index));
    }

    override public IEnumerator Animate(int index)
    {
        yield return new WaitForSeconds(_startDelay);

        Vector3 targetRotation = _rotateTo.rotation.eulerAngles;

        transform.DORotate(targetRotation, _durration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
}