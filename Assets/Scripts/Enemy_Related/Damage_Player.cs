using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    [SerializeField] int damage = 10;
    HealthManager healthManager;
    [SerializeField] bool instaKillOn;

    private void Awake()
    {
        healthManager = FindObjectOfType<HealthManager>().GetComponent<HealthManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Incase you want this object to be lava. 
        if (collision.tag.Equals("Player") && instaKillOn)
        {
            healthManager.isRespawning = false;
            healthManager.Respawn();
        }
        //Normal damage.
        else if (collision.tag.Equals("Player"))
        {
            healthManager.HurtPlayer(damage);
        }
        
    }
}
