using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRoomDoor : Door
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
