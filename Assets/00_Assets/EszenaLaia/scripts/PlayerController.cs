using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded = true;
    bool jump = false;
    public float speed = 10.0f;
    public float jumpHeight = 2.0f;
    public float maxVelocityChange = 10.0f;
    float gravity = 10.0f;

    public Rigidbody rigidbody;

    //CheckSphere parameters
    [SerializeField]
    private Transform playerFeet;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private LayerMask groundLayer;

    private void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }*/
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(playerFeet.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            //Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            targetVelocity = transform.TransformDirection(targetVelocity);
            //targetVelocity = targetVelocity * speed;
            targetVelocity *= speed;

            //Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.linearVelocity;
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            //Jump
            if (jump)
            {
                rigidbody.linearVelocity = new Vector3(velocity.x, CalculateJumpSpeed(), velocity.z);
            }
        }

        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        jump = false;
        isGrounded = false;
    }

    float CalculateJumpSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerFeet.position, groundCheckRadius);
    }
}
