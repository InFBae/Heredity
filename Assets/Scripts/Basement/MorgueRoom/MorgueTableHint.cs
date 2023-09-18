using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.MorgueRoom
{
    public class MorgueTableHint : MonoBehaviour, IScatterable
    {
        [SerializeField]
        private Image imgHint;

		[SerializeField]
		private Image imgHintPoint;

		private int openCnt;

		private void Awake()
		{
			openCnt = 0;
		}

		public void ScatteredView()
		{
			if(imgHint.color.a <= 0.8)
			{
				var color = imgHint.color;
				color.a = (++openCnt * 1) * 0.1f;
				imgHint.color = color;
			}
			else if (imgHintPoint.color.a <= 1)
			{
				var color = imgHintPoint.color;
				color.a = (++openCnt * 1) * 0.1f;
				imgHintPoint.color = color;
			}
		}
	}
}