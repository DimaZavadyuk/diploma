using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityUtils;

public class PlayerParry : MonoBehaviour
{
    public bool isActive = true;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _sphereRadius = 1f; 
    [SerializeField] private LayerMask _parryLayerMask;
    [SerializeField] private Gradient _playerBulletGradient;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private UnityEvent _OnParry;
    [SerializeField] private UnityEvent _OnTryParry;
    [SerializeField] private UnityEvent _OnSpecialRatParry;
    [SerializeField] private UnityEvent _OnSpecialFinalParry;
    [SerializeField] private Material _handRenderer;
    [SerializeField] private Color _defaultHandColor;
    [SerializeField] private Color _specialHandColor;
    [SerializeField] private float _parryDelay = 0.5f;
    [Space(20)] [SerializeField] private AudioClip _successHit;// Configurable delay for parry
    [Space(20)] [SerializeField] private AudioClip _emptyHit;// Configurable delay for parry

    private string _enemyTag = "enemy";
    private bool _canParry = true;

    private void Update()
    {
        if(!isActive) return;
        if (!_canParry) return;
        if (!_playerMovement.canMove) return;

        Vector3 sphereCenter = _cameraTransform.position + _cameraTransform.forward * _distance;
        Collider[] hits = Physics.OverlapSphere(sphereCenter, _sphereRadius, _parryLayerMask);

        _handRenderer.color = _defaultHandColor;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(HandleParry(hits));
        }
    }

    private IEnumerator HandleParry(Collider[] hits)
    {
        _OnTryParry.Invoke();

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                _handRenderer.color = _specialHandColor;
                SoundFXManager.Instance.PlaySoundFXClip(_successHit, transform, 0.4f, false);

                if (hit.CompareTag(_enemyTag))
                {
                    _OnParry.Invoke();
                    _OnSpecialRatParry.Invoke();
                    _parryCor = StartCoroutine(ParryCooldown());
                    yield break;
                }
                if (hit.CompareTag(_enemyTag) && hit.transform.GetComponent<PhaseManager>())
                {
                    _OnParry.Invoke();
                    _OnSpecialFinalParry.Invoke();
                    _parryCor = StartCoroutine(ParryCooldown());
                    yield break;
                }

                _OnParry.Invoke();
                Bullet bullet = hit.GetComponentInParent<Bullet>();
                if (bullet != null && !bullet.isPlayerBullet)
                {
                    bullet.isPlayerBullet = true;
                    bullet.direction = _cameraTransform.forward;
                    bullet._speed *= 2;
                    bullet.GetComponent<DamageArea>()._isOwnedByPlayer = true;
                    bullet.GetComponent<TrailRenderer>().colorGradient = _playerBulletGradient;
                    _parryCor = StartCoroutine(ParryCooldown());
                    yield break;
                }
            }
        }
        SoundFXManager.Instance.PlaySoundFXClip(_emptyHit, transform, 0.4f, false);
        StartCoroutine(ParryCooldown());
    }

    private void OnDisable()
    {
        if(_parryCor != null)
        StopCoroutine(_parryCor);
        _parryCor = null;
        _canParry = true;
    }

    private Coroutine _parryCor;
    private IEnumerator ParryCooldown()
    {
        _canParry = false; // Disable parrying
        yield return new WaitForSeconds(_parryDelay); // Wait for the configured delay
        _canParry = true; // Enable parrying again
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 sphereCenter = _cameraTransform.position + _cameraTransform.forward * _distance;
        Gizmos.DrawWireSphere(sphereCenter, _sphereRadius);
    }
}
