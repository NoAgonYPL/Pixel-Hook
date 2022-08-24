using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 10f;
    public float jumpPower = 20f;
    public float cutJumpHeight = 0.5f;
    private bool isFacingRight = true;
    float jumpTimer;
    float jumpRemmeberTime = 0.2f;
    float groundedTimer;
    float groundedRemmeberTime = 0.2f;
    [SerializeField]float horizontalDamping = 0.5f;
    [SerializeField] float dampingWhenStopped = 0.2f;
    [SerializeField] float dampingWhenTurning = 0.1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jumping();
        Flip();
    }

    private void Jumping()
    {
        //Ground timer
        groundedTimer -= Time.deltaTime;
        if (IsGrounded())
        {
            groundedTimer = groundedRemmeberTime;
        }
        //Jump timer
        jumpTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = jumpRemmeberTime;
           
        }
        //Jump
        if(jumpTimer > 0 && (groundedTimer > 0))
        {
            groundedTimer = 0;
            jumpTimer = 0;
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
        //Declare the velocity the player will be moving in.
        horizontal = rb.velocity.x; 
        //Get the axis the player is moving in. 
        horizontal += Input.GetAxisRaw("Horizontal");

        //damping when stopped horizontal math
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
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
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);  
    }

    private void Flip() 
    //Flip the player to turn left or right
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
