using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WheelLocomotion : MonoBehaviour
{
    [SerializeField] private float maxVelocityMagnitude;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform meshTransform;
    [SerializeField] private float maxRotateX;
    [SerializeField] private float maxRotateY;

    private Rigidbody rb;

    private float verticalInput, horizontalInput;
    public Vector3 LastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.left;
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        verticalInput = Input.GetKey(KeyCode.W) ? 1f : 0f;
        horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.x >= 0)
            rb.velocity = Vector3.left;

        rb.velocity += verticalInput * verticalSpeed * rb.velocity.normalized + horizontalInput * horizontalSpeed * Vector3.forward;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);

        LastVelocity = rb.velocity;

        //else
        //{
        //    Ray ray1 = new Ray(transform.position, Vector3.down);
        //    Ray ray2 = new Ray(transform.position + Vector3.left, Vector3.down);

        //    if (Physics.Raycast(ray1, out RaycastHit hit1, groundLayer))
        //    {
        //        if (Physics.Raycast(ray2, out RaycastHit hit2, groundLayer))
        //        {
        //            Vector3 direction = hit2.point - hit1.point;

        //            rb.velocity += horizontal * speed * direction.normalized;
        //        }
        //    }
        //}
    }

    private void Rotate()
    {
        meshTransform.eulerAngles = new Vector3(horizontalInput * maxRotateX, horizontalInput * maxRotateY, 0f);
    }

    public void Deceleration(float multiplier)
    {
        rb.velocity = LastVelocity * multiplier;
        if (rb.velocity.x >= 0)
            rb.velocity += Vector3.left;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            rb.velocity += jumpForce * Vector3.up;
        }
    }

    private bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}