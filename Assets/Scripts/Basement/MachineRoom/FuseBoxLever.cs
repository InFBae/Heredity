using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MachineRoom
{
	public class FuseBoxLever : XRBaseInteractable
	{
		const float LeverDeadZone = 0.05f;

		[SerializeField]
		private float minAngle;

		private float maxAngle;

		[SerializeField]
		private float lockAngle;

		[SerializeField]
		private float unlockAngle;

		[SerializeField]
		private Transform handle;

		/// <summary>
		/// ������ ������ �� ���� ��������
		/// </summary>
		[SerializeField]
		private bool isLockLever;

		/// <summary>
		/// ���� Ȱ�� ����
		/// </summary>
		[SerializeField]
		private bool isOnLever;

		private IXRSelectInteractor currentInteractor;

		public UnityAction OnLeverActivate;
		public UnityAction OnLeverDeactivate;

		public UnityAction<bool> OnMovedLever;

		//private bool isGrab;

		protected override void Awake()
		{
			base.Awake();

			selectEntered.AddListener(SelectedGrab);
			selectExited.AddListener(ExitedGrab);
		}

		private void SelectedGrab(SelectEnterEventArgs args)
		{
			currentInteractor = args.interactorObject;
		}

		private void ExitedGrab(SelectExitEventArgs args)
		{
			bool isDown;
			if (handle.localRotation.x <= 0.75)
			{
				handle.localRotation = Quaternion.identity;
				isDown = false;
			}
			else
			{
				handle.localRotation = Quaternion.Euler(maxAngle, 0, 0);
				isDown = true;
			}
			OnMovedLever?.Invoke(isDown);

			currentInteractor = null;
		}

		public void LockLever()
		{
			maxAngle = lockAngle;
		}

		public void UnLockLever()
		{
			maxAngle = unlockAngle;
		}

		public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
		{
			base.ProcessInteractable(updatePhase);

			if (isSelected)
			{
				if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
				{
					UpdateValue();
				}
			}
		}

		/// <summary>
		/// �ڵ� �̵� ���� ��������  ���� ������ ���� ����
		/// </summary>
		/// <returns></returns>
		private Vector3 GetLookDirection()
		{
			Vector3 direction = currentInteractor.GetAttachTransform(this).position - handle.position; //�ڵ� �����̴� ���� 
			direction = transform.InverseTransformDirection(direction);
			direction.x = 0; //x�� ���� ȸ���ϱ� ����

			return direction.normalized;
		}

		/// <summary>
		/// ���� �� ������Ʈ
		/// </summary>
		private void UpdateValue()
		{
			var lookDirection = GetLookDirection();
			var lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.z * -1) * Mathf.Rad2Deg; //���� �ڵ��� ȸ�� ����  ���

			if (minAngle < maxAngle)
				lookAngle = Mathf.Clamp(lookAngle, minAngle, maxAngle);
			else
				lookAngle = Mathf.Clamp(lookAngle, maxAngle, minAngle);

			var maxAngleDistance = Mathf.Abs(maxAngle - lookAngle); //�ִ� ������ ���� lookAngle ���� ���� ���� ���
			var minAngleDistance = Mathf.Abs(minAngle - lookAngle); //�ּ� ������ ���� lookAngle ���� ���� ���� ���

			//���� ���� ���� �������� ���� : ������ ���� ���� �������� �������� �ʾҴٰ� �Ǵ���
			if (isOnLever)
				maxAngleDistance *= (1.0f - LeverDeadZone);
			else
				minAngleDistance *= (1.0f - LeverDeadZone);

			var newValue = (maxAngleDistance < minAngleDistance); //���� �� ���� ���� ���� 

			SetHandleAngle(lookAngle); 

			SetValue(newValue);
		}

		/// <summary>
		/// ���� �� ����
		/// </summary>
		/// <param name="isOn">���� ���� ���� ����</param>
		/// <param name="forceRotation">���� ���� ȸ�� ����</param>
		private void SetValue(bool isOn, bool forceRotation = false)
		{
			if (isOnLever == isOn)
			{
				if (forceRotation)
					SetHandleAngle(isOnLever ? maxAngle : minAngle);

				return;
			}

			isOnLever = isOn;

			if (isOnLever)
				OnLeverActivate?.Invoke();
			else
				OnLeverDeactivate?.Invoke();

			if (!isSelected && (isLockLever || forceRotation))
				SetHandleAngle(isOnLever ? maxAngle : minAngle);
		}

		/// <summary>
		/// ���� ���� ����
		/// </summary>
		/// <param name="angle">������ �� (x��)</param>
		private void SetHandleAngle(float angle)
		{
			if (handle != null)
				handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
		}
	}
}
