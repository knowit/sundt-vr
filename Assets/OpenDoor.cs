using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimationController;
    private static readonly int AnimateDoor = Animator.StringToHash("AnimateDoor");

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.enabled)
        {
            doorAnimationController.SetTrigger(AnimateDoor);
        }
    }

    private void OnMouseDown()
    {
        doorAnimationController.SetTrigger(AnimateDoor);
    }
}
