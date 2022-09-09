using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Plate : MonoBehaviour
{
    private Idoor door;
    [SerializeField] bool isPlatformActivator = false;
    [SerializeField] private GameObject doorGameObject;
    [SerializeField] List <GameObject> activeObject = new List<GameObject>();
    [SerializeField] List <GameObject> deactiveObject= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (!isPlatformActivator)
            door = doorGameObject.GetComponent<Idoor>();

        if (isPlatformActivator)
        {
            for (int i = 0; i < activeObject.Count; i++)
            {
                activeObject[i].SetActive(false);
            }
            for (int i = 0; i < deactiveObject.Count; i++)
            {
                deactiveObject[i].SetActive(true);
            }
        }
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
                for (int i = 0; i < activeObject.Count; i++)
                {
                    activeObject[i].SetActive(false);
                }
                for (int i = 0; i < deactiveObject.Count; i++)
                {
                    deactiveObject[i].SetActive(true);
                }
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
                for (int i = 0; i < activeObject.Count; i++)
                {
                    activeObject[i].SetActive(true);
                }
                for (int i = 0; i < deactiveObject.Count; i++)
                {
                    deactiveObject[i].SetActive(false);
                }
            }
        }
    }
}
