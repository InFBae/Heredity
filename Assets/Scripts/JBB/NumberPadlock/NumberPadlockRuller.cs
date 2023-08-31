using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPadlockRuller : MonoBehaviour
{
    private GameObject _myRuller;
    public float blinkingTime = 0.5f;

    private void Start()
    {
        _myRuller = gameObject;
    }

    public void StartBlinkingMaterial()
    {
        _myRuller.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.clear, Color.yellow, Mathf.PingPong(Time.time, blinkingTime)));          
    }

    public void StopBlinkingMaterial()
    {
        _myRuller.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
    }
}
