using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAnimation : MonoBehaviour
{
    private Animator _doorAnimationController;

    [SerializeField] private bool openDoorToTheLeft = false;
    [SerializeField] private bool doorIsOpen = false;

    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private static readonly int TurnsLeft = Animator.StringToHash("TurnsLeft");

    void Awake()
    {
        _doorAnimationController = GetComponent<Animator>();
        
        _doorAnimationController.SetBool(TurnsLeft, openDoorToTheLeft);
    }


    private void StartAnimation()
    {
        Debug.LogFormat($"Starting animation, doorIsOpen: {doorIsOpen}");
        _doorAnimationController.SetBool("IsOpen", doorIsOpen);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat($"Triggered by {other.name}, door is open: {doorIsOpen}" );
        if (doorIsOpen && !other.CompareTag("Player"))
        {
            return;
        }
        doorIsOpen = !doorIsOpen;
        StartAnimation();
    }
    
}
