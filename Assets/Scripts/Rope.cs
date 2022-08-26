using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer line;

    public Material mat;
    public Rigidbody2D origin;
    public float line_width = .1f;
    public float speed = 75;
    public float pull_force = 50;
    public float maxDistance = 20;
    private Vector3 velocity;
    public bool pull = false;
    public CircleCollider2D circleCollider;
    public int layerToGrab = 9;

    void Start()
    {
        circleCollider.enabled = false;
    }
    public void setStart(Vector2 targetPos)
    {
        line.enabled = true;
        //Direction the player will be pulled towards. 
        Vector2 dir = targetPos - origin.position;
        dir = dir.normalized;
        //Add speed to the velocity variebale.
        velocity = dir * speed;
        //This objects transform.position should be the player's position + the direction the rope is going in. 
        transform.position = origin.position + dir;
        circleCollider.enabled = true;
        pull = false;
    }
    // Update is called once per frame
    void Update()
    {
        //If the player hit an object with the grappling hook. 
        if (pull)
        {
            Vector2 dir = (Vector2)transform.position - origin.position;
            dir = dir.normalized;
            origin.AddForce(dir * pull_force);
        }
        else
        {
            //Fire the rope. 
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            //If the rope reaches max distance. 
            if(distance > maxDistance)
            {
                circleCollider.enabled = false;
                DisableRope();
                return;
            }
        }
  
        //Draw a line from the line and the player. 
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    //Disable the rope
    public void DisableRope()
    {
        pull = false;
        line.enabled = false;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        circleCollider.enabled = false;
    }

    //If we collide with an object. 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToGrab)
        {
            //Add force to the player.
            origin.AddForce(Vector3.zero);
            //Remove force from the rope. 
            velocity = Vector2.zero;
            pull = true;
        }
        //Add force to the player.
        origin.AddForce(Vector3.zero);
        //Remove force from the rope. 
        velocity = Vector2.zero;
    }
}
