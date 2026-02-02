using UnityEngine;

public class SnowCollect : MonoBehaviour
{
    [Header("Collection Settings")]
    [Tooltip("The name of the object that can collect this sphere (e.g., 'BlueCube')")]
    public string collectorName = "BlueCube";
    
    [Tooltip("The distance at which the sphere will be collected")]
    public float collectDistance = 2.0f;

    [Header("Pickup Visual Feedback")]
    [Tooltip("How high the object bobs up and down (in units)")]
    public float hoverAmplitude = 0.15f;
    
    [Tooltip("Speed of the hover oscillation")]
    public float hoverSpeed = 2f;
    
    [Tooltip("Speed of rotation in degrees per second")]
    public float rotationSpeed = 45f;
    
    private GameObject collector;
    private bool isCollected = false;
    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        collector = GameObject.Find(collectorName);
        
        if (collector == null)
        {
            Debug.LogWarning($"SnowCollect: Could not find object named '{collectorName}'. Make sure the name matches exactly.");
        }
    }

    void Update()
    {
        if (isCollected) return;

        // Hover effect: smooth up-and-down motion
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;
        transform.position = basePosition + Vector3.up * hoverOffset;

        // Slow constant rotation to signify collectible
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        if (collector != null)
        {
            float distance = Vector3.Distance(transform.position, collector.transform.position);
            if (distance <= collectDistance)
            {
                CollectSphere();
            }
        }
    }

    void CollectSphere()
    {
        isCollected = true;
        gameObject.SetActive(false);
        Debug.Log($"Sphere '{gameObject.name}' collected by '{collectorName}'!");
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectDistance);
    }
}
