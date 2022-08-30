using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{

    [SerializeField] float platformSpeed = 5; //Platform speed.
    [SerializeField] int startingPoint; //Starting point for the platform.
    [Header("Platform paths.")]
    [SerializeField] Transform[] points; //Points the platform will go towards.

    private int i; //Index of the array. 

    void Start()
    {
        //Setting the position of the platform to the starting position.
        transform.position = points[startingPoint].position; 
    }

    void Update()
    {
        //Checking the distance between the platform and the point. 
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;//Increase the index.

            //If the platform is on the last index of the array of platform points.
            if (i == points.Length)
            {
                i = 0; //resets the index. 
            }
            
        }

        //Moving the platform to the point position with the index i. 
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null); 
    }
}
