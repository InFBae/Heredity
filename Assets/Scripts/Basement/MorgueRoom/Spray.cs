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
		private AudioSource spraySound;

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
				grabInteractable.selectExited.AddListener(StopSpray);
			}    
		}

		private void StartSpray(ActivateEventArgs args)
        {
			sparyEffect.Play();
			spraySound.Play();
			sprayHead.IsSpraying = true;
		}

        private void StopSpray(DeactivateEventArgs args)
        {
			StopSpray();
		}

		private void StopSpray(SelectExitEventArgs args)
		{
			StopSpray();
		}

		private void StopSpray()
		{
			sparyEffect.Stop();
			spraySound.Stop();
			sprayHead.IsSpraying = false;
		}
	}
}