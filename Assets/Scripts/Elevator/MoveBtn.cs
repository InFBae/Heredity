using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Elevator
{
	public class MoveBtn : ElevatorBtn
	{
		[SerializeField]
		private ElevMoveDirection elevatorDirection;
		public ElevMoveDirection MoveDirection { get { return elevatorDirection; } }

		public UnityAction<ElevMoveDirection> OnClikcedButton;

		protected override void Awake()
		{
			base.Awake();

			OnElevatorBtnClick += NotifyClickedBtn;
        }

		private void NotifyClickedBtn()
		{
			OnClikcedButton?.Invoke(elevatorDirection);
		}
    }

}
