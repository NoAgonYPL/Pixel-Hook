using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Camera_Trigger_Volume : MonoBehaviour
{
    [Header("The camera that this trigger handles.")]
    //Which camera should we be on.
    [SerializeField]private CinemachineVirtualCamera cam;
    //How big the box should be
    [Header("Size of the camera trigger.")]
    [SerializeField] private Vector2 boxSize;

    BoxCollider2D box;
    Rigidbody2D rb;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        box.isTrigger = true;
        box.size = boxSize;

        rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        //Draw a cube that is the same size as the camera collision box.
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(CameraSwitcher.ActiveCamera != cam)
            {
                CameraSwitcher.SwitchCamera(cam);
            }
        }
    }
}
