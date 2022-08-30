using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    public Rope rope;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    public bool playerCantGrapple;
    public bool canGrab = false;
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
            rope.setStart(worldPoint);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            rope.DisableRope();
        }

        if (Input.GetButtonDown("Jump"))
        {
            rope.DisableRope();
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
