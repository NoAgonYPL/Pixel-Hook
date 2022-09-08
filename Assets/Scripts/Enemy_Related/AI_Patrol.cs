using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : MonoBehaviour
{
    [SerializeField] float patrolSpeed;

    [HideInInspector]
    public bool mustPatrol;

    [HideInInspector]
    public bool mustFlip;

    [SerializeField] Rigidbody2D rb2D;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] BoxCollider2D box2D;



    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }

    public void Patrol()
    {
        if ( box2D.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        //Move the Patrolling enemy
        rb2D.velocity = new Vector2(patrolSpeed * Time.fixedDeltaTime, rb2D.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        //Flip the patrolling enemy
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        //Flips the movement to change the speed.
        patrolSpeed *= -1;
        mustPatrol = true;
    }
}
