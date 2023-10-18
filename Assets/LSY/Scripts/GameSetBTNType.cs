using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameSetBTNType : MonoBehaviour
{
    public GameSetButtonType setcurrentType;

    public CanvasGroup SoundGroup;
    public CanvasGroup SettingGroup;

    public void OnBtnClick()
    {
        switch (setcurrentType)
        {
            case GameSetButtonType.Play:
                SettingGroupOff(SettingGroup);
                Time.timeScale = 1f;
                break;

            case GameSetButtonType.Home:
                Debug.Log("스타트 씬으로");
                //SceneManager.LoadScene("StartScene");
                break;

            case GameSetButtonType.StageMenu:
                SceneManager.LoadScene("StageScene");
                break;

            case GameSetButtonType.Sound:
                SettingGroupOn(SoundGroup);
                SettingGroupOff(SettingGroup);
                break;

            case GameSetButtonType.Back:
                SettingGroupOff(SoundGroup);
                SettingGroupOn(SettingGroup);
                break;

            case GameSetButtonType.Setting:
                SettingGroupOn(SettingGroup);
                Time.timeScale = 0f;
                break;
        }
    }

    public void SettingGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void SettingGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

}
