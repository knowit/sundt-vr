using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField] private GameObject ovrPlayerController;
    [SerializeField] private GameObject fpsController;
    
    // Start is called before the first frame update
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
