using System;
using TMPro;
using UnityEngine;

public class IntUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    public void SetValue(int value)
    {
        _label.text = $"HP:{value}";
    }
}   
