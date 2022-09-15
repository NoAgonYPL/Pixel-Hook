using UnityEngine;

public class Grab_Rope : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Ref to different components.")]
    [SerializeField] LineRenderer line;
    [SerializeField] Material mat;
    [SerializeField] Rigidbody2D origin;
    [SerializeField] GameObject hook;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] float miniOffSetDistance;
    [SerializeField] RopeSetter ropeSetter;
    [SerializeField] AudioSource hitSF;
    [SerializeField] AudioSource grapplingSF;
    Grapple_Stick_To_Wall grapple_Stick_To_Wall;
    [HideInInspector] public bool retracting;
    [SerializeField] Animation retractAnimation;
    float distance;

    [SerializeField] ParticleSystem hitEffect;

    //The object that will be grabbed.  
    private Rigidbody2D target;
    [HideInInspector] public bool targetIsGrabbed;

    [Header("The travel speed for the grappling hook.")]
    [SerializeField] float speed = 75;

    [Header("Grappling hooks dragging power on the grabbed object.")]
    [SerializeField] float grab_Force = 50;

    [Header("Max distance the hook can travel.")]
    [SerializeField] float maxDistance = 20;

    [Header("Layer that should be grabebale.")]
    [SerializeField] int layerToGrab = 9;

    Vector2 offSet;
    [SerializeField] float offSetDistance = 0.5f; 

    //The velocity of the hook.
    private Vector3 velocity;
    protected bool pull = false;

    void Start()
    {
        circleCollider.enabled = false;
        hook.SetActive(false);
        DisableRope();
        retracting = false;
    }
    public void SetStart(Vector2 targetPos)
    {
        line.enabled = true;
        //Direction the object will be pulled towards. 
        Vector2 dir = targetPos - origin.position;
        dir = dir.normalized;
        //Add speed to the velocity variebale.
        velocity = dir * speed;
        //This objects transform.position should be the player's position + the direction the rope is going in. 
        transform.position = origin.position + dir;
        retracting = false;
        //Enabling and disabling variebales:
        circleCollider.enabled = true;
        pull = false;
        hook.SetActive(true);

    }

    private void Update()
    {
        //Draw a line from this object and the player. 
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    void FixedUpdate()
    {
        
        //If the player hit an object with the grappling hook. 
        if (pull)
        {
            //Create a variebale that indicates the direction between the player and the grappling hook line.
            Vector2 dir = (Vector2)transform.position - origin.position;

            //Normalize the direction to smooth it.
            dir = dir.normalized;

            ropeSetter.canGrab = false;

            //Create a variebale that checks the distance between the grabbed object and the player's position.
            distance = Vector2.Distance(origin.position, target.position);

            //Pulls the object towards the player.
            if (target)
            {
                target.AddForce(-dir * grab_Force);
                transform.position = target.position;

                if(distance >= miniOffSetDistance)
                {
                    target.AddForce(Vector3.zero);
                    velocity = Vector3.zero;
                }
            }

        }
        else
        {

            if (!retracting)
            {
                transform.position += velocity * Time.deltaTime;
            }

            ropeSetter.canGrab = true;

            //Create a variebale that checks the distance between this object and the player's.
            distance = Vector2.Distance(transform.position, origin.position);

            //If the rope reaches max distance. 
            if (distance >= maxDistance)
            {
                Retracting();
                retracting = true;
                //retracting = true;
            }
        }

        if (retracting)
        {
            transform.position = Vector2.MoveTowards(transform.position, origin.position, speed * Time.deltaTime);

            if (distance <= 0)
            {
                hook.SetActive(false);
                DisableRope();
            }
        }
    }
    public void Retracting()
    {
        if (!retractAnimation.IsPlaying("Retract"))
        {
            //DisableRope();
        }
        else
        {
            retracting = true;
        }
        //Do something when the animation is complete
    }

    //Disable the rope
    public void DisableRope()
    {
        pull = false;
        line.enabled = false;
        //Reset the line's position
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        //Enabling and disabling variebales:
        retracting = false;

        circleCollider.enabled = false;
        hook.SetActive(false);
        ropeSetter.playerCantGrapple = false;

        if (grapple_Stick_To_Wall != null)
        {
            grapple_Stick_To_Wall.Attach(false);
        }
    }

    //If this object collide with an object. 
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == layerToGrab)
        {
            //Remove force from the player.
            origin.AddForce(Vector3.zero);

            //Remove force from the rope. 
            velocity = Vector2.zero;

            //Enabling and disabling variebales:
            pull = true;

            //Connects the target to the grappling hook. 
            target = collision.attachedRigidbody;

            //Make an offset, so that the objects don't enter each other and cause chaos. 
            if (target)
            {
                hitSF.Play();
                offSet = target.position - (Vector2)transform.position;
                offSet *= offSetDistance;
            }
        }

        //Play effect
        if (collision != null)
        {
            hitEffect.Play();
        }

        //Remove force from the player.
        origin.AddForce(Vector3.zero);
        //Remove force from the rope. 
        velocity = Vector2.zero;
    }
}
