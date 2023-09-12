using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


        protected CircleCollider2D btnCollider;
		//public Button BtnElevator { get; private set; }

		public UnityAction OnElevatorBtnClick;

		protected bool isActiveBtn = true;

        protected virtual void Awake()
		{
			//BtnElevator = gameObject.GetComponent<Button>();
			//BtnElevator.onClick.AddListener(SetClickedBtn);

			OnElevatorBtnClick += SetClickedBtn;

            btnCollider = gameObject.GetComponent<CircleCollider2D>();

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
            if (isActiveBtn)
                OnElevatorBtnClick?.Invoke();
        }

        public void SetActiveBtn(bool isActive)
		{
			isActiveBtn = isActive;
        }
    }
}