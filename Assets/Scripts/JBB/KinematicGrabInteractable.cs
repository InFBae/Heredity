using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KinematicGrabInteractable : XRGrabInteractable
{
    Rigidbody m_rb;

    protected override void Awake()
    {
        base.Awake();
        m_rb = GetComponent<Rigidbody>();
    }
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        m_rb.isKinematic = true;

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        m_rb.isKinematic = false;
    }
}
