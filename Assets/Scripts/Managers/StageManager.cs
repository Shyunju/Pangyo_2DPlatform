using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageSelectManager : MonoBehaviour
{
    [SerializeField] GameObject[] stageButtons;
    private int currentMapIndex = 0;

    void Start()
    {
        ShowStageMap();
    }

    public void BtnNext()
    {
        currentMapIndex++;
        if (currentMapIndex >= stageButtons.Length)
        {
            currentMapIndex = 0;
        }

        SoundManager.instance.PlayClickEffect();
        ShowStageMap();
    }

    public void BtnPrior()
    {
        currentMapIndex--;
        if (currentMapIndex < 0)
        {
            currentMapIndex = stageButtons.Length - 1;
        }

        SoundManager.instance.PlayClickEffect();
        ShowStageMap();
    }

    void ShowStageMap()
    {
        foreach (GameObject button in stageButtons)
        {
            button.SetActive(false);
        }

        stageButtons[currentMapIndex].SetActive(true);
    }

    public void OnBackButtonClick()
    {
        SoundManager.instance.PlayClickEffect();
        SceneManager.LoadScene("StartScene");
    }


    public void OnSelectStage(int stageNumber)
    {
        SoundManager.instance.PlayClickEffect();
        PlayerPrefs.SetInt("SelectedStage", stageNumber);
        SceneManager.LoadScene("GameScene");
    }
}