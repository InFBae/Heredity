using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Basement.MorgueRoom
{
	public class MorgueRoomDoor : MonoBehaviour
	{
		[SerializeField]
		private float openAngle;

		private void Awake()
		{
		}

		public void Unlock()
		{
            StartCoroutine(OpenDoorRoutine());
        }

        private IEnumerator OpenDoorRoutine()
        {
            float startAngle = transform.rotation.eulerAngles.y;

            float elapsedTime = 0;
            while (elapsedTime < 3f)
            {
                float t = elapsedTime / 3f;
                float currentAngle = Mathf.Lerp(startAngle, openAngle, t);

                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentAngle, transform.rotation.eulerAngles.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, openAngle, transform.rotation.eulerAngles.z);
        }
    }
}