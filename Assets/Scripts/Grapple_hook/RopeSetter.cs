using UnityEngine;
using Cinemachine;

public class RopeSetter : MonoBehaviour
{
    [SerializeField] Rope_Towards rope;
    [SerializeField] Grab_Rope rope_Grab;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    public bool playerCantGrapple = false;
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
        if (Input.GetButtonDown("Fire1") && !playerCantGrapple)
        {
            Vector3 worldPoint = playerCam.ScreenToWorldPoint(Input.mousePosition);
            playerCantGrapple = true;
            rope.SetStart(worldPoint);
            grapplingSF.Play();
        }

        if (Input.GetButtonDown("Jump"))
        {
            rope.DisableRope();
            grapplingSF.Stop();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 worldPoint = playerCam.ScreenToWorldPoint(Input.mousePosition);
            grabSF.Play();
            rope_Grab.SetStart(worldPoint);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            rope_Grab.targetIsGrabbed = false;
            grabSF.Stop();
            rope_Grab.DisableRope();
        }
    }
}
