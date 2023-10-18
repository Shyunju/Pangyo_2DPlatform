using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
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

        ShowStageMap();
    }

    public void BtnPrior()
    {
        currentMapIndex--;
        if (currentMapIndex < 0)
        {
            currentMapIndex = stageButtons.Length - 1;
        }

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
        Debug.Log("Start È¨À¸·Î");
        //SceneManager.LoadScene("StartScene");
    }


    public void OnSelectStage(int stageNumber)
    {
        PlayerPrefs.SetInt("SelectedStage", stageNumber);
        SceneManager.LoadScene("GameScene");
    }
}