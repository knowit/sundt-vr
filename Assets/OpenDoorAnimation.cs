using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAnimation : MonoBehaviour
{
    private Animator doorAnimationController;

    [SerializeField] private bool openDoorToTheLeft = false;
    [SerializeField] private bool doorIsOpen = false;

    private static readonly int CloseDoorLeft = Animator.StringToHash("CloseDoorLeft");
    private static readonly int CloseDoor = Animator.StringToHash("CloseDoor");
    private static readonly int OpenDoorLeft = Animator.StringToHash("OpenDoorLeft");
    private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
    
    // Start is called before the first frame update
    void Awake()
    {
        doorAnimationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startAnimation()
    {
        if (openDoorToTheLeft)
        {
            if (doorIsOpen)
            {
                doorAnimationController.Play(CloseDoorLeft);
            }
            else
            {
                doorAnimationController.Play(OpenDoorLeft);
            }

            //doorAnimationController.SetTrigger(AnimateDoor);
        }
        else
        {
            if (doorIsOpen)
            {
                doorAnimationController.Play(CloseDoor);
            }
            else
            {
                doorAnimationController.Play(OpenDoor);
            }
        }

        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        startAnimation();
        doorIsOpen = !doorIsOpen;
    }
    
}
