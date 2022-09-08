using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    [SerializeField] int damage = 10;
    HealthManager healthManager;
    Rope_Towards rope_Towards;
    Grab_Rope grab_Rope;
    [SerializeField] bool instaKillOn;

    private void Awake()
    {
        healthManager = FindObjectOfType<HealthManager>().GetComponent<HealthManager>();
        grab_Rope = FindObjectOfType<Grab_Rope>().GetComponent<Grab_Rope>();
        rope_Towards = FindObjectOfType<Rope_Towards>().GetComponent<Rope_Towards>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Incase you want this object to be lava. 
        if (collision.tag.Equals("Player") && instaKillOn)
        {
            grab_Rope.DisableRope();
            rope_Towards.DisableRope();
            healthManager.isRespawning = false;
            healthManager.Respawn();
        }
        //Normal damage.
        else if (collision.tag.Equals("Player"))
        {
            grab_Rope.DisableRope();
            rope_Towards.DisableRope();
            healthManager.HurtPlayer(damage);
        }

        if(collision.tag.Equals("Grab") || collision.tag.Equals("Hook"))
        {
            grab_Rope.DisableRope();
            rope_Towards.DisableRope();
        }
        
    }
}
