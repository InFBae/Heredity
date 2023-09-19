using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
					var hitObject = hit.collider.gameObject.GetComponent<IScatterable>();

					if(hitObject != null)
					{
						if(++SprayCount == sprayTime)
						{
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