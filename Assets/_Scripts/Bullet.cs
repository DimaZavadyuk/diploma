using System;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float _speed = 1f;
    public Vector3 direction = Vector3.forward;
    private bool Once = false;
    private bool isInitialized = false;
    public bool IsFlyingImmediatly = true;
    private Rigidbody _rb;
    public bool isPlayerBullet = false;
    private DamageArea _damageArea;
    public void Shoot(Vector3 firePoint, float speed)
    {
        if(Once) return;
        Once = true;
        _speed = speed;
        isInitialized = true;
        direction = firePoint - transform.position;
        direction = direction.normalized;
        _rb = GetComponent<Rigidbody>();
        _damageArea = GetComponent<DamageArea>();
    }
    private void Update()
    {
        if (isInitialized && IsFlyingImmediatly)
        {
            _rb.MovePosition(transform.position + direction * (_speed * Time.deltaTime));
        }
    }
    private string _tagEnemy = "enemy";
    private string _tagPlayer = "Player";
    private string _tagBullet = "Unhittable";
    [SerializeField] private UnityEvent<GameObject> _onTriggerEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagBullet) || isInitialized == false) return;
        if (_damageArea._isOwnedByPlayer == false && other.CompareTag(_tagEnemy)) return;
        if (_damageArea._isOwnedByPlayer == true && other.CompareTag(_tagPlayer)) return;
        _onTriggerEnter?.Invoke(other.gameObject);
        Destroy(gameObject);
    }
}
