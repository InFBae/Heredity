using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Item : XRGrabInteractable
{
    public bool inSlot;
    public Vector3 slotPosition = Vector3.zero;
    public Vector3 slotRotation = Vector3.zero;
    public Vector3 slotScale = Vector3.one;
    public Vector3 originalScale = Vector3.one;
    public Slot currentSlot;

    private bool isOnSlot;
    private Slot onSlot;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (inSlot)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            currentSlot.ItemInSlot = null;
            transform.SetParent(null);
            transform.localScale = originalScale;
            inSlot = false;
            currentSlot.ResetColor();
            currentSlot = null;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (!inSlot && isOnSlot && onSlot.ItemInSlot == null)
        {
            onSlot.InsertItem(gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        onSlot = other.GetComponent<Slot>();
        if (onSlot != null)
        {
            isOnSlot = true;
        }
        else
        {
            isOnSlot = false;
        }
    }

}
