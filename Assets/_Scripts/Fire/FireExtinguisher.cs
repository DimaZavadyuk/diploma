using System.Collections;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    public GameObject smokeParticlesPrefab;  // Prefab for smoke particles
    public Transform nozzle;  // Position where particles are emitted
    public Transform nozzleShootPoint;  // Position where particles are emitted
    public float extinguishRate = 1f;  // Time it takes to extinguish a fire
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);  // Size of the detection box
    public float detectionDistance = 5f;  // Distance of the box from the player
    public AudioClip extinguishSound;  // Sound to play when extinguishing
    public AudioSource audioSource;  // AudioSource to play the sound

    private ParticleSystem smokeParticles;
    private bool isExtinguishing = false;

    void Start()
    {
        audioSource.clip = extinguishSound;

        if (smokeParticlesPrefab != null)
        {
            smokeParticles = Instantiate(smokeParticlesPrefab, nozzle.position, nozzle.rotation).GetComponent<ParticleSystem>();
            smokeParticles.Stop();  // Start with particles stopped
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (_playerMovement.canMove == false)
        {
            _animator.SetBool("isPushing", false);
            audioSource.Stop();
            smokeParticles.Stop();  // Stop particles
            isExtinguishing = false;
            return;
        }
        if (Input.GetMouseButton(0))  // Left mouse button is pressed
        {
            if (!isExtinguishing)
            {
                _animator.SetBool("isPushing", true);
                smokeParticles.Play();  // Start particles
                audioSource.Play();
                isExtinguishing = true;
            }
        }
        else
        {
            if (isExtinguishing)
            {
                _animator.SetBool("isPushing", false);
                audioSource.Stop();
                smokeParticles.Stop();  // Stop particles
                isExtinguishing = false;
            }
        }

        if (isExtinguishing)
        {
            // Update particle position
            if (smokeParticles != null)
            {
                smokeParticles.transform.position = nozzle.position;
                smokeParticles.transform.rotation = nozzle.rotation;
            }

            // Perform box check
            Collider[] hitColliders = Physics.OverlapBox(nozzleShootPoint.position + nozzleShootPoint.forward * detectionDistance, boxSize / 2, nozzleShootPoint.rotation);
            foreach (var collider in hitColliders)
            {
                FireCell fireCell = collider.GetComponent<FireCell>();
                if (fireCell != null && fireCell.IsBurning())
                {
                    StartCoroutine(ExtinguishFire(fireCell));
                }
            }
        }
    }

    IEnumerator ExtinguishFire(FireCell fireCell)
    {
        // Gradually extinguish the fire
        float elapsedTime = 0f;
        while (elapsedTime < extinguishRate)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fireCell.Extinguish();  // Call the method to extinguish the fire
    }

    void OnDrawGizmos()
    {
        if (nozzleShootPoint == null) return;

        Gizmos.color = Color.blue;
        Gizmos.matrix = Matrix4x4.TRS(nozzleShootPoint.position + nozzleShootPoint.forward * detectionDistance, nozzleShootPoint.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
    public void Stop()
    {
        if (smokeParticles == null) return;
        if (audioSource == null) return;
        smokeParticles.Stop();
        audioSource.Stop();
        isExtinguishing = false;
    }
}
