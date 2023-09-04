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

	[SerializeField]
	private float damperSize; 

	private float originPositionDistance;

	private Vector3 originPosition;
	private Quaternion originRotation;

	private void Awake()
	{
		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		rb = gameObject.GetComponent<Rigidbody>();

		originPositionDistance = Vector3.Distance(morgueBox.position, transform.position) + damperSize;
		originPosition = transform.position;
		originRotation = transform.rotation;
	}

	public void CloseDoor(SelectExitEventArgs args)
	{
		//Debug.Log($"[Close - Current] Distance : {Vector3.Distance(morgueBox.position, transform.position)}");
		//Debug.Log($"[Close - Oring] Distance : {originPositionDistance}");

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		if (Vector3.Distance(morgueBox.position, transform.position) <= originPositionDistance)
		{
			transform.position = originPosition;
			transform.rotation = originRotation;
		}
	}
}
