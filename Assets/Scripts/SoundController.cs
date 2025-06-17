using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void BirdMoveUp(string clipName, float volumeMultiplier)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + clipName, typeof(AudioClip)));
        audioSource.volume *= volumeMultiplier;
    }
}
