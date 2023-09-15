using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicDoor : Door
{
    protected XRBaseInteractable[] handles;
    protected override void Awake()
    {
        base.Awake();

        handles = GetComponentsInChildren<XRBaseInteractable>();

        Lock();
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

        if (hinge.angle <= hinge.limits.min + 1 )
        {
            //Debug.Log("Door Locked");
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
