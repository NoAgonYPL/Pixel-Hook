using UnityEngine;
using Cinemachine;

public class RopeSetter : MonoBehaviour
{
    [SerializeField] Rope_Towards rope;
    [SerializeField] Grab_Rope rope_Grab;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    [HideInInspector] public bool playerCantGrapple = false;
    [HideInInspector] public bool canGrab = false;
    public Camera playerCam;
    [SerializeField] AudioSource grapplingSF;
    [SerializeField] AudioSource grabSF;

    void Update()
    {
        GrapplingHook();
    }

    void GrapplingHook()
    {
        if (Input.GetButtonDown("Fire1") && !rope.retracting && playerCantGrapple)
        {
            Vector3 worldPoint = playerCam.ScreenToWorldPoint(Input.mousePosition);
            playerCantGrapple = true;
            rope.SetStart(worldPoint);
            grapplingSF.Play();
        }

        if (Input.GetButtonUp("Fire1") && !rope.retracting && !playerCantGrapple)
        {
            rope_Grab.targetIsGrabbed = false;
            grapplingSF.Stop();
            rope.DisableRope();
        }

        if (Input.GetButtonDown("Fire2") && canGrab)
        {
            Vector2 worldPoint = playerCam.ScreenToWorldPoint(Input.mousePosition);
            canGrab = true;
            grabSF.Play();
            rope_Grab.SetStart(worldPoint);
        }

        if (Input.GetButtonUp("Fire2") && !rope_Grab.retracting && !canGrab)
        {
            rope_Grab.targetIsGrabbed = false;
            grabSF.Stop();
            rope_Grab.DisableRope();
        }
    }
}
