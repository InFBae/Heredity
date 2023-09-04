using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] Slider slider = null;
    [SerializeField] float m_BarLength = 5f;
    [SerializeField] float m_Seconds = 5f;
    [SerializeField] GameObject m_NextStep = null;

    [SerializeField] VisitRoomMission visitRoomMission;

    float m_SecondsCnt;
    bool m_UpdateTimer;

    private void Update()
    {
        if (m_UpdateTimer)
            UpdateTimer();
    }

    public void UpdateTimerState(bool state)
    {
        m_UpdateTimer = state;
    }

    void UpdateTimer()
    {
        slider.maxValue = m_BarLength;

        m_SecondsCnt += Time.deltaTime;
        if (m_SecondsCnt > m_Seconds)
        {
            m_SecondsCnt = 0f;
            if (m_NextStep != null)
                m_NextStep.SetActive(true);

            // visitRoomMission.SetChairPosition();
            slider.gameObject.SetActive(false);
        }

        slider.value = m_SecondsCnt;
        // m_Blendshape.SetBlendShapeWeight(0, m_SecondsCnt / m_Seconds * m_BarLength);
    }

}
