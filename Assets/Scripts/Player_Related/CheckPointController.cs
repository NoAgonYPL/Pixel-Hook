using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [Header("References for all the variebales for the checkpoint")]
    [SerializeField] SpriteRenderer theRend;
    [SerializeField] Material cpOff;
    [SerializeField] Material cpOn;
    [SerializeField] HealthManager theHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        theHealthManager = FindObjectOfType<HealthManager>();
    }

    public void CheckPointOn()
    {
        //We are creating a list of checkpoints and search for all checkpoints in the scene. 
        CheckPointController[] checkPoints = FindObjectsOfType<CheckPointController>();

        //Look through all of the checkpoints in the list and turn them off. 
        foreach (CheckPointController cp in checkPoints)
        {
            cp.CheckPointOff();
        }

        //Turn on this checkpoint material.
        theRend.material = cpOn;

        
    }

    public void CheckPointOff()
    {
        theRend.material = cpOff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player") || collision.tag.Equals ("Hook"))
        {
            theHealthManager.SetSpawnPoint(transform.position);
            CheckPointOn();
        }
    }
}
