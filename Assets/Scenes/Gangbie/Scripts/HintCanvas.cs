using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;

    string hint1 = "면회실에서 지하 1층 영안실 열쇠를 획득할 수 있습니다";
    string hint2 = "원장실 라디에이터의 세기를 조절해보세요";
    string hint3 = "힌트 3";
    string hint4 = "힌트 4";
    string hint5 = "힌트 5";

    private void OnEnable()
    {
        int randNum = Random.Range(1, 6);

        if (randNum == 1 )
            hintText.text = hint1;
        else if (randNum == 2 )
            hintText.text = hint2;
        else if (randNum == 3)
            hintText.text = hint3;
        else if (randNum == 4)
            hintText.text = hint4;
        else if (randNum == 5)
            hintText.text = hint5;
    }
}
