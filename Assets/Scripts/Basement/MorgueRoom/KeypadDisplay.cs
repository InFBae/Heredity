using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.MorgueRoom
{
    public class KeypadDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text txtDisplayData;

        private Image imgDisplay;

        public List<int> inputDatas {  get; private set; }

        private void Awake()
        {
            imgDisplay = gameObject.GetComponent<Image>();
            inputDatas = new List<int>();
        }

        public void SetDisplayText(int inputData)
        {
			Debug.Log($"SetDisplayText : {inputData}");

			inputDatas.Add(inputData);
            ShowDisplay();
        }

        public void CancelInputData()
        {
            Debug.Log("CancelInputData");

            if(inputDatas.Count > 0)
                inputDatas.RemoveAt(inputDatas.Count-1);

            ShowDisplay();
        }

        private void ShowDisplay()
        {
            Debug.Log(string.Join(" ", inputDatas));

            if(inputDatas.Count > 4)
                txtDisplayData.text = string.Join(" ", inputDatas.TakeLast(4));
            else
                txtDisplayData.text = string.Join(" ", inputDatas);
        }
    }
}
