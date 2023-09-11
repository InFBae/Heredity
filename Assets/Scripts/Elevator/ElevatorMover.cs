using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace Elevator
{
	public class ElevatorMover : MonoBehaviour
	{
		public UnityAction<ElevMoveDirection, int> OnStartMovedElevator;
		public UnityAction<ElevMoveDirection, int> OnMovingElevator;
		public UnityAction<int> OnEndMovedElevator;

		public bool IsMoving { get; private set; } = false;

		public int CurrentFloor { get; private set; } = 2;

		private void Awake()
		{
			OnStartMovedElevator += DoMoveElevator;
			OnEndMovedElevator += SetEndMovedElevator;
		}

		public void DoMoveElevator(ElevMoveDirection direction, int floor)
		{
			if(IsMoving == false)
			{
				IsMoving = true;

				int movingCnt = Mathf.Abs(CurrentFloor - floor);
				StartCoroutine(MoveRoutine(direction, movingCnt));
			}
		}


		private IEnumerator MoveRoutine(ElevMoveDirection direction,  int moveCnt)
		{
			int moveStep = direction == ElevMoveDirection.Up ? 1 : -1;

			while (moveCnt > 0)
			{
				yield return new WaitForSeconds(1f);

				CurrentFloor += moveStep;

				OnMovingElevator?.Invoke(direction, CurrentFloor);
				moveCnt--;
			}

			OnEndMovedElevator?.Invoke(CurrentFloor);
		}

		private void SetEndMovedElevator(int floor)
		{
			IsMoving = false;
		}
	}
}
