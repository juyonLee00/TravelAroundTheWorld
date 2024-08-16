using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{

    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        // 마스터 볼륨 초기값 설정
        float currentBGMVolume;
        SoundManager.Instance.audioMixer.GetFloat(SoundManager.Instance.bgmVolumeParameter, out currentBGMVolume);
        bgmVolumeSlider.value = Mathf.Pow(10, currentBGMVolume / 20);

        // SFX 볼륨 초기값 설정
        float currentSFXVolume;
        SoundManager.Instance.audioMixer.GetFloat(SoundManager.Instance.sfxVolumeParameter, out currentSFXVolume);
        sfxVolumeSlider.value = Mathf.Pow(10, currentSFXVolume / 20);

        // 슬라이더 값이 변경될 때마다 볼륨 조절
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.Instance.SetBGMVolume(1.0f); // 최대 볼륨
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.Instance.SetBGMVolume(0.1f); // 낮은 볼륨
        }
    }

    private void SetBGMVolume(float volume)
    {
        SoundManager.Instance.SetBGMVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }
}
