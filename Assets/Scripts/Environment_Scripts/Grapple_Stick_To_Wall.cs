using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple_Stick_To_Wall : MonoBehaviour
{
    public GameObject hookObject;

    private void Start()
    {
        hookObject = GameObject.Find("Grappling_Gun_Handler");
    }

    public void Attach(bool value) => hookObject.transform.SetParent(value ? transform : null);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hook"))
        {
            Attach(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hook"))
        {
            Attach(false);
        }
    }
}