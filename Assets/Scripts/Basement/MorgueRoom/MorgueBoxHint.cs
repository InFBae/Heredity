using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MorgueBoxHint : MonoBehaviour
{

	private XRGrabInteractable grabInteractable;
	private Rigidbody rb;

	private void Awake()
	{
		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		rb = gameObject.GetComponent<Rigidbody>();
		//IsActiveGrab(false);
	}

	public void IsActiveGrab(bool isActive)
	{
		Debug.Log($"[IsActiveGrab] {isActive}");

		//rb.constraints = isActive ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;

		//grabInteractable.interactionLayers = isActive ? enableMask : disableMask;
	}
}
