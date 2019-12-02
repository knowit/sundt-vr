using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAnimation : MonoBehaviour
{
    private Animator _doorAnimationController;

    [SerializeField] private bool openDoorToTheLeft = false;
    [SerializeField] private bool doorIsOpen = false;
    private bool _firstTime = true;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private static readonly int TurnsLeft = Animator.StringToHash("TurnsLeft");
    private static readonly int FirstTime = Animator.StringToHash("FirstTime");

    void Awake()
    {
        _doorAnimationController = GetComponent<Animator>();
        
        _doorAnimationController.SetBool(TurnsLeft, openDoorToTheLeft);
    }


    private void StartAnimation()
    {
        _doorAnimationController.SetBool(IsOpen, doorIsOpen);
        if (_firstTime)
        {
            _doorAnimationController.SetBool(FirstTime, true);
            _firstTime = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (doorIsOpen && !other.CompareTag("Player"))
        {
            return;
        }
        doorIsOpen = !doorIsOpen;
        StartAnimation();
    }
    
}
