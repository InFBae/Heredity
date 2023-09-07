using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Elevator
{
	public class MoveByFloorController : MonoBehaviour
	{
		[SerializeField]
		private MoveBtn btnMoveUp;

		[SerializeField]
		private MoveBtn btnMoveDwon;

		[SerializeField]
		private TMP_Text txtCurrentLocation;

		public ElevatorMover Mover { get; set; }
		public ElevatorRider Rider { get; set; }

		private MoveBtn btnCurrentActive;
		private bool isOneway;

		private void Awake()
		{
			if(btnMoveUp != null)
				btnMoveUp.OnClikcedButton += OnClikcedMoveButton;

			if(btnMoveDwon != null)
				btnMoveDwon.OnClikcedButton += OnClikcedMoveButton;

			isOneway = (btnMoveUp == null || btnMoveDwon == null);
		}

		private void Start()
		{
			Mover.OnMovingElevator += DoMovingElevator;
		}


		private void OnClikcedMoveButton(ElevMoveDirection clikcedDir)
		{
			btnCurrentActive = GetClikcedBtn(clikcedDir);
			
			SetMoveBtnActive(clikcedDir == ElevMoveDirection.Up ? btnMoveDwon : btnMoveUp);

			var elevDir = Mover.CurrentFloor - btnCurrentActive.FloorNum < 0 ? ElevMoveDirection.Up : ElevMoveDirection.Down;

			Mover.OnEndMovedElevator += ArrivalElevator;
			Mover.OnStartMovedElevator?.Invoke(elevDir, btnCurrentActive.FloorNum);
		}

		private MoveBtn GetClikcedBtn(ElevMoveDirection clikcedDir)
		{
			if (isOneway)
				return btnMoveUp == null ? btnMoveDwon : btnMoveUp;
			else
				return clikcedDir == ElevMoveDirection.Up ? btnMoveUp : btnMoveDwon;
		}

		private void SetMoveBtnActive(MoveBtn otherBtn)
		{
			if (isOneway == false)
				otherBtn.BtnElevator.gameObject.SetActive(!btnCurrentActive.IsEnableBtn);
		}

		private void DoMovingElevator(ElevMoveDirection direction, int currentPos)
		{
			txtCurrentLocation.text = currentPos == 0 ? "B1" : currentPos.ToString();
		}

		private void ArrivalElevator(int currentFloor)
		{
			Mover.OnEndMovedElevator -= ArrivalElevator;

			SetMoveBtnActive(btnCurrentActive.MoveDirection == ElevMoveDirection.Up ? btnMoveDwon : btnMoveUp);

			btnCurrentActive.SetInitBtn();

			Rider.EnteredElevator(Mover, btnCurrentActive.MoveDirection);

		}
	}
}