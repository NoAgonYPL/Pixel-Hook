using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open_Animation : MonoBehaviour, Idoor
{
    [SerializeField] Animator animator;
    private bool isOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("Open", false);
    }

    public void ToggleDoor()
    {

    }
}
