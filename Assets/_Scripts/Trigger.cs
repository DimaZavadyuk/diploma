using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnTrigger; 
    public void Trigged()
    {
        OnTrigger.Invoke();
    }
}
