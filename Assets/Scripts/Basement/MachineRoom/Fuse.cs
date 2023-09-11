using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fuse : MonoBehaviour
{
	public bool IsConnectable;

	private XRGrabInteractable grabInteractable;

	private void Awake()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
	}

	public void SetActiveGrab(bool isActive)
	{
		
	}
}
