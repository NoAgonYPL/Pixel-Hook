using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Down_Grapple : MonoBehaviour
{
    [Header("Ref for the line and distancejoint")]
    LineRenderer line;
    SpringJoint2D rope;

    [Header("Layer mask refs")]
    [SerializeField] LayerMask grappleMask;
    [SerializeField] private int grappableLayerNumber;

    [Header("Variebale's for the grappling hook.")]
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grapple_Range;
    [SerializeField] float grappleSpeed = 10f;

    [Header("The player transform")]
    [SerializeField] Transform player;

    [Header("Grappling gun positions")]
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject grapplingGun;

    [Header("Grapple gun rotation speed.")]
    [SerializeField] float grapplingGunRotationSpeed;

    bool isGrappling = false;
    //[HideInInspector] public bool retracting = false;

    Vector2 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        rope = GetComponent<SpringJoint2D>();
        rope.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartGrapple();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisableRope();
        }

        //Moves the player to grapple point.
        //if (retracting)
        //{
        //    Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

        //    transform.position = grapplePos;

        //    line.SetPosition(0, transform.position);

        //    if(Vector2.Distance(transform.position, target) < 0.5f)
        //    {
        //        DisableRope();
        //        isGrappling = false;
        //        retracting = false;
        //    }
        //}
    }

    private void FixedUpdate()
    {
        //Distance between mouse and gun variebale. 
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Debug.DrawLine(transform.position, lookDirection);

        //Makes it so that the difference variebale is between 0 and 1. 
        difference.Normalize();

        //Calculates the rotation angle of our gun, makes it so that grappling gun points towards this angle. 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //Rotate the gun around the player. 
        grapplingGun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        
        if(rotZ < -90 || rotZ > 90)
        {
            //If the grappling gun is facing to the left side of the character. Keep it pointing at the mouse essentially. 
             if (grapplingGun.transform.eulerAngles.y == 180)
             {
                //Flip the localrotation of the grappling gun so that it's not upside down. 
                transform.localRotation = Quaternion.Euler(180, 180, -rotZ);
             }
        }


    }

    //Fire a raycast that looks for something on the screen to hit.
    private void StartGrapple()
    {
        //Fire a raycast at the tip of the gun, forward and it has a max distance. 
        RaycastHit2D hit = Physics2D.Raycast(player.position, gunTip.right, maxDistance);
        //Fire a line when the raycast is cast.
        line.enabled = true;
        line.SetPosition(0, gunTip.position);
        line.SetPosition(1, gunTip.position + gunTip.right * maxDistance);
        //If it hits something
        if (hit.transform.gameObject.layer == grappableLayerNumber && !isGrappling)
        {
            if(Vector2.Distance(hit.point, gunTip.position) <= maxDistance)
            {
                Debug.Log(hit.transform.name);
                Grapple(hit);
            }

        }
    }
    void Grapple(RaycastHit2D hit)
    {
        line.SetPosition(0, gunTip.position);
        line.SetPosition(1, gunTip.position * hit.point);
        rope.connectedAnchor = hit.point;
        //grappleRope.DrawRopeNoWaves();
        rope.enabled = true;

        if (Input.GetMouseButtonDown(1))
        {
            GrappleTowardsPoint(hit);
        }
    }
    public void DisableRope()
    {
        rope.enabled = false;
        line.enabled = false;
        line.SetPosition(0, transform.position);
    }

    //Makes sure that we grapple 
    void GrappleTowardsPoint(RaycastHit2D hit)
    {
        Vector2 grapplePos = Vector2.Lerp(player.transform.position, hit.point, grappleSpeed * Time.deltaTime);

        DisableRope();

        if (Vector2.Distance(grapplePos, hit.point) < 0.5f)
        {
            
            isGrappling = false;
            //retracting = false;
        }

    }
}
