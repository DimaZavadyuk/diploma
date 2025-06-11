using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 2f;
    public float rotationSpeed = 5f;
    public float wallDetectionDistance = 0.5f;
    public float sideRayAngle = 45f;
    public LayerMask wallLayer; // LayerMask for detecting walls
    public float fallThreshold = 10f; // Threshold for destruction

    private Rigidbody rb;
    private Vector3 movementDirection;
    private float changeDirectionTimer;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeDirection();
        initialPosition = transform.position;
    }

    void Update()
    {
        HandleDirectionChange();
        DetectWalls();
        CheckFallDistance();
    }

    void FixedUpdate()
    {
        MoveRat();
        RotateRat();
    }

    void MoveRat()
    {
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateRat()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void HandleDirectionChange()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        changeDirectionTimer = changeDirectionInterval;
    }

    void DetectWalls()
    {
        RaycastHit hit;
        Vector3[] directions = new Vector3[]
        {
            movementDirection,
            Quaternion.Euler(0, -sideRayAngle, 0) * movementDirection,
            Quaternion.Euler(0, sideRayAngle, 0) * movementDirection
        };

        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out hit, wallDetectionDistance, wallLayer))
            {
                // Calculate the new direction as the opposite of the wall's normal
                Vector3 wallNormal = hit.normal;
                movementDirection = Vector3.Reflect(movementDirection, wallNormal);

                // Apply a small random offset to the direction
                float randomAngle = Random.Range(-30f, 30f);
                movementDirection = Quaternion.Euler(0f, randomAngle, 0f) * movementDirection;
                return; // Exit after handling one wall detection
            }
        }
    }

    void CheckFallDistance()
    {
        if (initialPosition.y - transform.position.y > fallThreshold)
        {
            Destroy(gameObject); // Destroy the rat if it falls more than 10f
        }
    }

    // Draws the rays in the Scene view for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Draw forward ray
        Gizmos.DrawLine(transform.position, transform.position + movementDirection * wallDetectionDistance);

        // Draw left ray
        Vector3 leftDirection = Quaternion.Euler(0, -sideRayAngle, 0) * movementDirection;
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * wallDetectionDistance);

        // Draw right ray
        Vector3 rightDirection = Quaternion.Euler(0, sideRayAngle, 0) * movementDirection;
        Gizmos.DrawLine(transform.position, transform.position + rightDirection * wallDetectionDistance);
    }
}
