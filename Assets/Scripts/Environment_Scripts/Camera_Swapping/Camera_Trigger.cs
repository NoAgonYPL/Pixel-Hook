using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    
    [SerializeField] Transform _cameraPosition;
    BoxCollider2D box2D;
    [SerializeField] float cameraSize = 6.922986f;
    // Start is called before the first frame update
    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        box2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Swaps the camera's position to this object. 
            Camera.main.transform.SetPositionAndRotation(_cameraPosition.transform.position, transform.rotation);
            Camera.main.orthographicSize = cameraSize;
        }
    }

   
}
