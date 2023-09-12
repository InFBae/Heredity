using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoor : Door
{
    protected override void Awake()
    {
        base.Awake();
        Lock();
    }

    private void Update()
    {
        if (hinge.angle <= hinge.limits.min + 1)
        {
            Debug.Log("Door Locked");
            Lock();
        }
    }

    public override void Open()
    {
    }

    public void Unlock(int step)
    {
        //Debug.Log($"Step : {step}");
        if (step == 1)
        {
            //Debug.Log("Basic Door Unlocked");
            Unlock();
        }
    }
}
