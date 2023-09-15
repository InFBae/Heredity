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
			rb.isKinematic = true;
		}

        public override void Unlock()
        {
            base.Unlock();
			rb.isKinematic = false;
        }

        public override void Open()
		{
		}
	}
}