using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public string bgmVolumeParameter = "BGMVolume";
    public string sfxVolumeParameter = "SFXVolume"; 
    private float fadeDuration = 0.02f; 

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private AudioSource dialogueSource;
    private Coroutine fadeCoroutine;

    private float prevAudioVolume;

    private string currentMusicClipName;
    private string currentSFXClipName;
    private string currentDialogueClipName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

        dialogueSource = gameObject.AddComponent<AudioSource>(); // 대화용 오디오 소스 초기화
        dialogueSource.loop = false;

        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        SetBGMVolume(musicSource.volume);
        SetSFXVolume(sfxSource.volume);
    }

    //배경음악 재생
    public void PlayMusic(string audioClipName, bool loop = true)
    {
        string resourcePath = $"Sounds/{audioClipName}";
        currentMusicClipName = audioClipName;
        prevAudioVolume = musicSource.volume;
        StartCoroutine(LoadAndPlayMusic(resourcePath, loop));
    }

    private IEnumerator LoadAndPlayMusic(string resourcePath, bool loop)
    {
        ResourceRequest request = Resources.LoadAsync<AudioClip>(resourcePath);
        yield return request;
        Debug.Log("LoadAndPlay");
        AudioClip musicClip = request.asset as AudioClip;
        if (musicClip != null)
        {
            if (fadeCoroutine != null)
            {
                Debug.Log("StopCoroutine");
                StopCoroutine(fadeCoroutine);
            }
            Debug.Log("StartCoroutine");
            fadeCoroutine = StartCoroutine(FadeMusic(musicClip, loop));
            Debug.Log("EndCoroutine");
        }
        else
        {
            Debug.LogWarning($"No music found for audio clip in folder: {resourcePath}");
        }
    }

    //효과음 재생
    public void PlaySFX(string audioClipName)
    {
        string resourcePath = $"Sounds/{audioClipName}";
        currentSFXClipName = audioClipName;
        StartCoroutine(LoadAndPlaySFX(resourcePath));
    }

    private IEnumerator LoadAndPlaySFX(string resourcePath)
    {
        ResourceRequest request = Resources.LoadAsync<AudioClip>(resourcePath);
        yield return request;

        AudioClip sfxClip = request.asset as AudioClip;
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
        else
        {
            Debug.LogWarning($"No SFX found for audio clip in folder: {resourcePath}");
        }
    }

    //대화창 사운드 재생
    public void PlayDialogueSound(string audioClipName)
    {
        string resourcePath = $"Sounds/{audioClipName}";
        currentDialogueClipName = audioClipName;
        StartCoroutine(LoadAndPlayDialogueSound(resourcePath));
    }


    private IEnumerator LoadAndPlayDialogueSound(string resourcePath)
    {
        ResourceRequest request = Resources.LoadAsync<AudioClip>(resourcePath);
        yield return request;

        AudioClip dialogueClip = request.asset as AudioClip;
        if (dialogueClip != null)
        {
            dialogueSource.PlayOneShot(dialogueClip);
        }
        else
        {
            Debug.LogWarning($"No dialogue sound found for audio clip in folder: {resourcePath}");
        }
    }

    //FadeIn 위한 코루틴 설정
    private IEnumerator FadeMusic(AudioClip newClip, bool loop)
    {
        if (musicSource.isPlaying)
        {
           //해당 코루틴이 켜지면 맵 이동시 사운드가 0이 된 상태로 유지되어서 수정 필요
            yield return StartCoroutine(FadeOut(musicSource, fadeDuration));
        }
        musicSource.clip = newClip;
        musicSource.loop = loop;
        musicSource.Play();

        yield return StartCoroutine(FadeIn(musicSource, fadeDuration));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        Debug.Log("PlayMusic");
        float startVolume = 0;
        float endVolume = prevAudioVolume;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endVolume;
        Debug.Log(audioSource.volume);
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float endVolume = 0;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endVolume;
        audioSource.Stop();
    }


    public void SetBGMVolume(float volume)
    {
        musicSource.volume = volume;
        prevAudioVolume = volume;
        //오디오 믹서 적용 후 수정
        //audioMixer.SetFloat(bgmVolumeParameter, Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        dialogueSource.volume = volume;

        //오디오믹서 적용 후 수정 
        //audioMixer.SetFloat(sfxVolumeParameter, Mathf.Log10(volume) * 20);
    }

    //전체 마스터볼륨 조절
    /*
    public void Mute(bool mute)
    {
        audioMixer.SetFloat(volumeParameter, mute ? -80 : 0);
    }
    */

    public void StopAllSounds()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        musicSource.Stop();
        sfxSource.Stop();
    }

    public void StopMusic()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        musicSource.Stop();
        currentMusicClipName = null; 
    }

    public void StopSFX()
    {
        sfxSource.Stop();
        currentSFXClipName = null; // 현재 재생 중인 SFX 클립 이름 초기화
    }

    public void StopDialogueSound()
    {
        dialogueSource.Stop();
        currentDialogueClipName = null;
    }

    public void StopCurrentMusic()
    {
        if (musicSource.isPlaying)
        {
            StopMusic();
        }
    }


    public void StopCurrentSFX()
    {
        if (sfxSource.isPlaying)
        {
            StopSFX();
        }
    }

}
