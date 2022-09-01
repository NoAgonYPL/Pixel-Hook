using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour, Idoor 
{

   

    private bool doorIsOpen = false;

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }

    public void ToggleDoor()
    {
        doorIsOpen = !doorIsOpen;
        if (doorIsOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
}
