using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MachineRoom
{
	public class FuseSocket : XRSocketInteractor
	{
		private IXRHoverInteractable selectedObj;

		public bool IsLockSocket;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void OnHoverEntering(HoverEnterEventArgs args)
		{
			base.OnHoverEntering(args);

			selectedObj = args.interactableObject;
		}

		protected override void OnHoverExiting(HoverExitEventArgs args)
		{
			base.OnHoverExiting(args);

			selectedObj = null;
		}

		protected override void OnSelectEntered(SelectEnterEventArgs args)
		{
			base.OnSelectEntered(args);
		}

		protected override void OnSelectExiting(SelectExitEventArgs args)
		{
			base.OnSelectExiting(args);
		}

		public override bool CanSelect(IXRSelectInteractable interactable)
		{
			if (selectedObj != null)
			{
				if (base.CanSelect(interactable))
				{
					var connected = selectedObj.transform.gameObject.GetComponent<Fuse>();
					return (connected != null && connected.IsConnectable);
				}
			}
			return false;
		}
	}
}