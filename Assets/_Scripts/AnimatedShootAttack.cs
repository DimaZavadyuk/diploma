using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AnimatedShootAttack : ConcreteAttack
{
    [SerializeField] private float _animDuration;
    [SerializeField] private float _endANimPosY;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private UnityEvent _onShoot;
    private GameObject _objectBullet;

    public override void Attack(float speed, float cooldDownPerAttack, Transform _originBulletPosition)
    {
        _objectBullet = Instantiate(_bullet);
        _objectBullet.transform.position = _originBulletPosition.position;

        _objectBullet.transform.DOMoveY(_endANimPosY, _animDuration).SetEase(Ease.OutQuint)
            .OnComplete (()=>
            {
                StartCoroutine(ProcessAttack(speed, cooldDownPerAttack, _originBulletPosition));
            });
    }
    
    private IEnumerator ProcessAttack(float speed, float cooldDownPerAttack, Transform _originBulletPosition)
    {
        _onShoot.Invoke();
        var componentBullet = _objectBullet.GetComponent<Bullet>();
        componentBullet.IsFlyingImmediatly = true;
        componentBullet.Shoot(_playerTransform.position, speed);
        yield return new WaitForSeconds(cooldDownPerAttack);
        OnAttackEnd.Invoke();
    }
    
}
