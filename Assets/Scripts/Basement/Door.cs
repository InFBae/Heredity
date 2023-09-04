using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour, IOpenable
{
	private HingeJoint hinge;
	private Rigidbody rb;

	[SerializeField]
	private float openMinAngle;

	[SerializeField]
	private float openMaxAngle;

	[SerializeField]
	private float lockMinAngle;

	[SerializeField]
	private float lockMaxAngle;

	private Vector3 originPosition;
	private Quaternion originRotation;

	private void Awake()
	{
		hinge = gameObject.GetComponent<HingeJoint>();
		rb = gameObject.GetComponent<Rigidbody>();

		originPosition = transform.position;
		originRotation = transform.rotation;

		Unlock();
	}

	public void Open()
	{
		
	}

	public void Unlock()
	{
		SetHingeAngle(openMinAngle, openMaxAngle);
	}

	public void Lock()
	{
		SetHingeAngle(lockMinAngle, lockMaxAngle);
	}

	private void SetHingeAngle(float minAngle, float maxAngle)
	{
		hinge.useLimits = true;

		JointLimits limits = hinge.limits;
		limits.min = minAngle;
		limits.max = maxAngle;
		hinge.limits = limits;
	}

	public void CloseDoor(SelectExitEventArgs args)
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		transform.position = originPosition;
		transform.rotation = originRotation;
	}

}
