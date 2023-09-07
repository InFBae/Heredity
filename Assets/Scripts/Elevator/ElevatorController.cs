using Elevator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

namespace Elevator
{
	public class ElevatorController : MonoBehaviour
	{
		private ElevatorMover mover;
		private ElevatorRider rider;
		private ElevatorQuitter quitter;

		[SerializeField]
		private MoveByFloorController[] moveControllers;

		private void Awake()
		{
			InitComponents();
		}

		private void InitComponents()
		{
			mover = gameObject.GetComponent<ElevatorMover>();
			rider = gameObject.GetComponent<ElevatorRider>();
			quitter = gameObject.GetComponent<ElevatorQuitter>();

			rider.quitter = quitter;

			foreach (var controller in moveControllers)
			{
				controller.Mover = mover;
				controller.Rider = rider;
			}
				
		}
	}

}
