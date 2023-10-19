using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject stage1GameObject;
    public GameObject stage2GameObject;
    public GameObject stage3GameObject;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject diePopup;

    private void Awake()
    {
        itemUI.SetActive(true);
        diePopup.SetActive(true);
    }
    private void Start()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage");
        

        switch (selectedStage)
        {
            case 1:
                stage1GameObject.SetActive(true);
                stage2GameObject.SetActive(false);
                stage3GameObject.SetActive(false);
                break;
            case 2:
                stage1GameObject.SetActive(false);
                stage2GameObject.SetActive(true);
                stage3GameObject.SetActive(false);
                break;
            case 3:
                stage1GameObject.SetActive(false);
                stage2GameObject.SetActive(false);
                stage3GameObject.SetActive(true);
                break;

            default:
                Debug.LogError("스테이지 선택이 잘 못 되었습니다.");
                break;
        }
    }
}
