using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{

    [SerializeField] private GameObject doorGameObjectA;
    [SerializeField] bool doorActiveByButtonPress;

    private Idoor doorA;
    private void Awake()
    {
        doorA = doorGameObjectA.GetComponent<Idoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && doorActiveByButtonPress)
        {
            doorA.OpenDoor();
        }
    }
}
