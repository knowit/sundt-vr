using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField] private GameObject ovrPlayerController;
    [SerializeField] private GameObject fpsController;
    
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ovrPlayerController.SetActive(false);
            fpsController.SetActive(true);
        }
        else
        {
            ovrPlayerController.SetActive(true);
            fpsController.SetActive(false);
        }
    }

}
