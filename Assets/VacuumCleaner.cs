using System.Collections;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    public GameObject smokeParticlesPrefab;  
    public Transform nozzle;  
    public Transform nozzleShootPoint;  
    public Vector3 boxSize = new Vector3(1f, 1f, 1f);  
    public float detectionDistance = 5f;  
    public AudioClip vacuumSound;  
    public AudioSource audioSource; 

    private ParticleSystem smokeParticles;
    private bool isVacuuming = false;

    private void Start()
    {
        audioSource.clip = vacuumSound;
    }

    void Update()
    {
        if (_playerMovement.canMove == false)
        {
            StopVacuuming();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isVacuuming = true;
            StartVacuuming();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isVacuuming = false;
            StopVacuuming();
        }
        _animator.SetBool("isVacuuming", isVacuuming);

        if (isVacuuming)
        {
            if (smokeParticles != null)
            {
                smokeParticles.transform.position = nozzle.position;
                smokeParticles.transform.rotation = nozzle.rotation;
            }
            Collider[] hitColliders = Physics.OverlapBox(nozzleShootPoint.position + nozzleShootPoint.forward * detectionDistance, boxSize / 2, nozzleShootPoint.rotation, LayerMask.GetMask("Rats"));

            foreach (Collider collider in hitColliders)
            {
                StartCoroutine(MoveRatToPoint(collider.gameObject));
            }
        }
    }

    void StartVacuuming()
    {
        if (smokeParticles == null)
        {
            GameObject particlesObject = Instantiate(smokeParticlesPrefab, nozzle.position, nozzle.rotation);
            smokeParticles = particlesObject.GetComponent<ParticleSystem>();
        }
        audioSource.Play();
        smokeParticles.Play();
    }

    public void StopVacuuming()
    {
        if (smokeParticles != null)
        {
            audioSource.Stop();
            smokeParticles.Stop();
        }
    }

    IEnumerator MoveRatToPoint(GameObject rat)
    {
        float moveSpeed = 5f; // Speed of moving the rat towards the point
        float scaleDownFactor = 0.8f; // Maximum scale factor
        Vector3 initialScale = rat.transform.localScale;

        if (rat == null) yield break;
    
        while (Vector3.Distance(rat.transform.position, nozzleShootPoint.position + (Vector3.forward * 3f)) > 0.25f)
        {
            if (rat == null) yield break;

            // Move the rat towards the nozzle shoot point
            rat.transform.position = Vector3.MoveTowards(rat.transform.position, nozzleShootPoint.position + (Vector3.forward * 3f), moveSpeed * Time.deltaTime);

            // Calculate the scale based on distance
            float distance = Vector3.Distance(rat.transform.position, nozzleShootPoint.position + (Vector3.forward * 3f));
            float scaleRatio = Mathf.Clamp01(distance / detectionDistance);
            rat.transform.localScale = Vector3.Lerp(initialScale, initialScale * scaleDownFactor, 1 - scaleRatio);

            // Rotate the rat towards the nozzle shoot point
            Quaternion targetRotation = Quaternion.LookRotation(nozzleShootPoint.position + (Vector3.forward * 2f) - rat.transform.position);
            rat.transform.rotation = Quaternion.Lerp(rat.transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
            yield return null; // Wait for the next frame
            if (rat == null) yield break;
        }

        Destroy(rat);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(nozzle.position, boxSize); // Draw the detection box for visualization
    }
}
