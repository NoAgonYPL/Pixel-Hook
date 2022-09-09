using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Follow : MonoBehaviour
{
    //To know where the player is at all times. 
    [SerializeField] GameObject player;
    Transform playerPos;
    Vector2 currentPos;
    [SerializeField] float distance;
    [SerializeField] float movementSpeed;
    public bool movingBack = false;
    [SerializeField] string toCollideOn; 

    private void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }

    private void Update()
    {
        TrackingPlayer();
    }

    void TrackingPlayer()
    {
        //If enemy is in distance of the player. 
        if(Vector2.Distance(transform.position, playerPos.position) < distance && !movingBack)
        {
            //Move towards the player. 
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            //Become idle if it's to close. 
            if(Vector2.Distance(transform.position, currentPos) <= 0)
            {
                Debug.Log("I am not moving back");
                movingBack = false;
            }
            else
            {
                //Go back to starting position. 
                MoveBack();
                movingBack = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(toCollideOn) || collision.tag.Equals("Player") || collision.tag.Equals("Hook") || collision.tag.Equals("Grab"))
        {
            Debug.Log("We hit!");
            //We hit! 
            MoveBack();
            movingBack = true;
        }
    }

    public void MoveBack()
    {
        Debug.Log("I am moving back");
        // Move back towards starting location. 
        transform.position = Vector2.MoveTowards(transform.position, currentPos, movementSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, currentPos) <= 0 && movingBack) 
        {
            Debug.Log("I am no longer moving back");
            movingBack = false;
        }
    }

}
