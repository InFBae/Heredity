using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basement.MorgueRoom
{
	public class MorgueRoomDoor : Door
	{
		protected override void Awake()
		{
			base.Awake();

			Unlock();
		}

		public override void Open()
		{
		}
	}
}