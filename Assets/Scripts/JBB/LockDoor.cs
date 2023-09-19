using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockDoor : BasicDoor
{
    private bool isLocked = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        //Debug.Log(hinge.angle);
        foreach (XRBaseInteractable handle in handles)
        {
            if (handle.isSelected)
            {
                return;
            }
        }

        if (hinge.angle <= hinge.limits.min + 1)
        {
            //Debug.Log("Door Locked");
            Lock();
        }
    }

    public new void Unlock(int step)
    {
        //Debug.Log($"Step : {step}");
        if (!isLocked && step == 1)
        {
            //Debug.Log("LockDoor Unlocked");
            Unlock();
        }
    }

    public void SetLock(bool isLocked)
    {
        this.isLocked = isLocked;
    }
}
