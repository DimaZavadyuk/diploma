using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ShootAttack : ConcreteAttack
{
    [SerializeField] private UnityEvent _onShoot;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _originBulletPosition;
    public override void Attack(float speed, float cooldDownPerAttack, Transform posAttack)
    {
        StartCoroutine(ProcessAttack(speed, cooldDownPerAttack));
    }

    private IEnumerator ProcessAttack(float speed, float cooldDownPerAttack)
    {
        _onShoot.Invoke();
        var bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _originBulletPosition.position;
        bullet.GetComponent<Bullet>().Shoot(_playerTransform.position, speed);
        yield return new WaitForSeconds(cooldDownPerAttack);
        OnAttackEnd.Invoke();
    }
}