using UnityEngine;

public class Door_Open_Animation : MonoBehaviour, Idoor
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource openSF;
    [SerializeField] AudioSource closeSF;
    public bool alreadyPlaying = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        animator.SetBool("Open", true);
        if (!alreadyPlaying)
        {
            openSF.Play();
            alreadyPlaying = true;
        }
        
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
        if (alreadyPlaying)
        {
            closeSF.Play();
            alreadyPlaying = false;
        }
    }

    public void ToggleDoor()
    {

    }
}
