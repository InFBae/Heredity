using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider bgmSlider;

    [SerializeField] SwitchManager soundSwitch;
    [SerializeField] SwitchManager bgmSwitch;

    float prevSoundValue = 0;
    float prevBgmValue = 0;

    private void Awake()
    {
        soundSlider.value = 0;
        bgmSlider.value = 0;
        soundSwitch.isOn = true;
        bgmSwitch.isOn = true;
    }

    public void OnSoundOnEvent()
    {
        soundSlider.value = prevSoundValue;
    }

    public void OnSoundOffEvent()
    {
        prevSoundValue = soundSlider.value;
        soundSlider.value = -80;
    }

    public void OnBgmOnEvent()
    {
        bgmSlider.value = prevBgmValue;
    }

    public void OnBgmOffEvent()
    {
        prevBgmValue = bgmSlider.value;
        bgmSlider.value = -80;
    }

    public void SetBGMVolume(float volume)
    {
        GameManager.Sound.SetAudioVolume("BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        GameManager.Sound.SetAudioVolume("SFX", volume);
    }
}
