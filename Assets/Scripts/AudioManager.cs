using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource clipPlayer;
    [SerializeField] private AudioSource sountrackPlayer;

    [SerializeField] private List<AudioClip> soundEffects;
    [SerializeField] private List<AudioClip> soundtracks;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayOneShot(string clip)
    {

    }

    public void PlaySoundtrack(string clip)
    {

    }
}
