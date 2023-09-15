using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Basement.MorgueRoom
{
    public class KeypadBtn : MonoBehaviour
    {
        [SerializeField]
        private KeypaydNumType keyNumber;
        public KeypaydNumType KeyNumberType { get { return keyNumber; } }

        [SerializeField]
        private TMP_Text txtKeyNumber;

        [SerializeField]
        private LayerMask PlayerLayer;

        private Image imgKeypadBtn;

        private bool isActiveBtn = false;
        public UnityAction<KeypaydNumType> OnBtnClick;

        private void Awake()
        {
            imgKeypadBtn = gameObject.GetComponent<Image>();

            if (keyNumber == KeypaydNumType.Enter)
                txtKeyNumber.text = "E";
            else if (keyNumber == KeypaydNumType.Cancel)
                txtKeyNumber.text = "C";
            else
                txtKeyNumber.text = ((int)keyNumber).ToString();
        }

        public void OnTriggerEnter(Collider other)
        {
            if(isActiveBtn == false)
            {
                if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
                {
                    Debug.Log($"[KeypadBtn] OnTriggerEnter : {keyNumber}");

                    imgKeypadBtn.color = Color.gray;
                    OnBtnClick?.Invoke(keyNumber);

                    isActiveBtn = true;
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
            {
                imgKeypadBtn.color = Color.white;
                isActiveBtn = false;
            }
        }
    }
}