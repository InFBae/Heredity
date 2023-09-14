using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basement.MachineRoom
{
	public class MachineRoomDoor : Door
	{
		protected override void Awake()
		{
			base.Awake();

			Lock();
		}

		public override void Open()
		{
		}
	}
}