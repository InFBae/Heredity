using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Basement.MorgueRoom
{
    public class Spray : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem sparyEffect;

        [SerializeField]
        private bool IsScatterable;

		private SprayHead sprayHead;
		private XRGrabInteractable grabInteractable;
        
		private void Awake()
		{
			sprayHead = gameObject.GetComponentInChildren<SprayHead>();

			if (IsScatterable)
            {
				grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
				grabInteractable.activated.AddListener(StartSpray);
				grabInteractable.deactivated.AddListener(StopSpray);
			}    
		}

		private void StartSpray(ActivateEventArgs args)
        {
			sparyEffect.Play();
			sprayHead.IsSpraying = true;
		}

        private void StopSpray(DeactivateEventArgs args)
        {
			sparyEffect.Stop();
			sprayHead.IsSpraying = false;
		}
    }
}