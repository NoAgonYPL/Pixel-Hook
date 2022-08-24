using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    private RaycastHit2D grappleObject;
    public LayerMask grappleLayer;

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
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }
        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
