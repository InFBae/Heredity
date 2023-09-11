using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] Hint hint;

    [SerializeField] TMP_Text hintCount;
    [SerializeField] TMP_Text hintText;

    string hint1 = "면회실에서 지하 1층 영안실 열쇠를 획득할 수 있습니다";
    string hint2 = "";
    string hint3 = "원장실 라디에이터의 세기를 조절해보세요";
    string hint4 = "힌트 4";
    string hint5 = "힌트 5";
    string hint6 = "힌트 6";

    private void OnEnable()
    {
        // int randNum = Random.Range(1, 6);
        hintCount.text = "남은 힌트 : " + hint.leftHintCount.ToString();

        if (hint.triggerNum == 1 )
            hintText.text = hint1;
        else if (hint.triggerNum == 2 )
            hintText.text = hint2;
        else if (hint.triggerNum == 3)
            hintText.text = hint3;
        else if (hint.triggerNum == 4)
            hintText.text = hint4;
        else if (hint.triggerNum == 5)
            hintText.text = hint5;
        else if (hint.triggerNum == 6)
            hintText.text = hint6;
    }
}
