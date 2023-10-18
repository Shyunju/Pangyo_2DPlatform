using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Items;
    
    // Start is called before the first frame update
    void Start()
    {
        int num = GameManager.instance._items.Length;
        CheckItem();
    }

    public void CheckItem()
    {
        for(int i = 0; i < _Items.Length; i++)
        {
            if (GameManager.instance._items[i])
            {
                _Items[i].SetActive(true);
            }
        }
    }
}
