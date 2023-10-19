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
            SceneManager.LoadScene("EndScene");  //������üŬ����
        }
        else
        {
            SceneManager.LoadScene("StageScene"); //�������� Ŭ����
            Reset(); //���� ����� ������
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
