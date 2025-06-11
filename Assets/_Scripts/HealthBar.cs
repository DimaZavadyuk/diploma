using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _easeHpSlider;
    [SerializeField] private StatsData _statsData;
    [SerializeField] private bool isShield = false;
    public void Awake()
    {
        if (isShield)
        {
            _hpSlider.maxValue = _statsData._maxShield;
            _hpSlider.value = _statsData._shield;
            _easeHpSlider.maxValue = _statsData._maxShield;
            _easeHpSlider.value = _statsData._shield;
            _health = _statsData._shield;
            return;
        }
        _hpSlider.maxValue = _statsData.Hp;
        _hpSlider.value = _statsData.Hp;
        _easeHpSlider.maxValue = _statsData.Hp;
        _easeHpSlider.value = _statsData.Hp;
        _health = _statsData.Hp;
    }

    private int _health = 1;
    public void UpdateBar(int health, int maxHealth)
    {
        _health = health;
    }

    private void Update()
    {
        _easeHpSlider.value = Mathf.Lerp(_easeHpSlider.value, _health, _lerpSpeed);
        _hpSlider.value = _health;
    }
}
