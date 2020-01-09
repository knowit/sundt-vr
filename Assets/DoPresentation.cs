using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class DoPresentation : MonoBehaviour
{
    [SerializeField] private string guideName = "NavAgent";
    [SerializeField] private VideoPlayer presentation;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //not the one, no not the one
        }
        else
        {
            //this is the one
            if (other.name.Equals(guideName))
            {
                presentation.Play();
            }
        }
    }

}
