using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class test123 : MonoBehaviour
{
    public void HoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"{args.interactableObject.transform.gameObject.name} entered");
    }
}
