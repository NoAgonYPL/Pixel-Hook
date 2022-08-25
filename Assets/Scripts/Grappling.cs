using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    public LayerMask grappleLayer;
    public float grappleShotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GrapplingHookActive();
    }

    public void GrapplingHookActive()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint2D.connectedAnchor = mousePos;
            distanceJoint2D.enabled = true;
            lineRenderer.enabled = true;

            float t = 0;
            float time = 10;
            Vector2 target = mousePos;


            if (Input.GetKeyDown(KeyCode.Mouse1) && distanceJoint2D.enabled)
            {
                for (; t < time; t += grappleShotSpeed * Time.deltaTime)
                {
                    mousePos = Vector2.Lerp(transform.position, target, t / time);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, mousePos);
                }
                if (Vector2.Distance(transform.position, target) < 0.5f)
                {
                    lineRenderer.enabled = false;
                    distanceJoint2D.enabled = false;
                    distanceJoint2D.enableCollision = false;
                }
            }
           
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && distanceJoint2D.enabled)
        {

        }
        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
       
    }
}
