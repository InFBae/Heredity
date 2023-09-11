using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MorgueRoom
{
	public class MorgueBoxDoorHandle : DoorHandle
	{
		[SerializeField]
		private float openPositionDistance;

		public UnityAction<bool> OnMovedDoor;

		protected override void Awake()
		{
			base.Awake();

			grabInteractable.selectExited.AddListener((args) => ChangedDoorState(args));
		}

		public void ChangedDoorState(SelectExitEventArgs args)
		{
			OnMovedDoor?.Invoke(isOpened);
		}

		public bool IsFixedDoorRotation()
		{
			return (Vector3.Distance(doorFrame.position, transform.position) > openPositionDistance);
		}
	}
}
