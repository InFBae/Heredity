using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Elevator
{
	public class ElevatorRider : MonoBehaviour
	{
		[SerializeField]
		private FloorBtn[] floorBtns;

		[SerializeField]
		private Transform enteredPoint;

		[SerializeField]
		private XROrigin xrOrigin;

		private ElevMoveDirection moveDirection;
		private ElevatorMover mover;

		public ElevatorQuitter quitter { get; set; }

		private void Awake()
		{
			foreach (var btn in floorBtns) 
				btn.OnSelectedFloor += SelectedFloor;
		}

		public void EnteredElevator(ElevatorMover mover, ElevMoveDirection direction)
		{
			this.mover = mover;
			this.moveDirection = direction;

			xrOrigin.transform.position = enteredPoint.transform.position;
			xrOrigin.transform.rotation = enteredPoint.transform.rotation;
		}

		private void SelectedFloor(int arrival)
		{
			Debug.Log($"[SelectedFloor] : {arrival}");

			mover.OnEndMovedElevator += ArrivalElevator;
			mover.OnStartMovedElevator?.Invoke(moveDirection, arrival);
		}

		private void ArrivalElevator(int currentFloor)
		{
			mover.OnEndMovedElevator -= ArrivalElevator;
			quitter.ExitedElevator(currentFloor, xrOrigin);
		}
	}
}