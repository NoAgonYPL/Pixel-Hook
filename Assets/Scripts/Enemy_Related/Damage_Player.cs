using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] HealthManager healthManager;
    [SerializeField] bool instaKillOn;
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
