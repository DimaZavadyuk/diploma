using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _mixerName;
    [SerializeField] private Slider _soundSlider;


    private void Awake()
    {
        _audioMixer.GetFloat(_mixerName, out float masterVolume);
        _soundSlider.value =(Mathf.Pow(10, masterVolume / 20f));
    }

    public void SetVolume(float level)
    {
        _audioMixer.SetFloat(_mixerName, Mathf.Log10(level) * 20f);
    }
}
