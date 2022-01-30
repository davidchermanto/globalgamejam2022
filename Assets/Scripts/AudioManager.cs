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
        int index = 0;

        switch (clip)
        {
            case "hitheavy":
                index = 0;
                break;
            case "hitlight":
                index = 1;
                break;
            case "hitverylight":
                index = 2;
                break;
            case "wave":
                index = 3;
                break;
            case "turnend":
                index = 4;
                break;
            case "battle1":
                index = 5;
                break;
            case "battle2":
                index = 6;
                break;
        }

        clipPlayer.PlayOneShot(soundEffects[index]);
    }

    public void PlaySoundtrack(string clip)
    {

    }
}
