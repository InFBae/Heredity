using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintCanvas : MonoBehaviour
{
    [SerializeField] Hint hint;

    [SerializeField] TMP_Text hintCount;
    [SerializeField] TMP_Text hintText;

    string hint1 = "상담실 의자를 정돈하고 컴퓨터 화면을 확인하세요.";
    string hint2 = "라디에이터의 세기를 조절한 뒤 라디에이터 위 벽을 확인하세요.";
    string hint3 = "두 가지의 물약을 빈 병에 담고 섞어서 흰색 물약을 완성하세요.";
    string hint4 = "퓨즈를 전원차단기에 넣고 레버를 돌려 전력을 공급하세요.";
    string hint5 = "선반의 스프레이를 테이블 위에 분사해 미션을 확인하세요.";

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
