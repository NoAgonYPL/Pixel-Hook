using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerLever : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    [SerializeField] Rigidbody2D rb2D;
    private Idoor door;
    //Tracks the time of the lever.
    [SerializeField] float timer;
    [Header("The speed the lever rotates back to turning itself off")]
    [SerializeField] float rotateBackSpeed;
    [SerializeField] Transform go_Back_To;
    [SerializeField] Vector2 dir;

    private void Awake()
    {
         rb2D = GetComponent<Rigidbody2D>();
         door = doorGameObject.GetComponent<Idoor>(); 
    }

    private void Update()
    {
       
        //if(timer > 0)
        //{
        //    timer -= Time.deltaTime;
        //}

        //if (timer <= 0)
        //{
        //    door.CloseDoor();
        //}

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lever_Trigger"))
        {
            door.CloseDoor();

            //rb2D.AddForce(Vector2.zero);
            Debug.Log("Door Closed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lever_Trigger"))
        {
            door.OpenDoor();
            //Rotate back the lever.
            rb2D.AddForce(dir* rotateBackSpeed);
            Debug.Log("Door Open");
        }
    }
}
