using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public bool useFpsControllerInEditor = true;

    [SerializeField] private GameObject ovrPlayerController;
    [SerializeField] private GameObject fpsController;
    
    private void Awake()
    {
        if (!useFpsControllerInEditor)
            return;

#if UNITY_EDITOR
        ovrPlayerController.SetActive(false);
        fpsController.SetActive(true);
#else
        ovrPlayerController.SetActive(true);
        fpsController.SetActive(false);
#endif
    }

}
