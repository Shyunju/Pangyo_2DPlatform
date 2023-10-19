using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _ItemsSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetItemUI(gameObject);

        int num = GameManager.instance._items.Length;
        CheckItem();
    }

    public void CheckItem()
    {
        for(int i = 0; i < _ItemsSprite.Length; i++)
        {
            if (GameManager.instance._items[i])
            {
                _ItemsSprite[i].SetActive(true);
            }
        }
    }
}
