using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class ClickableDialogueView : MonoBehaviour
{
    [SerializeField] private Image _spriteHolder;
    [SerializeField] private TextMeshProUGUI _messageTextHolder;
    [SerializeField] private TextMeshProUGUI _characterNameTextHolder;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 0f;
    }

    public void AddChar(char symbol)
    {
        _messageTextHolder.text += symbol;
    }

    public void SetFullMessage(string text)
    {
        _messageTextHolder.text = text;
    }

    public void PlayVoice(AudioClip voice)
    {
        if(_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = voice;
        _audioSource.Play();
    }

    public void SetName(string name)
    {
        _characterNameTextHolder.text = name;
    }
    public void SetSprite(Sprite sprite)
    {
        _spriteHolder.sprite = sprite;
    }

    public void Clear()
    {
        if(_audioSource.isPlaying) _audioSource.Stop();
        SetName("");
        ClearMessage();
        SetSprite(null);
    }
    public void ClearMessage()
    {
        _messageTextHolder.text = "";
    }
}
