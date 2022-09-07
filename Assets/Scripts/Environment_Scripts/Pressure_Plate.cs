using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    private Idoor door;
    [SerializeField] private GameObject doorGameObject;
    // Start is called before the first frame update
    void Start()
    {
        door = doorGameObject.GetComponent<Idoor>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Grab"))
        {
            door.CloseDoor();
            Debug.Log("Door Closed");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Grab"))
        {
            door.OpenDoor();
            Debug.Log("Door Open");
        }
    }
}
