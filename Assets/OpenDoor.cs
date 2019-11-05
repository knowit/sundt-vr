using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator doorAnimationController;
    private static readonly int DoClose = Animator.StringToHash("DoClose");
    private static readonly int DoOpen = Animator.StringToHash("DoOpen");

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
            var isOpen = doorAnimationController.GetCurrentAnimatorStateInfo(0).IsName("Open");
            doorAnimationController.SetTrigger(isOpen ? DoClose : DoOpen);
        }
    }
}
