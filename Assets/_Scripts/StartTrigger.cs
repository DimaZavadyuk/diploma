using UnityEngine;
using UnityEngine.Events;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnStart;
    void Start()
    {
        _OnStart.Invoke();
    }
}
