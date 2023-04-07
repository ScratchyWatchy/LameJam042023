using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HorizontalDrag : MonoBehaviour
{
    [SerializeField] private float drag = 1f;
    [SerializeField] private float verticalDrag = 1f;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0f; // Set the y-component of velocity to 0 to ignore vertical velocity
        velocity -= velocity.normalized * drag * Time.fixedDeltaTime;
        
        
        Vector3 velocityY = rb.velocity;
        velocityY -= velocityY.normalized * verticalDrag * Time.fixedDeltaTime;

        
        
        rb.velocity = new Vector3(velocity.x, velocityY.y, velocity.z);
    }
}