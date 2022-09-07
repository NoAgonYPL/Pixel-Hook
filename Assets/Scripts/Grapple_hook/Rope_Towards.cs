using UnityEngine;

public class Rope_Towards : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Ref to different components.")]
    [SerializeField] LineRenderer line;
    [SerializeField] Material mat;
    [SerializeField] Rigidbody2D origin;
    [SerializeField] GameObject hook;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] RopeSetter ropeSetter;
    Grapple_Stick_To_Wall grapple_Stick_To_Wall;

    [Header("The travel speed for the grappling hook.")]
    [SerializeField] float speed = 75;

    [Header("Grappling hooks dragging power on the player.")]
    [SerializeField] float pull_force = 50;
    [SerializeField] float drag_Back_Force = 7f;

    [Header("Stopping force for the hook.")]
    [SerializeField] float close_By_Force = 1;

    [Header("Max distance the hook can travel.")]
    [SerializeField] float maxDistance = 20;
    [SerializeField] float miniHookDistance;

    [Header("Max distance the player can be from hook impact and itself")]
    [SerializeField] float maxDistanceAwayFromHook = 12;

    [Header("Layer that should be grabebale.")]
    [SerializeField] int layerToGrab = 9;

    //The velocity of the hook.
    private Vector3 velocity;
    [HideInInspector] public bool pull = false;

    void Start()
    {
        circleCollider.enabled = false;
        hook.SetActive(false);
    }
    public void SetStart(Vector2 targetPos)
    {
            line.enabled = true;
            //Direction the player will be pulled towards. 
            Vector2 dir = targetPos - origin.position;
            dir = dir.normalized;
            //Add speed to the velocity variebale.
            velocity = dir * speed;
            //This objects transform.position should be the player's position + the direction the rope is going in. 
            transform.position = origin.position + dir;

            //Enabling and disabling variebales:
            circleCollider.enabled = true;
            pull = false;
            hook.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        //If the player hit an object with the grappling hook. 
        if (pull)
        {
            //Create a variebale that indicates the direction between the player and the grappling hook line.
            Vector2 dir = (Vector2)transform.position - origin.position;

            //Normalize the direction to smooth it.
            dir = dir.normalized;

            if (grapple_Stick_To_Wall != null)
            {
                grapple_Stick_To_Wall.isAttachedToAnObject = true;
            }

            //Create a variebale that checks the distance between this object and the player's.
            float distance = Vector2.Distance(transform.position, origin.position);
            if (!Input.GetButton("Vertical"))
            {
                origin.AddForce(dir * pull_force);
            }
  
            //If the player would be going to far on the hook. 
            if (distance >= maxDistanceAwayFromHook)
            {
                //Remove force from the player.
                origin.AddForce(dir * drag_Back_Force);
                Debug.Log("We are being to far away.");
            }
        }
        else
        {
            //Fire the rope. 
            transform.position += velocity * Time.deltaTime;

            //Create a variebale that checks the distance between this object and the player's.
            float distance = Vector2.Distance(transform.position, origin.position);

            //If the rope reaches max distance. 
            if(distance >= maxDistance)
            {
                DisableRope();
                return;
            }
        }
        //Draw a line from this object and the player. 
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
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
        circleCollider.enabled = false;
        hook.SetActive(false);
        ropeSetter.playerCantGrapple = false;
        origin.AddForce(Vector2.zero);
        velocity = Vector2.zero;
        if(grapple_Stick_To_Wall != null)
        {
            grapple_Stick_To_Wall.isAttachedToAnObject = false;
        }
        
    }

    //If this object collide with an object. 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToGrab)
        {
            //Remove force from the player.
            //origin.AddForce(Vector3.zero);

            //Remove force from the rope. 
            velocity = Vector2.zero;

            //Enabling and disabling variebales:
            pull = true;

            //Get ref from hit target.
            grapple_Stick_To_Wall = collision.GetComponent<Grapple_Stick_To_Wall>();

            //Incase there is no grapple_Stick_to_wall script attached to the object we are hitting, return null. 
            if (grapple_Stick_To_Wall == null)
            {
                return;
            }
            else if (grapple_Stick_To_Wall != null)
            {
                //Attach to moving platform.
                grapple_Stick_To_Wall.isAttachedToAnObject = true;
            }
        }
        //Remove force from the rope. 
        velocity = Vector2.zero;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToGrab)
        {
            //Incase there is no grapple_Stick_to_wall script attached to the object we are hitting, return null. 
            if (grapple_Stick_To_Wall == null)
            {
                return;
            }
            else if (grapple_Stick_To_Wall != null)
            {
                //Get ref from hit target.
                grapple_Stick_To_Wall = collision.GetComponent<Grapple_Stick_To_Wall>();
                //Detach to moving platform.
                grapple_Stick_To_Wall.isAttachedToAnObject = false;
            }
        }
    }
}
