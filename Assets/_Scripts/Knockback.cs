using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Vector3 knockbackDirection = new Vector3(1, 0, 0); 
    [SerializeField] private float knockbackForce = 10f;
    private Rigidbody rb;
    private CapsuleCollider _playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<CapsuleCollider>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
        }
    }

    private bool isRatDied = false;

    public void ApplyKnockback()
    {
        if (rb != null) StartCoroutine(ApplyKnockbackForces());
        
    }

    private IEnumerator ApplyKnockbackForces()
    {
        Vector3 verticalForce = new Vector3(0, knockbackForce/4, 0);
        Vector3 horizontalForce = new Vector3(knockbackDirection.x, 0, knockbackDirection.z).normalized * knockbackForce;

        _playerCollider.isTrigger = true;

        rb.AddForce(verticalForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.05f);
        rb.AddForce(horizontalForce, ForceMode.Impulse);

        _playerCollider.isTrigger = false;
    }
}