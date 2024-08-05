using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public string volumeParameter = "MasterVolume";
    public float fadeDuration = 1.0f; 

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Coroutine fadeCoroutine;

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
    }

    public void PlayMusic(string audioClipName, bool loop = true)
    {
        string resourcePath = $"Sounds/{audioClipName}";
        StartCoroutine(LoadAndPlayMusic(resourcePath, loop));
    }

    private IEnumerator LoadAndPlayMusic(string resourcePath, bool loop)
    {
        ResourceRequest request = Resources.LoadAsync<AudioClip>(resourcePath);
        yield return request;

        AudioClip musicClip = request.asset as AudioClip;
        if (musicClip != null)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeMusic(musicClip, loop));
        }
        else
        {
            Debug.LogWarning($"No music found for audio clip in folder: {resourcePath}");
        }
    }

    private IEnumerator FadeMusic(AudioClip newClip, bool loop)
    {
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOut(musicSource, fadeDuration));
        }

        musicSource.clip = newClip;
        musicSource.loop = loop;
        musicSource.Play();

        yield return StartCoroutine(FadeIn(musicSource, fadeDuration));
    }


    public void PlaySFX(string audioClipName)
    {
        string resourcePath = $"Sounds/{audioClipName}";
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

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0;
        float startVolume = audioSource.volume;
        float endVolume = 1;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = endVolume;
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(volume) * 20);
    }

    public void Mute(bool mute)
    {
        audioMixer.SetFloat(volumeParameter, mute ? -80 : 0);
    }

    public void StopAllSounds()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        musicSource.Stop();
        sfxSource.Stop();
    }
}
