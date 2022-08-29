using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    public Rope rope;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player;
    [SerializeField] int layerToGrab;
    public bool playerCantGrapple;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playerCantGrapple)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rope.setStart(worldPos);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            rope.DisableRope();
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
