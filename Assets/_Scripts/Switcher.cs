using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToDisable;

    public void Switch()
    {
        _gameObjectToDisable.SetActive(false);
    }
}
