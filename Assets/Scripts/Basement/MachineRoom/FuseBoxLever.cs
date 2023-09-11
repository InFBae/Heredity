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
		/// 레버를 놓았을 때 레버 고정여부
		/// </summary>
		[SerializeField]
		private bool isLockLever;

		/// <summary>
		/// 레버 활성 여부
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
		/// 핸들 이동 방향 기준으로  레버 움직일 방향 설정
		/// </summary>
		/// <returns></returns>
		private Vector3 GetLookDirection()
		{
			Vector3 direction = currentInteractor.GetAttachTransform(this).position - handle.position; //핸들 움직이는 방향 
			direction = transform.InverseTransformDirection(direction);
			direction.x = 0; //x축 기준 회전하기 위함

			return direction.normalized;
		}

		/// <summary>
		/// 레버 값 업데이트
		/// </summary>
		private void UpdateValue()
		{
			var lookDirection = GetLookDirection();
			var lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.z * -1) * Mathf.Rad2Deg; //레버 핸들의 회전 각도  계산

			if (minAngle < maxAngle)
				lookAngle = Mathf.Clamp(lookAngle, minAngle, maxAngle);
			else
				lookAngle = Mathf.Clamp(lookAngle, maxAngle, minAngle);

			var maxAngleDistance = Mathf.Abs(maxAngle - lookAngle); //최대 각도와 현재 lookAngle 사이 각도 차이 계산
			var minAngleDistance = Mathf.Abs(minAngle - lookAngle); //최소 각도와 현재 lookAngle 사이 각도 차이 계산

			//레버 값에 따라 데드존을 적용 : 데드존 보다 작은 움직임은 움직이지 않았다고 판단함
			if (isOnLever)
				maxAngleDistance *= (1.0f - LeverDeadZone);
			else
				minAngleDistance *= (1.0f - LeverDeadZone);

			var newValue = (maxAngleDistance < minAngleDistance); //레버 값 변경 여부 결정 

			SetHandleAngle(lookAngle); 

			SetValue(newValue);
		}

		/// <summary>
		/// 레버 값 설정
		/// </summary>
		/// <param name="isOn">레버 고정 여부 설정</param>
		/// <param name="forceRotation">레버 강제 회전 여부</param>
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
		/// 레버 각도 세팅
		/// </summary>
		/// <param name="angle">세팅할 각 (x값)</param>
		private void SetHandleAngle(float angle)
		{
			if (handle != null)
				handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
		}
	}
}
