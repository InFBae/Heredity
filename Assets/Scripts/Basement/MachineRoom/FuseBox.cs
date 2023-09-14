using Elevator;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MachineRoom
{
	public class FuseBox : MonoBehaviour
	{
		[SerializeField]
		private FuseBoxLever fuseLever;

		[SerializeField]
		private FuseSocket fuseSocket;

		[SerializeField]
		private ElevatorController elevator;

		[SerializeField]
		private ParticleSystem[] sparksParticles;

		private void Awake()
		{
			fuseLever.LockLever();
			fuseLever.OnMovedLever += MovedLever;
		}

		public void PutFuse(SelectEnterEventArgs args)
		{
			foreach (var particle in sparksParticles)
				particle.Play();

			fuseLever.UnLockLever();
		}

		public void RemoveFuse(SelectExitEventArgs args)
		{
			fuseLever.LockLever();
		}

		private void MovedLever(bool isLeverDown)
		{
			fuseSocket.IsLockSocket = isLeverDown;

			if(isLeverDown) 
				elevator.StartElevator();
			else
				elevator.StopElevator();
		}
	}
}
