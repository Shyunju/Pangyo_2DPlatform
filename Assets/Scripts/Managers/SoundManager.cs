using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip BGM_Start;
    [SerializeField] private AudioClip BGM_Stage_Select;
    [SerializeField] private AudioClip BGM_Stage_1;
    [SerializeField] private AudioClip BGM_Stage_2;
    [SerializeField] private AudioClip BGM_Stage_3;
    [SerializeField] private AudioClip BGM_End;

    public AudioClip clickEffect;
    public AudioClip playerShotEffect;

    [SerializeField] private AudioClip stageClear;
    [SerializeField] private AudioClip gameOver;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider audioSlider;

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
        if (currentScene.name == "StartScene")
        {
            BgmPlay(BGM_Start);
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

    public void PlayerShotEffect()
    {
        effectAudioSource.PlayOneShot(playerShotEffect);
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
        if (scene.name == "StageScene")
        {
            BgmPlay(BGM_Stage_Select);
        }
        else if (scene.name == "StartScene")
        {
            BgmPlay(BGM_Start);
        }
        else if (scene.name == "GameScene")
        {
            audioSlider = GameObject.FindGameObjectWithTag("SoundSlider").GetComponent<Slider>();

            audioSlider.onValueChanged.AddListener(AudioControl);

            int selectedStage = PlayerPrefs.GetInt("SelectedStage");

            switch (selectedStage)
            {
                case 1:
                    BgmPlay(BGM_Stage_1);
                    break;
                case 2:
                    BgmPlay(BGM_Stage_2);
                    break;
                case 3:
                    BgmPlay(BGM_Stage_3);
                    break;
            }
        }
        else if (scene.name == "EndScene")
        {
            BgmPlay(BGM_End);
        }
        else
        {
            if (bgmAudioSource.clip != BGM_Start)
            {
                BgmPlay(BGM_Start);
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

    public void AudioControl(float volume)
    {
        masterMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}
