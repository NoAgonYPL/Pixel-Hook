using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple_To_Object_2 : MonoBehaviour
{
    public bool isAttachedToAnObject;
    public GameObject hookObject;
    public void Update()
    {
        if (isAttachedToAnObject)
        {
            hookObject.transform.SetParent(transform);
        }
        else if (!isAttachedToAnObject)
        {
            hookObject.transform.SetParent(null);
        }
    }
}
