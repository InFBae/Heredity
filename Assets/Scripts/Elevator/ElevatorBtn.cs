using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

		public Button BtnElevator { get; private set; }

		protected virtual void Awake()
		{
			BtnElevator = gameObject.GetComponent<Button>();
			BtnElevator.onClick.AddListener(SetClickedBtn);
	
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
	}
}