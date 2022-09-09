using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI_Follow_Updated : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform player;
    Vector2 moveDirection;
    [SerializeField] float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        //If we have a target
        if (Vector2.Distance(transform.position, player.position) < distance && player)
        {
            //Where the enemy should go. 
            Vector2 direction = (player.position - transform.position).normalized;

            //Get the angle the enemy needs to go in to reach the player. 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //Handle the values: 
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        //If we have a target. 
        if (player)
        {
            //Move the enemy towards the player. 
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed * Time.deltaTime;
        }
    }
}
