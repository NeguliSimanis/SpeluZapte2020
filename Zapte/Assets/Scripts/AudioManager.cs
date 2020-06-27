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

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void SetMusicSpeed(float speed)
    {
        
        audioSource.pitch = speed;
    }

    public void PlayFastMusic()
    {
        Debug.Log("STARTING FST ");
        fastAudioSource.clip = fastMusic;
        fastAudioSource.Play();
        audioSource.Stop();
    }
}
