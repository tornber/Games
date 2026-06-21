using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;   
    }

    public void PlaySFX(AudioClip audio,float volume = 1f)
    {
        StartCoroutine(PlaySFXCoroutine(audio,volume));
    }

    IEnumerator PlaySFXCoroutine(AudioClip audio, float volume = 1f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds(audio.length * 2f);

        Destroy(audioSource);
    }
}
