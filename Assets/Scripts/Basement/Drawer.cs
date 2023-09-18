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

	protected virtual void Awake()
	{
		configurableJoint = gameObject.GetComponent<ConfigurableJoint>();
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
