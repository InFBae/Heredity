using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FuseSocket : MonoBehaviour, ISocketConnectable
{
	private XRSocketInteractor socketInteractor;

	private void Awake()
	{
		socketInteractor = gameObject.GetComponent<XRSocketInteractor>();


		//socketInteractor.hoverEntered.AddListener(CanHover);
	}

	public bool CanHover(XRBaseInteractable interactable)
	{
		throw new System.NotImplementedException();
	}

	public bool CanSelect(XRBaseInteractable interactable)
	{
		throw new System.NotImplementedException();
	}

	
}
