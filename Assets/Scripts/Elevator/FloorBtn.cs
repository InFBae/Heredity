using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Elevator
{
	public class FloorBtn : ElevatorBtn
	{
		public UnityAction<FloorBtn> OnSelectedFloor;

		protected override void Awake()
		{
			base.Awake();

            OnElevatorBtnClick += DoMoveFloor;
        }

		private void DoMoveFloor()
		{
			OnSelectedFloor?.Invoke(this);
		}
	}
}