using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip defaultBGMusic;
    [SerializeField]
    AudioClip menuMusic;

    [SerializeField]
    AudioSource fastAudioSource;
    [SerializeField]
    AudioClip fastMusic;

    [SerializeField]
    AudioSource sfxAudioSource;
    [SerializeField]
    AudioClip friendFoundSFX;
    [SerializeField]
    AudioClip friendLostSFX;
    [SerializeField]
    AudioClip keyTapSFX;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    public void StartMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
        fastAudioSource.Stop();
    }
    public void StartGameMusic()
    {
        audioSource.clip = defaultBGMusic;
        audioSource.Play();
    }

    public void SetMusicSpeed(float speed)
    {
        audioSource.pitch = speed;
    }

    public void PlayFriendFoundSFX()
    {
        sfxAudioSource.pitch = Random.Range(0.85f, 1.1f);
        sfxAudioSource.PlayOneShot(friendFoundSFX);
    }

    public void PlayFriendLostSFX()
    {
        sfxAudioSource.pitch = Random.Range(0.85f, 1.1f);
        sfxAudioSource.PlayOneShot(friendLostSFX);
    }

    public void PlayKeyTapSFX()
    {
        sfxAudioSource.pitch = Random.Range(0.85f, 1.1f);
        sfxAudioSource.PlayOneShot(keyTapSFX);
    }

    public void PlayFastMusic(bool start)
    {
        if (start)
        {
            fastAudioSource.clip = fastMusic;
            fastAudioSource.Play();
            audioSource.Stop();
        }
        else
        {
            fastAudioSource.Stop();
            audioSource.Play();
        }
    }
}
