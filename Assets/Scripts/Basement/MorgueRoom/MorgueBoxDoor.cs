using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Basement.MorgueRoom
{
	public class MorgueBoxDoor : Door
	{
		private MorgueBoxDoorHandle handle { get; set; }

		public bool IsOpened;

		public UnityAction OnChangedDoorState;

		protected override void Awake()
		{
			base.Awake();

			handle = gameObject.GetComponentInChildren<MorgueBoxDoorHandle>();
			handle.OnMovedDoor += SetMovedDoor;

			IsOpened = false;

			Unlock();
		}

		public override void Open()
		{
		}

		private void SetMovedDoor(bool isOpened)
		{
			//if(handle.IsFixedDoorRotation())
			//{
			//	transform.localRotation = Quaternion.Euler(0, -100, 0);
			//	hinge.breakForce = 0;
			//}
				

			IsOpened = isOpened;
			OnChangedDoorState?.Invoke();
		}

		public void SetDisableOpenable()
		{
			rb.isKinematic = true;
		}
	}
}