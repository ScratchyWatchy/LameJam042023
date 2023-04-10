using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float maxVelocityChange = 10f;
    [SerializeField] private float maxVelocityChangeInAir = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxCameraAngle = 90f;
    [SerializeField] private float minCameraAngle = -90f;
    [SerializeField] private float downForce = -9f;
    
    

    [SerializeField] private float cashJumpTime = 0.3f;

    [SerializeField] private float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    public bool isGrounded;
    private float cameraRotation = 0f;
    private float lastJumpInputTime = 0f;
    private Vector3 currentVelocity;
    private Vector3 targetVelocity;

    private bool addExtraForce = false;
    private Vector3 extraForce = new Vector3();
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;

        Debug.DrawLine(transform.position, transform.position + -Vector3.up * groundCheckDistance);
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hitInfo, groundCheckDistance))
        {
            isGrounded = true;
            if (Time.time - lastJumpInputTime < cashJumpTime)
            {
                //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        movement.y = 0f;

        if (movement.magnitude > 1f)
        {
            movement = movement.normalized;
        }


        Vector3 targetVelocity = movement * movementSpeed;
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - velocity;

        velocityChange.x = Mathf.Clamp(velocityChange.x, isGrounded ? -maxVelocityChange : -maxVelocityChangeInAir,
            isGrounded ? maxVelocityChange : maxVelocityChangeInAir);
        velocityChange.z = Mathf.Clamp(velocityChange.z, isGrounded ? -maxVelocityChange : -maxVelocityChangeInAir,
            isGrounded ? maxVelocityChange : maxVelocityChangeInAir);
        velocityChange.y = 0f;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        rb.AddForce(-Vector3.up * downForce);

        if (addExtraForce)
        {
            rb.AddForce(extraForce);
            addExtraForce = false;
        }



        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            lastJumpInputTime = Time.time;
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, minCameraAngle, maxCameraAngle);

        cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void AddExtraForce(Vector3 force)
    {
        addExtraForce = true;
        extraForce = force;
    }

    public void SetMaxVelocity(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

}
