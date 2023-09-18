using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MorgueRoom
{
	public class MorgueBed : Drawer
	{
		[SerializeField]
		private Transform boxFrame;

		[SerializeField]
		private float pullDistance;

		[SerializeField]
		private MorgueBoxHint boxHint;

		private XRGrabInteractable grabInteractable;
		private Rigidbody rb;

		protected override void Awake()
		{
			base.Awake();

			grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
			grabInteractable.selectExited.AddListener(DrawerBed);
			grabInteractable.selectEntered.AddListener(SelectedGrab);
			rb = gameObject.GetComponent<Rigidbody>();
		}

		private void SelectedGrab(SelectEnterEventArgs args)
		{
			//rb.constraints = RigidbodyConstraints.None;
		}

		private void DrawerBed(SelectExitEventArgs args)
		{
			//*
			if(Vector3.Distance(boxFrame.position, transform.position) > pullDistance) 
			{
				boxHint.IsActiveGrab(true);
				//rb.constraints = RigidbodyConstraints.FreezePositionZ;
			}
			else
			{
				boxHint.IsActiveGrab(false);
			}
			//*/
		}
	}
}