using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource backgroundSource;
    [SerializeField]
    private AudioSource effectSource;


    [SerializeField]
    public AudioClip BackgroundMusic;
    [SerializeField]
    public AudioClip BossgroundMusic;

    [SerializeField]
    public AudioClip Aura;
    [SerializeField]
    public AudioClip Brechen;
    [SerializeField]
    public AudioClip Construction;
    [SerializeField]
    public AudioClip Defeat;
    [SerializeField]
    public AudioClip Explosion1;
    [SerializeField]
    public AudioClip Explosion2;
    [SerializeField]
    public AudioClip Cannon1;
    [SerializeField]
    public AudioClip Cannon2;
    [SerializeField]
    public AudioClip Radio;
    [SerializeField]
    public AudioClip Hit;


    [SerializeField]
    public AudioClip Death;
    [SerializeField]
    public AudioClip Victory;

    [SerializeField]
    public AudioClip BossIntro;
    [SerializeField]
    public AudioClip BossHit;


    private void Awake()
    {
        instance = this;
        GameManager.GameStart += StartMusic;
            GameManager.GameOver += StopMusic;
    }

    private void StopMusic()
    {
        effectSource.PlayOneShot(Defeat);
        backgroundSource.Stop();
    }

    private void StartMusic()
    {
        backgroundSource.clip = BackgroundMusic;
        backgroundSource.Play();
    }

    public void PlayerSound(AudioClip clipToPlay) => effectSource.PlayOneShot(clipToPlay);

    public void PlayBossMusic()
    {
        backgroundSource.clip = BossgroundMusic;
        backgroundSource.Play();
    }
}
