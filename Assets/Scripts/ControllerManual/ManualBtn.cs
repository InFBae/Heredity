using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ControllerManual
{
	public class ManualBtn : MonoBehaviour
	{
		[SerializeField]
		private LayerMask playerLayer;

		[SerializeField]
		private RectTransform manualCanvas;

		public void OnTriggerEnter(Collider other)
		{
			Debug.Log("[OnTriggerEnter] IN TRIGGER");

			if (((1 << other.gameObject.layer) & playerLayer) != 0)
			{
				Debug.Log("[OnTriggerEnter] IN  PLAYERLAYER");
				manualCanvas.gameObject.SetActive(false);
			}
		}

	}

}
