using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;

    public Rigidbody rb;

    public Vector3 LastVelocityXZ;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = horizontal * speed * Vector3.forward + vertical * speed * Vector3.left + rb.velocity.y * Vector3.up;
        LastVelocityXZ = rb.velocity;
        LastVelocityXZ.y = 0;
    }

    private void Rotate()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;
        
        if (direction.magnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
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