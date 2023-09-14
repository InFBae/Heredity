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

        private int openCnt;

		private void Awake()
		{
			openCnt = 0;
		}

		public void ScatteredView()
		{
			var color = imgHint.color;
			color.a = (++openCnt * 2) * 0.1f;
			imgHint.color = color;

			Debug.Log($"[ScatteredView] : {color.a}");
		}
	}
}