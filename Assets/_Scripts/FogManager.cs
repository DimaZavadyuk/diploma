using DG.Tweening;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] private FogData _defaultFog;
    [SerializeField] private float _duration = 2f;

    private void Awake()
    {
        UpdateFog(_defaultFog);
    }

    public void UpdateFog(FogData fogData)
    {
        RenderSettings.fogDensity = fogData.Density;
        RenderSettings.fogColor = fogData.Color;
    }
    public void SetDefault()
    {
        RenderSettings.fogDensity = _defaultFog.Density;
        RenderSettings.fogColor = _defaultFog.Color;
    }

    public void LerpUpdateFog(FogData fogData)
    {
        DOTween.To(() => RenderSettings.fogDensity, (x) => RenderSettings.fogDensity = x, fogData.Density, _duration);
        DOTween.To(() => RenderSettings.fogColor, (x) => RenderSettings.fogColor = x, fogData.Color, _duration/2);
    }
}