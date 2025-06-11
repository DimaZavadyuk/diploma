using UnityEngine;
using UnityEngine.Events;

public class HealArea : MonoBehaviour
{
    [SerializeField] private int _heal = 10;
    [SerializeField] private bool isOnce = true;
    [SerializeField] private UnityEvent _OnAreaHeal;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && isOnce)
        {
            isOnce = false;
            other.GetComponent<StatsData>().Hp += _heal;
            _OnAreaHeal.Invoke();
        }
    }
}