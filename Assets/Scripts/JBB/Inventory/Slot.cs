using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    public Image slotImage;
    Color originalColor;

    private void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

    public void InsertItem(GameObject go)
    {
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.transform.SetParent(gameObject.transform, true);
        go.transform.localPosition = go.GetComponent<Item>().slotPosition;
        go.transform.localEulerAngles = go.GetComponent<Item>().slotRotation;
        go.transform.localScale = go.GetComponent<Item>().slotScale;        
        go.GetComponent<Item>().inSlot = true;
        go.GetComponent<Item>().currentSlot = this;
        ItemInSlot = go;
        slotImage.color = Color.gray;
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }
}
