using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //GameManager.Instance.GetItem();

    private GameObject _player;
    private GameObject _endPanel;
    private GameObject _itemUI;
    private PlayerAction _playerReset;
    private ItemUIManager _itemUIManager;

    private int index = -1;
    public bool[] _items = new bool[3];

    private void Awake()
    {
        //instance = this;
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
    }
    void Start()
    {
        
        
    }
    public void SetPlayer(GameObject player)
    {
        _player = player;
        _playerReset = _player.GetComponent<PlayerAction>();
    }

    public void SetGameOver(GameObject panel)
    {
        _endPanel = panel;
        _endPanel.SetActive(false);
    }
    public void SetItemUI(GameObject panel)
    {
        _itemUI = panel;
        _itemUIManager = _itemUI.GetComponent<ItemUIManager>();
    }

    private void Reset()
    {
        _playerReset.SetNomalState();
    }

    public void GetItem()
    {
        _items[++index] = true;
        _itemUIManager.CheckItem();
        StartCoroutine(nameof(GetCo));
        if( index == 2)
        {
            SceneManager.LoadScene("EndScene");  //게임전체클리어
        }
        else
        {
            SceneManager.LoadScene("StageScene"); //스테이지 클리어
            Reset(); //문제 생기면 여기임
        }
    }

    IEnumerable GetCo()
    {
        yield return new WaitForSecondsRealtime(2f);
    }

    public void GameOver()
    {
        _endPanel.SetActive(true);
        SoundManager.instance.PlayGameOver();
    }
}
