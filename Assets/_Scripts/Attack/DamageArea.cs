using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private int _damagePerHit = 10;
    [SerializeField] private float cooldown = 0f;
    public bool _isOwnedByPlayer = false;
    public bool _isArea = false;
    private Coroutine _cooldownCor;
    private void OnTriggerStay(Collider other)
    {
        if (_cooldownCor == null && _isArea)
        {
            StartDamage(other.gameObject);
        }
        else if(!_isArea)
        {
            StartCoroutine(Damage(other.gameObject));
        }
    }

    public void StartDamage(GameObject other)
    {
        _cooldownCor = StartCoroutine(Damage(other.gameObject));
    }

    private string _playerTag = "Player";
    public IEnumerator Damage(GameObject other)
    {
        if (_isOwnedByPlayer)
        {
            if (other.GetComponent<StatsData>() && !other.GetComponent<PlayerMovement>())
            {
                other.GetComponent<DamageManager>().Damage(-_damagePerHit);
            }
        }
        else
        {
            if (other.CompareTag(_playerTag))
            {
                other.GetComponent<DamageManager>().Damage(-_damagePerHit);
                if (_isArea)
                {
                    yield return new WaitForSeconds(cooldown);
                }
                _cooldownCor = null;
            }
        }
    }
}
