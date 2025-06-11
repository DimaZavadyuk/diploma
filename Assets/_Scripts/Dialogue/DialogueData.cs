using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueData
{
    public string Name;
    public Sprite Image;
    public string Message;
    public float TimeToNextChar = 0.1f;
    public UnityEvent OnMessageStart;
    public UnityEvent OnMessageEnd;
    public AudioClip Voice;
}
