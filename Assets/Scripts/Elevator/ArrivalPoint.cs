using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator
{
	public class ArrivalPoint : MonoBehaviour
	{
		[SerializeField]
		private int floorNumber;
		public int FloorNum { get { return floorNumber; } }
	}
}