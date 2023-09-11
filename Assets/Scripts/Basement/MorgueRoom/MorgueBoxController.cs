using Basement.MorgueRoom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MorgueBoxController : MonoBehaviour
{
	[SerializeField]
	private MorgueBox[] morgueBoxes;

	[SerializeField]
	private Image imgKeyPostionHint;

	private int closedCnt = 0;

	private void Awake()
	{
		foreach(var box in morgueBoxes)
		{
			box.OnChangedStatus += ChangedDoorStatus;
		}
	}

	private void ChangedDoorStatus()
	{
		if (morgueBoxes.Count(x => x.IsCorrected) == morgueBoxes.Length)
		{ 
			CompleteRoom(); 
		}
	}

	private void CompleteRoom()
	{
		imgKeyPostionHint.gameObject.SetActive(true);
	}
}
