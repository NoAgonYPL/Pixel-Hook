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

    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;

    [Header("For animation")]
    [SerializeField] private int resolution, waveCount, wobbleCount;
    [SerializeField] private float waveSize, animSpeed;

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

    //Fire a raycast that looks for something on the screen to hit.
    private void StartGrapple()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleMask);
        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;
            
            StartCoroutine(Grapple());
        }

        if(hit.collider == null)
        {
            line.enabled = true;
            line.positionCount = 2; 
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!isGrappling) return;
        {
            line.SetPosition(0, gunTip.position);
            line.SetPosition(0, target);
        }
    }

    //Makes sure that we grapple 
    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPosition;

        for(; t < time; t += grappleShotSpeed * Time.deltaTime)
        {
            newPosition = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPosition);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }
}
