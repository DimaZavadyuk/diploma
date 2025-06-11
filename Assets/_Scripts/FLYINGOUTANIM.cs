using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FLYINGOUTANIM : MonoBehaviour
{
    [SerializeField] private Transform _toPoint;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private float _selfUpPosition = 10f;

    public void StartAnim()
    {
        Animate();
    }

    private void Animate()
    {
        transform.DOMoveY(_toPoint.position.y, _duration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true) // Ensure the tween is unscaled (not affected by Time.timeScale)
            .OnComplete(() =>
            {
                // Correct the position to ensure it reaches the target
                var newPos = new Vector3(transform.position.x, _toPoint.position.y, transform.position.z);
                transform.position = newPos;
            });
    }

    public void StartSelfPositionAnim()
    {
        StartCoroutine(SelfPositionAnimate());
    }

    private IEnumerator SelfPositionAnimate()
    {
        yield return new WaitForSecondsRealtime(_startDelay); // Use WaitForSecondsRealtime for unscaled delay

        transform.DOMoveY(_selfUpPosition, _duration)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true); // Ensure the tween is unscaled (not affected by Time.timeScale)
    }
}