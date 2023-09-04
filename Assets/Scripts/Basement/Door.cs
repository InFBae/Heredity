using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour, IOpenable
{
	private HingeJoint hingeJoint;

	[SerializeField]
	private float openMinAngle;

	[SerializeField]
	private float openMaxAngle;

	[SerializeField]
	private float lockMinAngle;

	[SerializeField]
	private float lockMaxAngle;

	private void Awake()
	{
		hingeJoint = gameObject.GetComponent<HingeJoint>();


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
		hingeJoint.useLimits = true;

		JointLimits limits = hingeJoint.limits;
		limits.min = minAngle;
		limits.max = maxAngle;
		hingeJoint.limits = limits;
	}

}
