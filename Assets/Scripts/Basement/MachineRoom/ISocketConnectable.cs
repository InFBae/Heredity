using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface ISocketConnectable 
{
	public bool CanHover(XRBaseInteractable interactable);

	public bool CanSelect(XRBaseInteractable interactable);

}
