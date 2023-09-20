using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        private KeypadBtn[] keypadBtns;

        [SerializeField]
        private MorgueRoomDoor door;

        [SerializeField]
        private float delaySeconds;

        private AudioSource btnAudio;

        private bool isEnableBtn;

        private void Awake()
        {
			btnAudio = gameObject.GetComponent<AudioSource>();

			foreach (var btn in keypadBtns)
				btn.OnBtnClick += ClikedKeypadNum;

			StopKeypad();
		}

        public void StartKeypad()
        {
            isEnableBtn = true;
		}

        public void StopKeypad()
        {
            isEnableBtn = false;
		}


		private void ClikedKeypadNum(KeypadBtn clickedBtn)
        {
            if (isEnableBtn == false)
                return;

            StartCoroutine(ClikedBtnStatusRoutine(clickedBtn.ImgKeypadBtn));

			switch (clickedBtn.KeyNumberType)
            {
                case KeypaydNumType.Enter:
					CheckedCorrectPassword();
                    break;
                case KeypaydNumType.Cancel:
					display.CancelInputData();
                    break;
                default:
                    display.SetDisplayText((int)clickedBtn.KeyNumberType);
                    break;
            }

            StartCoroutine(WaitClikedBtnRoutine());
        }

        private void CheckedCorrectPassword()
        {
            var inputPassword = display.inputDatas.ToArray();

            if(inputPassword.Length == password.Length)
            {
                bool isCorrect = true;
                for(int i =0; i<inputPassword.Length; i++)
                {
                    if (inputPassword[i] != password[i])
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {
					door.Unlock();
                    return;
				}
			}
            StartCoroutine(PlayAudioCoroutine());
		}

        private IEnumerator WaitClikedBtnRoutine()
        {
            isEnableBtn = false;

			yield return new WaitForSeconds(delaySeconds);

            isEnableBtn = true;

		}

		private IEnumerator ClikedBtnStatusRoutine(Image btnImg)
		{
            btnImg.color = Color.gray;
			btnAudio.Play();

			yield return new WaitForSeconds(0.5f);

			btnImg.color = Color.white;

		}

		private IEnumerator PlayAudioCoroutine()
		{
            int currentRepeatCount = 0;
			while (currentRepeatCount < 2)
			{
				btnAudio.Play();
				yield return new WaitForSeconds(btnAudio.clip.length);

				currentRepeatCount++;
			}
		}
	}
}