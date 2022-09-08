using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    private Idoor door;
    [SerializeField] bool isPlatformActivator = false;
    [SerializeField] private GameObject doorGameObject;
    [SerializeField] GameObject activeObject;
    [SerializeField] GameObject deactiveObject;
    // Start is called before the first frame update
    void Start()
    {
        if (!isPlatformActivator)
            door = doorGameObject.GetComponent<Idoor>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!isPlatformActivator)
        if (collision.tag.Equals("Grab") || collision.tag.Equals("Enemy"))
        {
            door.CloseDoor();
            Debug.Log("Door Closed");
        }

        if (isPlatformActivator)
        {
            if (collision.tag.Equals("Enemy"))
            {
                activeObject.SetActive(false);
                deactiveObject.SetActive(true);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPlatformActivator)
            if (collision.tag.Equals("Grab") || collision.tag.Equals("Enemy"))
        {
            door.OpenDoor();
            Debug.Log("Door Open");
        }

        if (isPlatformActivator)
        {
            if (collision.tag.Equals("Enemy"))
            {
                activeObject.SetActive(true);
                deactiveObject.SetActive(false);
            }
        }
    }
}
