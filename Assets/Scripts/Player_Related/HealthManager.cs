using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //This script handles: Player health, taking damage, gaining health, respawning the player and invincibility. 

    public int maxHealth;
    public int currentHealth;

    //For disabling the player.
    [SerializeField] GameObject thePlayer;
    //For disabling the rope.
    [SerializeField] Rope_Towards rope_Towards;
    [SerializeField] Grab_Rope rope_Grab;

    //The invincibility flashing variebales:
    public float invincibilityLength = 2;
    private float invincibilityCounter;
    [SerializeField] SpriteRenderer playerRend;
    private float flashCounter;
    //How long the flashing will continue
    [SerializeField] float flashLength = 0.1f;

    [HideInInspector] public bool isRespawning;
    private Vector3 respawnPoint;
    //How long it takes to respawn.
    public float respawnLength;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    [SerializeField] float fadeSpeed;
    [SerializeField] float waitForFade;

    void Start()
    {
        currentHealth = maxHealth;

        //Set respawn point to where the player starts.
        respawnPoint = thePlayer.transform.position;
    }

    void Update()
    {
        //If invincibility is bigger then zero, 
        if(invincibilityCounter > 0)
        {
            
            invincibilityCounter -= Time.deltaTime;  //Timer goes down.
            flashCounter -= Time.deltaTime;  //Flash time goes down

            if (flashCounter <= 0)
            {
                //Here we reset the sprite renderer by turning it on and off. 
                playerRend.enabled = !playerRend.enabled;

                //Reset the flash counter.
                flashCounter = flashLength;
            }
            //here we make sure that the player modell won't be invisibale. 
            if(invincibilityCounter <= 0)
            {
                playerRend.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            //Fade the alpha color to black, a.k.a Alpha value 1 or 255. 
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            //Stop fading to black. 
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            //Fade the alpha color to transparent, a.k.a Alpha value 0. 
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            //Stop fading to white. 
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void HurtPlayer(int damage)
    {
        //If we are not invincible
        if(invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            rope_Towards.DisableRope();
            rope_Grab.DisableRope();

            //Call respawn function if health is 0 or less. 
            if(currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                //Reset the invinciblity counter.
                invincibilityCounter = invincibilityLength;

                playerRend.enabled = false;

                //Flash counter get's reset.
                flashCounter = flashLength;
            }
        }
    }
    public void HealPlayer(int healAmount)
    {
        //Heal the player.
        currentHealth += healAmount;

        //To make sure we get to much health.
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Respawn()
    {
        //If we are not allready respawning
        if (!isRespawning)
        {
            rope_Towards.DisableRope();  //Makes sure that we don't have a random rope staying behind if we would die to something that onehits. 
            rope_Grab.DisableRope(); //Makes sure that we don't have a random grabbed rope staying behind if we would die to something. 
            StartCoroutine(RespawnCO()); //Start the respawning corutine. 
        }
    }

    //Quicky on corutines: Starts at it's own time and works independent of other functions instead of going through each step in the progress. 
    //This can last for a long time and be done multiple times, at the same time.
    //This corutine respawns the player and plays some stuff under a specific time window. 
    public IEnumerator RespawnCO()
    {
        //Respawning is now true.
        isRespawning = true;

        //Disable the player.
        thePlayer.SetActive(false);
        //Death effect here:

        //Start fade out to black.
        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        //Start fade in.
        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;   //Makes sure it allways goes bright. 
        isFadeFromBlack = true;

        //Invincibility is back on for now/////////////
       
        invincibilityCounter = invincibilityLength;  //Reset the invinciblity counter.

        playerRend.enabled = false; //Disable the player renderer. 

        flashCounter = flashLength;   //Flash counter get's reset.
        //////////////////////////////////////////////

        thePlayer.SetActive(true); //Enable the player.

        isRespawning = false; //Respawning is now false;

        thePlayer.transform.position = respawnPoint; //Set the position of the player to the latest respawnpoint.

        currentHealth = maxHealth;  //Restore health:
    }

    //Set a new spawnpoint (Called in CheckPointController script).
    public void SetSpawnPoint(Vector3 respawnPos)
    {
        respawnPoint = respawnPos;
    }
}
