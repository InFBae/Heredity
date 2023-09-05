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

    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;
        GameObject go = other.gameObject;
        if (!IsItem(go)) return;
        if (!go.GetComponent<XRBaseInteractable>().isSelected)
        {
            if (!go.GetComponent<Item>().inSlot)
                InsertItem(go);
        }
    }

    private bool IsItem(GameObject go)
    {
        return go.GetComponent<Item>();
    }

    private void InsertItem(GameObject go)
    {
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.transform.SetParent(gameObject.transform, true);
        go.transform.localPosition = go.GetComponent<Item>().slotPosition;
        go.transform.localEulerAngles = go.GetComponent<Item>().slotRotation;
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
