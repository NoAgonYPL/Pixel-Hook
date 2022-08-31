using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerLever : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private Idoor door;
    //Tracks the time of the lever.
    [SerializeField] float timer;

    private void Awake()
    {
        door = doorGameObject.GetComponent<Idoor>(); 
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            door.CloseDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lever_Trigger"))
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Lever_Trigger"))
        {
            timer = 1f;
        }
    }
}
