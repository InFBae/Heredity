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
        _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.clear, Color.yellow, Mathf.PingPong(Time.time, blinkingTime)) * 0.01f);     
    }

    public void StopBlinkingMaterial()
    {
        _myRuller.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        _myRuller.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
    }

    public int GetNumber()
    {
        // 0 216 -> 324     215 -> 324 180 180 -> 
        // 1 180 -> 360
        // 2 144 -> 36
        // 3 111 -> 69      110 -> 69 180 180 -> -110 -> 69
        // 4 72 -> 72
        // 5 395 -> 35      394 -> 34 0 0 -> -145 -> 34
        // 6 359 -> 359
        // 7 321 -> 321
        // 8 288 -> 288     288 -> 288 0 0 -> 108 -> 288
        // 9 252 -> 287     248 -> 291 180 180 -> 111 -> 291

        float eulerAngX = transform.localEulerAngles.x;
        float eulerAngY = transform.localEulerAngles.y;
        float xAngle;

        if (eulerAngY >= 180f)
        {
            if (eulerAngX > 270f)
                xAngle = 540f - eulerAngX;
            else
                xAngle = 180 -eulerAngX;
        }
        else
        {
            xAngle = eulerAngX;
        }
        float x = 10 - ((xAngle) / 36f);
        return ((Mathf.RoundToInt(x)) + 6) % 10;
    }
}
