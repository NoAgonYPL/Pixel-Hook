using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Register : MonoBehaviour
{
    private void OnEnable()
    {
        //Register the camera and feed in the component we want to register.
        CameraSwitcher.Register(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        //Unregister the camera and feed in the component we want to register.
        CameraSwitcher.Unregister(GetComponent<CinemachineVirtualCamera>());
    }
}
