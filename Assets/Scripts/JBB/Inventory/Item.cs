using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Item : XRGrabInteractable
{
    public bool inSlot;
    public Vector3 slotPosition = Vector3.zero;
    public Vector3 slotRotation = Vector3.zero;
    public Vector3 slotScale = Vector3.one;
    public Vector3 originalScale = Vector3.one;
    public Slot currentSlot;

    public UnityEvent<GameObject> onSlot = new UnityEvent<GameObject>();

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (inSlot)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            currentSlot.ItemInSlot = null;
            transform.SetParent(null);
            transform.localScale = originalScale;
            inSlot = false;
            //gameObject.layer = LayerMask.NameToLayer("Item");
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Item"));
            currentSlot.ResetColor();
            currentSlot = null;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        onSlot?.Invoke(this.gameObject);
    }

}
