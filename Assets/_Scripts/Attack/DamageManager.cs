using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField] private StatsData _statsData;
    [SerializeField] private float _damageCooldown = 1f;
    private float _lastDamageTime = 0f;
    public void Damage(int value)
    {
        if (_lastDamageTime + _damageCooldown <= Time.time)
        {
            _lastDamageTime = Time.time;
            _statsData.Hp = value;
        }
    }
}
