using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Basement.MorgueRoom
{
    public class SprayHead : MonoBehaviour
	{
		[SerializeField]
		private int sprayTime;

		public bool IsSpraying { get; set; }
		public int SprayCount { get; set; }
		
		private void Update()
		{
			if(IsSpraying)
			{
				RaycastHit hit;

				if (Physics.Raycast(transform.position, transform.right, out hit))
				{
					//Debug.DrawRay(transform.position, transform.right * hit.distance, Color.red);
					//Debug.Log($"[Raycast] : {hit.collider.gameObject.name}");

					var hitObject = hit.collider.gameObject.GetComponent<IScatterable>();

					if(hitObject != null)
					{
						//Debug.Log($"[SprayHead] : {SprayCount}");

						if(++SprayCount == sprayTime)
						{
							//Debug.Log($"[SprayHead] : ScatteredView");

							SprayCount = 0;
							hitObject.ScatteredView();
						}
					}
					else
					{
						SprayCount = 0;
					}
				}
				else
				{
					SprayCount = 0;
				}
			}
		}
	}
}