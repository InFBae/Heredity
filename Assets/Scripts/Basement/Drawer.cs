using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Drawer : MonoBehaviour, IPullable
{
	private ConfigurableJoint configurableJoint;

	[SerializeField]
	private float limitLength;

	[SerializeField]
	private float lockLength;

	private void Awake()
	{
		configurableJoint = gameObject.GetComponent<ConfigurableJoint>();

		// Unlock();
	}

	public void Pull()
	{
		
	}

	public void Unlock()
	{
		SetLinearLimit(limitLength);
	}

	public void Lock()
	{
		SetLinearLimit(lockLength);
	}

	private void SetLinearLimit(float length)
	{
		SoftJointLimit linearLimit = configurableJoint.linearLimit;
		linearLimit.limit = length;
		configurableJoint.linearLimit = linearLimit;
	}
}
