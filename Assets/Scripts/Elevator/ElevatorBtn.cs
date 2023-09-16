using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Elevator
{ 
	public class ElevatorBtn : MonoBehaviour
    {
		[SerializeField]
		protected TMP_Text btnInfoText;

		private bool isClicked;
		public bool IsEnableBtn { get { return isClicked; } }

		[SerializeField]
		protected int floorNumber;
		public int FloorNum { get { return floorNumber; } }

		[SerializeField]
		private LayerMask PlayerLayer;

		public UnityAction OnElevatorBtnClick;

		protected bool isActiveBtn = true;

		protected virtual void Awake()
		{
			OnElevatorBtnClick += SetClickedBtn;
            SetInitBtn();
		}
	
		private void SetClickedBtn()
		{
			isClicked = isClicked ? false : true;
			btnInfoText.color = isClicked ? Color.red : Color.white;
		}
	
		public void SetInitBtn()
		{
			isClicked = false;
			btnInfoText.color = Color.white;
		}

		public void OnTriggerEnter(Collider other)
		{
			if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
			{
				if (isActiveBtn)
					OnElevatorBtnClick?.Invoke();
			}
		}

		public void SetActiveBtn(bool isActive)
		{
			isActiveBtn = isActive;
        }
    }
}