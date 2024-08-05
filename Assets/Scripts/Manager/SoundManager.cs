using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioSource effectsSource;

    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PlayEffect(string soundName)
    {
        if (soundEffects.TryGetValue(soundName, out AudioClip clip))
        {
            effectsSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + soundName);
        }
    }

    public void AddSoundEffect(string soundName, AudioClip clip)
    {
        if (!soundEffects.ContainsKey(soundName))
        {
            soundEffects[soundName] = clip;
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }

    public void FadeInMusic(AudioClip newClip, float duration, bool loop = true)
    {
        StartCoroutine(FadeIn(audioSource, newClip, duration, loop));
    }

    public void FadeOutMusic(float duration)
    {
        StartCoroutine(FadeOut(audioSource, duration));
    }

    private IEnumerator FadeIn(AudioSource audioSource, AudioClip newClip, float duration, bool loop)
    {
        audioSource.volume = 0;
        audioSource.clip = newClip;
        audioSource.loop = loop;
        audioSource.Play();

        float startVolume = 0f;
        float endVolume = 1f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, t / duration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
