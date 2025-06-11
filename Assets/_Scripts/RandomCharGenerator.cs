using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCharGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    private void Awake()
    {
        _textMeshPro.text = RandomChar();
    }

    public string RandomChar()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return Convert.ToString(chars[Random.Range(0, chars.Length)]);
    }
}
