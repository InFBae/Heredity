using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MachineRoom
{
	public class DoorLockSocket : XRSocketInteractor
	{
		[SerializeField]
		private Door door;

		[SerializeField]
		private int delayTime;

		private bool isEnteredKey = false;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void OnSelectEntered(SelectEnterEventArgs args)
		{
			base.OnSelectEntered(args);

			isEnteredKey = args.interactableObject.transform.GetComponent<IKeychain>() != null;
		}

		protected override void OnSelectExiting(SelectExitEventArgs args)
		{
			base.OnSelectExiting(args);

			if (args.interactableObject.transform.GetComponent<IKeychain>() != null && isEnteredKey)
			{
				StartCoroutine(DelayOpenDoor());
			}
		}

		public override bool CanHover(IXRHoverInteractable interactable)
		{
			if (!base.CanHover(interactable))
				return false;

			var keyChain = interactable.transform.GetComponent<IKeychain>();
			return (keyChain != null);
		}

		public override bool CanSelect(IXRSelectInteractable interactable)
		{
			if (!base.CanSelect(interactable))
				return false;

			var keyChain = interactable.transform.GetComponent<IKeychain>();
			return (keyChain != null);
		}

		private IEnumerator DelayOpenDoor()
		{
			int time = 0;

			while (delayTime >= time)
			{
				time++;
				yield return new WaitForSeconds(1f);
			}
			door.Unlock();
		}
	}
}