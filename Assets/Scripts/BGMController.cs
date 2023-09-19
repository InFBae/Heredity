using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] string startBGM;
    private void Start()
    {
        GameManager.Sound.Play($"{startBGM}", SoundManager.Sound.BGM);
    }

    public void ChangeBGM(string path)
    {
        GameManager.Sound.Play(path, SoundManager.Sound.BGM);
    }
}
