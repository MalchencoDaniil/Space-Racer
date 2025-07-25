using UnityEngine;

public class AllAudioSource : MonoBehaviour
{
    public static AllAudioSource Instance;

    public AudioSource SoundAudioSource;
    public AudioSource MusicAudioSource;

    private void Awake()
    {
        Instance = this;
    }
}