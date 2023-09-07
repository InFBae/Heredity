using Elevator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Elevator
{
	public class ElevatorQuitter : MonoBehaviour
	{
		[SerializeField]
		private ArrivalPoint[] arrivalPoints;

		public void ExitedElevator(int arrival, XROrigin xrOrigin)
		{
			var point = arrivalPoints.Where(x => x.FloorNum == arrival).FirstOrDefault();

			if (point != null)
			{
				xrOrigin.transform.position = point.transform.position;
				xrOrigin.transform.rotation = point.transform.rotation;
			}
		}
	}
}
