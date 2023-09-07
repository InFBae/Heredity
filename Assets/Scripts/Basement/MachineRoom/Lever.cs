using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.DebugUI;

public class Lever : XRBaseInteractable
{
	const float LeverDeadZone = 0.1f;

	[SerializeField]
	private float minAngle;

	[SerializeField]
	private float maxAngle = 170;

	[SerializeField]
	private Vector3 rotationAxis;

	[SerializeField]
	private Transform handle;

	private XRGrabInteractable grabInteractable;

	private IXRSelectInteractor currentInteractor;

	private Vector3 initialPosition;

	bool isOnLever = false;

	public UnityAction OnLeverActivate;
	public UnityAction OnLeverDeactivate;

	protected virtual void Awake()
	{
		base.Awake();


		grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
		grabInteractable.selectEntered.AddListener(SelectedGrab);
		grabInteractable.selectExited.AddListener(ExitedGrab);

		initialPosition = transform.position;
	}

	private void SelectedGrab(SelectEnterEventArgs args)
	{
		currentInteractor = args.interactorObject;
		initialPosition = args.interactableObject.transform.position;
	}

	private void ExitedGrab(SelectExitEventArgs args)
	{
		currentInteractor = null;
	}
	/*
	public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
	{
		base.ProcessInteractable(updatePhase);

		if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
		{
			if (isSelected)
			{
				UpdateValue();
			}
		}
	}

	Vector3 GetLookDirection()
	{
		Vector3 direction = currentInteractor.GetAttachTransform(this).position - handle.position; //핸들 움직이는 방향 
		direction = transform.InverseTransformDirection(direction);
		direction.x = 0;

		return direction.normalized;
	}

	void UpdateValue()
	{
		var lookDirection = GetLookDirection();
		var lookAngle = Mathf.Atan2(lookDirection.z, lookDirection.y) * Mathf.Rad2Deg;

		if (minAngle < maxAngle)
			lookAngle = Mathf.Clamp(lookAngle, minAngle, maxAngle);
		else
			lookAngle = Mathf.Clamp(lookAngle, maxAngle, minAngle);

		var maxAngleDistance = Mathf.Abs(maxAngle - lookAngle);
		var minAngleDistance = Mathf.Abs(minAngle - lookAngle);

		if (isOnLever)
			maxAngleDistance *= (1.0f - LeverDeadZone);
		else
			minAngleDistance *= (1.0f - LeverDeadZone);

		var newValue = (maxAngleDistance < minAngleDistance);

		SetHandleAngle(lookAngle);

		SetValue(newValue);
	}
	
	void SetValue(bool isOn, bool forceRotation = false)
	{
		if (isOnLever == isOn)
		{
			if (forceRotation)
				SetHandleAngle(isOnLever ? maxAngle : minAngle);

			return;
		}

		isOnLever = isOn;

		if (isOnLever)
		{
			OnLeverActivate.Invoke();
		}
		else
		{
			OnLeverDeactivate.Invoke();
		}

		if (!isSelected && (m_LockToValue || forceRotation))
			SetHandleAngle(isOnLever ? maxAngle : minAngle);
	}

	void SetHandleAngle(float angle)
	{
		if (m_Handle != null)
			m_Handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
	}

	void OnValidate()
	{
		SetHandleAngle(isOnLever ? maxAngle : minAngle);
	}
	//*/
}
