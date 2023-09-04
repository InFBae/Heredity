using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieGazeController : MonoBehaviour
{
    private GameObject player;

    [SerializeField] public Slider slider = null;
    [SerializeField] float m_BarLength = 3f;
    [SerializeField] float m_Seconds = 3f;
    [SerializeField] Zombie zombie;

    float m_SecondsCnt;
    bool m_UpdateTimer;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

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
            if (zombie.curStateNum != 1)
            {
                m_SecondsCnt = 0f;
                zombie.targetEntity = player;

                slider.gameObject.SetActive(false);
            }
        }

        slider.value = m_SecondsCnt;
    }
}
