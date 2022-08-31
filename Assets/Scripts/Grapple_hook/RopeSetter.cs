using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    [SerializeField] Rope_Towards rope;
    [SerializeField] Grab_Rope rope_Grab;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    [HideInInspector] public bool playerCantGrapple;
    [HideInInspector] public bool canGrab = false;
    void Update()
    {
        GrapplingHook();
    }

    void GrapplingHook()
    {
        if (Input.GetButtonDown("Fire1") && !playerCantGrapple)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(player.transform.position, worldPoint, Color.yellow);
            rope.SetStart(worldPoint);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            rope.DisableRope();
        }

        if (Input.GetButtonDown("Jump"))
        {
            rope.DisableRope();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            rope_Grab.SetStart(worldPoint);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            rope_Grab.targetIsGrabbed = false;
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
