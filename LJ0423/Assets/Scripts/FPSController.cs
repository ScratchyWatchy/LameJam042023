using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxCameraAngle = 90f;
    [SerializeField] private float minCameraAngle = -90f;
    
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float deceleration = 0.2f;
    
    [SerializeField] private float cashJumpTime = 0.3f;
    
    [SerializeField] private LayerMask groundLayers;

    private Rigidbody rb;
    public bool isGrounded;
    private float cameraRotation = 0f;
    private float lastJumpInputTime = 0f;
    private Vector3 currentVelocity;
    private Vector3 targetVelocity;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Target velocity based on input and max speed
        targetVelocity = new Vector3(horizontal, 0f, vertical) * movementSpeed;

        // Smoothly accelerate or decelerate towards target velocity
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, targetVelocity.magnitude > currentVelocity.magnitude ? acceleration : deceleration);

        // Move the rigidbody with the current velocity
        rb.MovePosition(transform.position + transform.TransformDirection(currentVelocity * Time.fixedDeltaTime));


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    private void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & groundLayers) != 0)
        {
            isGrounded = true;
            if (Time.time - lastJumpInputTime < cashJumpTime)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                lastJumpInputTime = 0f;
            }
        }
    }
}
