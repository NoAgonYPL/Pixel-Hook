using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple_Stick_To_Wall : MonoBehaviour
{
    public bool isAttachedToAnObject;
    public GameObject hookObject;
    public int ropeLayer;

    public void Update()
    {
        if (isAttachedToAnObject)
        {
            hookObject.transform.SetParent(transform);
        }
        else
        {
            hookObject.transform.SetParent(null);
        }
    }
}
