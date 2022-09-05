using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Camera_Switch : MonoBehaviour
{
    BoxCollider2D box2D;
    [SerializeField] GameObject camera_To_Deactivate;
    [SerializeField] GameObject camera_To_Activate;
    [SerializeField] RopeSetter ropeSetter;
    // Start is called before the first frame update
    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        box2D.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ropeSetter.playerCam = camera_To_Activate.GetComponent<Camera>(); 
            camera_To_Activate.SetActive(true);
            camera_To_Deactivate.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ropeSetter.playerCam = camera_To_Deactivate.GetComponent<Camera>();
            camera_To_Activate.SetActive(false);
            camera_To_Deactivate.SetActive(true);
        }
    }
}
