using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Down_Grapple : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] LayerMask grappleMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [Header("How long it takes for the grapple to hit the object")]
    [SerializeField]float grappleShotSpeed = 20f;
    [SerializeField] Transform gunTip;
    [SerializeField] GameObject grapplingGun;
    [SerializeField] float grapple_Range;
    [SerializeField] float grapplingGunRotationSpeed;


    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartGrapple();
        }

        //Moves the player to grapple point.
        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);


            if(Vector2.Distance(transform.position, target) < 0.5f)
            {
                    line.enabled = false;
                    retracting = false;
                    isGrappling = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Distance between mouse and gun variebale. 
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //Makes it so that the difference variebale is between 0 and 1. 
        difference.Normalize();

        //Calculates the rotation angle of our gun, makes it so that grappling gun points towards this angle. 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //A line that shows where the gun is pointing.
        Debug.DrawLine(transform.position, difference);

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
        RaycastHit2D hit = Physics2D.Raycast(gunTip.position, gunTip.right, maxDistance);
        //If it hits something
        if (hit && !isGrappling)
        {
            
            Debug.Log(hit.transform.name);
            //isGrappling = true;
            //target = hit.point;
            line.enabled = true;
            line.SetPosition(0, gunTip.position);
            line.SetPosition(1, hit.point);
            //StartCoroutine(Grapple());
        }
        //If it does not hit something it should fire the grapplinghook anyway and if hit's something now. 
        else
        {
            Debug.Log("I hit nothing");
            StartCoroutine(GrappleNothing());
           
        }
    }

    //Turn on grapple effect if we don't grapple anything.
    IEnumerator GrappleNothing()
    {
        line.enabled = true;
        line.SetPosition(0, gunTip.position);
        line.SetPosition(1, gunTip.position + gunTip.right * 10);

        yield return new WaitForSeconds(6);

        line.enabled = false;
        
    }

    //Makes sure that we grapple 
    //IEnumerator Grapple()
    //{
    //    float t = 0;
    //    float time = 10;
    //    line.SetPosition(0, transform.position);
    //    line.SetPosition(1, transform.position);

    //    Vector2 newPosition;

    //    for(; t < time; t += grappleShotSpeed * Time.deltaTime)
    //    {
    //        newPosition = Vector2.Lerp(transform.position, target, t / time);
    //        line.SetPosition(0, transform.position);
    //        line.SetPosition(1, newPosition);
    //        yield return null;
    //    }

    //    line.SetPosition(1, target);
    //    retracting = true;
    //}
}
