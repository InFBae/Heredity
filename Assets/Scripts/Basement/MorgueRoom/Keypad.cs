using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basement.MorgueRoom
{
    public enum KeypaydNumType
    {
        Cancel = 98,
        Enter = 99,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Zero = 0
    }

    public class Keypad : MonoBehaviour
    {
        [SerializeField]
        private int[] password;

        [SerializeField]
        private KeypadDisplay display;

        [SerializeField]
        private KeypadBtn[] keypads;

        [SerializeField]
        private MorgueRoomDoor door;

        private void Awake()
        {
            foreach (var btn in keypads)
            {
                btn.OnBtnClick += ClikedKeypadNum;
            }
        }

        private void ClikedKeypadNum(KeypaydNumType inputData)
        {
            switch(inputData)
            {
                case KeypaydNumType.Enter:
                    CheckedCorrectPassword();
                    break;
                case KeypaydNumType.Cancel:
                    display.CancelInputData();
                    break;
                default:
                    display.SetDisplayText((int)inputData);
                    break;
            }
        }

        private void CheckedCorrectPassword()
        {
            var inputPassword = display.inputDatas.ToArray();

            if(inputPassword.Length == keypads.Length)
            {
                bool isCorrect = true;
                for(int i =0; i<inputPassword.Length; i++)
                {
                    if (inputPassword[i] != (int)keypads[i].KeyNumberType)
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if(isCorrect)
                {
                    door.Unlock();
                }
            }
        }
    }
}