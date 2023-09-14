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

        public Stack<int> inputDatas {  get; private set; }

        private void Awake()
        {
            imgDisplay = gameObject.GetComponent<Image>();

            inputDatas = new Stack<int>();
        }

        public void SetDisplayText(int inputData)
        {
            inputDatas.Push(inputData);
            ShowDispaly();
        }

        public void CancelInputData()
        {
            inputDatas.Pop();
            ShowDispaly();
        }

        private void ShowDispaly()
        {
            if(inputDatas.Count > 4)
            {
                txtDisplayData.text = string.Join(" ", inputDatas.TakeLast(4));
            }
            else
                txtDisplayData.text = string.Join(" ", inputDatas);
        }
    }
}