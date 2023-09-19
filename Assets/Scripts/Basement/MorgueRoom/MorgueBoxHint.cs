using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MorgueRoom
{
	public class MorgueBoxHint : MonoBehaviour
	{

		private XRGrabInteractable grabInteractable;
		private Rigidbody rb;



		private bool isGrabHint;

		private void Awake()
		{
			grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
			rb = gameObject.GetComponent<Rigidbody>();

			isGrabHint = false;
		}

		private void Start()
		{
			IsActiveGrab(false);
		}

		public void IsActiveGrab(bool isActive)
		{
			if (isGrabHint == false)
			{
				rb.constraints = isActive ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
			}
		}

		private void OnSelectEnter(SelectEnterEventArgs args)
		{
			isGrabHint = true;
		}

		private void OnSelectExit(SelectExitEventArgs args)
		{
			isGrabHint = false;
		}
	}
}
