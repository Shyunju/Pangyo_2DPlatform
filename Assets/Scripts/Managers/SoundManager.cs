using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    [SerializeField] private AudioClip BGM_Home;
    [SerializeField] private AudioClip BGM_Stage_1;
    [SerializeField] private AudioClip BGM_Stage_2;
    [SerializeField] private AudioClip BGM_Stage_3;

    public AudioClip clickEffect;

    [SerializeField] private AudioClip stageClear;
    [SerializeField] private AudioClip gameOver;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "IntroScene")
        {
            BgmPlay(BGM_Home);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayPickUpItemEffect(AudioClip pickupSound)
    {
        effectAudioSource.PlayOneShot(pickupSound);
    }

    public void PlayClickEffect()
    {
        effectAudioSource.PlayOneShot(clickEffect);
    }

    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void ResumeBGM()
    {
        bgmAudioSource.Play();
    }

    public void PlayStageClear()
    {
        effectAudioSource.PlayOneShot(stageClear);
    }

    public void PlayGameOver()
    {
        effectAudioSource.PlayOneShot(gameOver);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded");
        if (scene.name == "GameScene")
        {
            BgmPlay(BGM_Stage_1);
        }
        else if (scene.name == "IntroScene")
        {
            BgmPlay(BGM_Home);
        }
        else
        {
            if (bgmAudioSource.clip != BGM_Home)
            {
                BgmPlay(BGM_Home);
            }
        }
    }

    private void BgmPlay(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0.5f;
        bgmAudioSource.Play();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
