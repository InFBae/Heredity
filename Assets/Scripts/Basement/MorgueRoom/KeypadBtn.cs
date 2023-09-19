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

        public Image ImgKeypadBtn { get; private set; }

        private bool isActiveBtn = false;
        public UnityAction<KeypadBtn> OnBtnClick;

        private void Awake()
        {
			ImgKeypadBtn = gameObject.GetComponent<Image>();

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

                    OnBtnClick?.Invoke(this);

                    isActiveBtn = true;
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & PlayerLayer) != 0)
            {
                isActiveBtn = false;
            }
        }
    }
}