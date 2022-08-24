using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Camera mainCamera;
    private Vector2 grapplepoint;
    [SerializeField]LayerMask whatIsGrappleable;
    public Transform gunTip, player;
    public float maxDistance = 10f;
    private SpringJoint playerJoint;

    public float springForce;
    public float springDamping;
    public float springMassScale;

    // Start is called before the first frame update


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
           StopGrapple();
        }
    }

    //Call when we want to start grappling.
    void StartGrapple()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(gunTip.position, gunTip.right);
        Debug.Log(hitInfo.collider.name);

        if(hitInfo)
        {
            playerJoint = player.gameObject.AddComponent<SpringJoint>();
            grapplepoint = hitInfo.point;
            playerJoint.autoConfigureConnectedAnchor = false;
            playerJoint.connectedAnchor = grapplepoint;

            float distanceFromPoint = Vector2.Distance(player.position, grapplepoint);

            //The distance grapple will try to keep from grapple point. 
            playerJoint.maxDistance = distanceFromPoint * 0.8f;
            playerJoint.minDistance = distanceFromPoint * 0.25f;

            playerJoint.spring = springForce;
            playerJoint.damper = springDamping;
            playerJoint.massScale = springMassScale;

            lineRenderer.positionCount = 2;
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!playerJoint) return;

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(0, grapplepoint);
    }

    //Call when we want to stop grappling
    void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(playerJoint);
    }
}
