using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

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

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
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
