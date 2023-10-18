using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject How;

    public void OnHow()
    {
        SoundManager.instance.PlayClickEffect();
        How.SetActive(true);
    }

    public void OffHow()
    {
        SoundManager.instance.PlayClickEffect();
        How.SetActive(false);
    }

    public void MoveToStageSelectScene()
    {
        SoundManager.instance.PlayClickEffect();
        SceneManager.LoadScene("StageScene");
    }
}
