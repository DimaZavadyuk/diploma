using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Flying_ANIM : MonoBehaviour
{
    [SerializeField] private float _waveEffectStrength = 1f;
    [SerializeField] private float _durration = 3f;
    [SerializeField] private float _startDelay = 0f;

     public void StartAnim()
    {
        StartCoroutine(Animate());
    }
     public IEnumerator Animate()
    {
        yield return new WaitForSeconds(_startDelay);
        _waveEffectStrength = transform.position.y + _waveEffectStrength;
       transform.DOMoveY(_waveEffectStrength, _durration)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
