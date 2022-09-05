using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed = 10f;
    public float jumpPower = 20f;
    public float cutJumpHeight = 0.5f;
    float jumpTimer;
    [SerializeField]float jump_Remmeber_Time = 0.2f;
    float groundedTimer;
    [SerializeField] float grounded_Remmeber_Time = 0.2f;
    [SerializeField] float horizontalDamping = 0.5f;
    [SerializeField] float dampingWhenStopped = 0.2f;
    [SerializeField] float dampingWhenTurning = 0.1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Rope_Towards rope;
    public bool walkingOnGrapplingHook;

    // Update is called once per frame
    void Update()
    {
        Jumping();
    }

    private void Jumping()
    {
        //Ground timer
        groundedTimer -= Time.deltaTime;
        if (IsGrounded())
        {
            groundedTimer = grounded_Remmeber_Time;
        }
        //Jump timer
        jumpTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = jump_Remmeber_Time;
        }
        //Jump
        if(jumpTimer > 0 && (groundedTimer > 0))
        {
            groundedTimer = 0;
            jumpTimer = 0;
            //Create a rigidbody velocity that will make the player jump. 
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        //Hold jump to jump higher.
        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerWalking();
    }

    void PlayerWalking()
    {
        //Declare up and down velocity the player will move in when they grapple.
        vertical = rb.velocity.y;
        //Declare the left and right velocity the player will be moving in.
        horizontal = rb.velocity.x;

        //Get the axis the player is moving in.
        horizontal += Input.GetAxisRaw("Horizontal");

        if (rope.pull)
        {
            vertical += Input.GetAxisRaw("Vertical");
            //damping when stopped vertical math
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.01f)
            {
                walkingOnGrapplingHook = true;
                vertical *= Mathf.Pow(1f - dampingWhenStopped, Time.deltaTime * speed);
            }
            //standard damping
            else
            {
                //Multiply the vertical axis with a damping power to get proper acceleration.
                vertical *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * speed);
            }
            //Move the player in the horizontal axis if they use the vertical keybindings.
            rb.velocity = new Vector2(rb.velocity.x, vertical);
        }

        //damping when stopped horizontal math
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
        {
            horizontal *= Mathf.Pow(1f - dampingWhenStopped, Time.deltaTime * speed);
        }
        //damping when turning
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontal))
        {
            horizontal *= Mathf.Pow(1f - dampingWhenTurning, Time.deltaTime * speed);
        }
        //standard damping
        else
        {
            //Multiply the horizontal axis with a damping power to get proper acceleration.
            horizontal *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * speed);
        }
        //Move the player in the horizontal axis if they use the horizontal keybindings.
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        //Return an Collision circle at the groundchecks position and check the groundlayer mask for detection. 
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayerMask);
    }
}



