using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
	private XRGrabInteractable grabInteractable;

	[SerializeField]
	private Door door;

	private void Awake()
	{
		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();

		//grabInteractable.selectExited.AddListener(() => { door.CloseDoor(); });
	}
}
