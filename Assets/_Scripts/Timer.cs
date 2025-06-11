using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float tickCooldown;
    [SerializeField] private UnityEvent _OnTick;
    private float lastTimeTick = 0f;
    private void Update()
    {
        if (lastTimeTick + tickCooldown <= Time.time)
        {
            lastTimeTick = Time.time;
            _OnTick.Invoke();
        }
    }
}
