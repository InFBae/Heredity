using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] Hint hint;

    [SerializeField] TMP_Text hintCount;
    [SerializeField] TMP_Text hintText;

    string hint1 = "상담실 의자를 정돈하세요";
    string hint2 = "라디에이터 위 벽을 확인하세요";
    string hint3 = "힌트 3";
    string hint4 = "힌트 4";
    string hint5 = "힌트 5";

    private void OnEnable()
    {
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
    }
}
