using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
	private XRGrabInteractable grabInteractable;

	private Rigidbody rb;

	[SerializeField]
	private Door door;

	[SerializeField]
	private Transform morgueBox;

	private float originPosDistance;

	private void Awake()
	{
		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		rb = gameObject.GetComponent<Rigidbody>();

		originPosDistance = Vector3.Distance(morgueBox.position, transform.position);
	}

	public void CloseDoor(SelectExitEventArgs args)
	{
		//if (Vector3.Distance(morgueBox.position, transform.position) <= originPosDistance)
		//{
		//	rb.velocity = Vector3.zero;
		//	rb.angularVelocity = Vector3.zero;
		//}
		//
		//door.CloseDoor(args);
	}

}
