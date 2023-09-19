using Elevator;
using Basement.MorgueRoom;
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
		private Keypad keypad;

		[SerializeField]
		private ParticleSystem[] sparksParticles;

		[SerializeField]
		private AudioSource fuseConnectedSound;


		private void Awake()
		{
			fuseLever.LockLever();
			fuseLever.OnMovedLever += MovedLever;
		}

		public void PutFuse(SelectEnterEventArgs args)
		{
			fuseConnectedSound.Play();
			StartCoroutine(ConnectedEffectPlayRoutine());

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
			{
				keypad.StartKeypad();
				elevator.StartElevator();
			}
			else
			{
				keypad.StopKeypad();
				elevator.StopElevator();
			}
		}

		private IEnumerator ConnectedEffectPlayRoutine()
		{
			foreach (var particle in sparksParticles)
			{
				particle.Play();
				yield return new WaitForSeconds(0.5f);
			}
				
		}
	}
}
