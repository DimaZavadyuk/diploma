using UnityEngine;
using UnityEngine.UI;

public class IntSliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private StatsData _statsData;
    
    private void Awake()
    {
        _slider.maxValue = _statsData.Hp;
        _slider.value = _statsData.Hp;
    }

    public void UpdateSlider(int value)
    {
        _slider.value = value;
    }
}
