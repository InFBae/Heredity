using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Door : MonoBehaviour, IOpenable
{
	protected HingeJoint hinge;
	protected Rigidbody rb;

	[SerializeField]
	private float openMinAngle;

	[SerializeField]
	private float openMaxAngle;

	[SerializeField]
	private float lockMinAngle;

	[SerializeField]
	private float lockMaxAngle;

	protected virtual void Awake()
	{
		hinge = gameObject.GetComponent<HingeJoint>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	public abstract void Open();

	public virtual void Unlock()
	{
		SetHingeAngle(openMinAngle, openMaxAngle);
	}

	public virtual void Lock()
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
}
