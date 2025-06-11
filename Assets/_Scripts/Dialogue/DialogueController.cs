using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DialogueController : MonoBehaviour
{
    [FormerlySerializedAs("_dialogueView")] [SerializeField] private ClickableDialogueView clickableDialogueView;
    [SerializeField] private SimpleDialogueView _simpleDialogueView; 
    [SerializeField] private GameObject _clickableDialogueHolder;
    [SerializeField] private GameObject _simpleDialogueHolder;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private UnityEvent _onDialogueEnd;
    private Coroutine _messageProcessing;
    public void StartDialogue(DialogueData[] dialogueData, bool isClickable = true)
    {
        if(isClickable)
        {
            StartCoroutine(ProcessClickableDialogue(dialogueData));
        }
        else
        {
            StartCoroutine(ProcessSimpleDialogue(dialogueData));
        }
    }

    private bool _isSkipping = false;
    private string _message;
    private IEnumerator ProcessSimpleDialogue(DialogueData[] dialogueData)
    {
        _simpleDialogueHolder.SetActive(true);
        
        foreach (var dialogue in dialogueData)
        {
            dialogue.OnMessageStart.Invoke();
            _simpleDialogueView.Clear();
            _simpleDialogueView.SetFullMessage(dialogue.Message);
            _simpleDialogueView.SetName(dialogue.Name);
            _simpleDialogueView.PlayVoice(dialogue.Voice);
            yield return new WaitForSeconds(dialogue.TimeToNextChar);
        }
        _simpleDialogueHolder.SetActive(false);
    }
    private IEnumerator ProcessClickableDialogue(DialogueData[] dialogueData)
    {
        _playerMovement.canMove = false;
        _clickableDialogueHolder.SetActive(true);
        foreach (var dialogue in dialogueData)
        {
            _messageProcessing = StartCoroutine(ClickableMessageProcessor(dialogue));
            yield return new WaitUntil(()=> _messageProcessing == null);
            yield return null;
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Mouse0));
            dialogue.OnMessageEnd.Invoke();
        }
        _clickableDialogueHolder.SetActive(false);
        _onDialogueEnd.Invoke();
        yield return null;
        yield return null;
        _playerMovement.canMove = true;
    }

    private IEnumerator ClickableMessageProcessor(DialogueData dialogue)
    {
        dialogue.OnMessageStart.Invoke();
        clickableDialogueView.Clear();
        if(dialogue.Image != null)
        clickableDialogueView.SetSprite(dialogue.Image);
        if(dialogue.Name.Length > 2)
        clickableDialogueView.SetName(dialogue.Name);
        if(dialogue.Voice != null)
        clickableDialogueView.PlayVoice(dialogue.Voice);
        _message = dialogue.Message;
        for (int i = 0; i < _message.Length; i++)
        {
            clickableDialogueView.AddChar(_message[i]);
            yield return new WaitForSeconds(dialogue.TimeToNextChar);
        }

        _messageProcessing = null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_messageProcessing != null)
            {
                StopCoroutine(_messageProcessing);
                _messageProcessing = null;
                clickableDialogueView.SetFullMessage(_message);
            }
        }
    }
}
