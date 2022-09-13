using UnityEngine;
using Cinemachine;

public class RopeSetter : MonoBehaviour
{
    [SerializeField] Rope_Towards rope;
    [SerializeField] Grab_Rope rope_Grab;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    [HideInInspector] public bool playerCantGrapple;
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
            rope.SetStart(worldPoint);
            grapplingSF.Play();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            rope.DisableRope();
            grapplingSF.Stop();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToGrab)
        {
            playerCantGrapple = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCantGrapple = false;
    }
}
