using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioData[] _audioClips;

    public void Play(int index)
    {
        SoundFXManager.Instance.PlaySoundFXClip(_audioClips[index]._audioClip, transform, 1f, _audioClips[index].is3D);
    }
}
