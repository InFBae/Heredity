using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIController : MonoBehaviour
{
    PlayerInput rightHandInput;
    [SerializeField] Canvas settingCanvas;
    [SerializeField] XRInteractorLineVisual[] rayLines;
    [SerializeField] GameObject UIOffset;
    [SerializeField] GameObject XROrigin;
    bool isEnabled = false;

    private void Awake()
    {
        rightHandInput = GetComponent<PlayerInput>();
        settingCanvas.enabled = isEnabled;
    }

    public void OnSetting()
    {
        isEnabled = !isEnabled;
        settingCanvas.enabled = isEnabled;
        settingCanvas.gameObject.transform.position = UIOffset.transform.position;
        settingCanvas.gameObject.transform.rotation = XROrigin.transform.rotation;
        if (isEnabled)
        {
            foreach (XRInteractorLineVisual line in rayLines)
            {
                line.enabled = true;
            }
        }
        else
        {
            foreach (XRInteractorLineVisual line in rayLines)
            {
                line.enabled = false;
            }
        }
    }
}
