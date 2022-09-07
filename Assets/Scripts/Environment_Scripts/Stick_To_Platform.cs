using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick_To_Platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals("Hook"))
        {
            collision.transform.SetParent(transform);
        }  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Hook"))
        {
            collision.transform.SetParent(null);
        }
    }
}
