using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple_Stick_To_Wall : MonoBehaviour
{
    public bool isAttachedToAnObject;
    public GameObject hookObject;

    private void Start()
    {
        hookObject = GameObject.Find("Grappling_Gun_Handler");
    }
    public void Update()
    {
        if (isAttachedToAnObject)
        {
            hookObject.transform.SetParent(transform);
        }
        else if(!isAttachedToAnObject)
        {
            hookObject.transform.SetParent(null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hook"))
       {
           isAttachedToAnObject = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hook"))
      {
            isAttachedToAnObject = false;
        }
    }
}