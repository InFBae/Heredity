using Elevator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Basement.MorgueRoom
{
	public class MorgueBox : MonoBehaviour
    {
		[SerializeField]
		private MorgueBoxAnswerInfo[] answers;

		public UnityAction OnChangedStatus;

		public bool IsCorrected { get; private set; }

		private void Awake()
		{
			foreach (var info in answers)
			{
				info.Door.OnChangedDoorState += CheckedMoveAnswers;
			}
		}

		private void CheckedMoveAnswers()
		{
			IsCorrected = !(answers.Any(x => x.IsOpenDoor != x.Door.IsOpened));

			OnChangedStatus?.Invoke();
		}
	}
}