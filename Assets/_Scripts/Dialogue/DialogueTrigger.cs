using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private bool isClickable = true;
    [SerializeField] private bool isTriggerable = true;
    private bool _isTriggered = false;
    private DialogueController _dialogueController;

    private string _playerTag = "Player";

    private void Awake()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered) return;
        if (!isTriggerable) return;
        if(other.CompareTag(_playerTag)) _dialogueController.StartDialogue(_dialogues, isClickable);
        _isTriggered = true;
    }

    public void ForceStart()
    {
        _dialogueController.StartDialogue(_dialogues, isClickable);
    }
    
}
