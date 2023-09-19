using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Elevator
{
	public class ElevatorRider : MonoBehaviour
	{
		[SerializeField]
		private FloorBtn[] floorBtns;

		[SerializeField]
		private ArrivalPoint enteredPoint;

		[SerializeField]
		private XROrigin xrOrigin;

		private FloorBtn currentBtn;

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

			enteredPoint.ArrivalSound.Play();
		}

		private void SelectedFloor(FloorBtn clickedBtn)
		{
			currentBtn = clickedBtn;

			mover.OnEndMovedElevator += ArrivalElevator;
			mover.OnStartMovedElevator?.Invoke(moveDirection, clickedBtn.FloorNum);
		}

		private void ArrivalElevator(int currentFloor)
		{
			currentBtn.SetInitBtn();
			currentBtn = null;

			mover.OnEndMovedElevator -= ArrivalElevator;
			quitter.ExitedElevator(currentFloor, xrOrigin);
		}
	}
}