using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SideToSide_ANIM : MonoBehaviour
{
    [SerializeField] private float _waveEffectStrength = 1f;
    [SerializeField] private float _durration = 3f;
    [SerializeField] private float _startDelay = 0f;

    public void StartAnim()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(_startDelay);
        _waveEffectStrength = transform.position.z + _waveEffectStrength;
        transform.DOMoveZ(_waveEffectStrength, _durration, false)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);
    }
}
