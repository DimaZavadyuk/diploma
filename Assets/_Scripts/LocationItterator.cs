using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LocationItterator : MonoBehaviour
{
    [SerializeField] private GameObject[] _locations;
    [SerializeField] private Transform _toPoint;
    [SerializeField] private Transform _outPoint;
    [SerializeField] private float _duration = 3f;
    [Space(15)]
    [SerializeField] private bool isAlternate = false;
    [SerializeField] private UnityEvent OnSpecial;
    private int _currentIndex = 0;

    private bool alternator = true;
    private int i = 0;

    public void StartNext()
    {
        if (!isAlternate) StartCoroutine(Animate());
        else
        {
            if (i < 3)
            {
                StartCoroutine(Animate());
            }
            else if (i == 3)
            {
                SpecialAnimate();
            }

            i++;
            alternator = !alternator;
        }
    }

    private void SpecialAnimate()
    {
        TurnOff();
        OnSpecial.Invoke();
    }

    private IEnumerator Animate()
    {
        if (_currentIndex > 0)
        {
            TurnOff();
        }
        if (_currentIndex > _locations.Length - 1) yield break;

        var location = _locations[_currentIndex];
        location.SetActive(true);

        location.transform.DOMoveY(_toPoint.position.y, _duration)
            .SetEase(Ease.InOutBack)
            .SetUpdate(true) // Ensure the tween is unscaled (not affected by Time.timeScale)
            .OnComplete(() => location.transform.position = new Vector3(location.transform.position.x, _toPoint.position.y, location.transform.position.z));

        _currentIndex++;
    }

    public void TurnOff()
    {
        var prevIndex = _currentIndex - 1;
        if (prevIndex >= 0)
        {
            var location = _locations[prevIndex];

            var colliders = location.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            location.transform.DOMoveY(_outPoint.position.y, _duration)
                .SetEase(Ease.InOutBack)
                .SetUpdate(true) // Ensure the tween is unscaled (not affected by Time.timeScale)
                .OnComplete(() =>
                {
                    location.transform.position = new Vector3(location.transform.position.x, _outPoint.position.y, location.transform.position.z);
                    location.SetActive(false);
                });
        }
    }

}
