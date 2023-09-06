using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : MonoBehaviour
{
	protected XRGrabInteractable grabInteractable;
	protected Rigidbody rb;

	[SerializeField]
	private Transform doorFrame;

	[SerializeField]
	private float damperSize; 

	private float originPositionDistance;

	private Vector3 originPosition;
	private Quaternion originRotation;

	protected virtual void Awake()
	{
		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		rb = gameObject.GetComponent<Rigidbody>();

		originPositionDistance = Vector3.Distance(doorFrame.position, transform.position) + damperSize;
		originPosition = transform.position;
		originRotation = transform.rotation;

		grabInteractable.selectExited.AddListener((args) => CloseDoor(args));

		Debug.Log($"[Close - Current] Distance : {Vector3.Distance(doorFrame.position, transform.position)}");
		Debug.Log($"[Close - Oring] Distance : {originPositionDistance}");

	}

	public void CloseDoor(SelectExitEventArgs args)
	{
		Debug.Log($"[Close - Current] Distance : {Vector3.Distance(doorFrame.position, transform.position)}");
		Debug.Log($"[Close - Oring] Distance : {originPositionDistance}");

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		if (Vector3.Distance(doorFrame.position, transform.position) <= originPositionDistance)
		{
			transform.position = originPosition;
			transform.rotation = originRotation;
		}
	}
}
