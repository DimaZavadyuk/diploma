using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;         // The player to follow
    public float rotationSpeed = 5f; // Speed of rotation (Lerp transition)
    public Transform Target;
    [SerializeField] private PlayerMovement _playerMovement; 
    void Update()
    {
        // Update the camera position to follow the player
        transform.position = player.transform.position;

        // If there is a target, rotate the camera to look at it
        if (Target != null)
        {
            _playerMovement.canMove = false;
            RotateTowardsTarget();
        }
    }

    void RotateTowardsTarget()
    {
        _playerMovement.canMove = false;
        Vector3 direction = Target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void Continue()
    {
        Target = null;
        _playerMovement.canMove = true;
    }
    
}