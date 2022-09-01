using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open_Animation : MonoBehaviour, Idoor
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }

    public void ToggleDoor()
    {

    }
}
