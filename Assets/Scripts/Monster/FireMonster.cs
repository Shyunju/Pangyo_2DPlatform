using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        //bullet = GetComponent<GameObject>();
        //spawner = GetComponent<GameObject>();
        MakeBullet();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MakeBullet()
    {
        Debug.Log("»ý¼º");
        Instantiate(bullet, spawner.transform.position, spawner.transform.rotation);
        Invoke(nameof(MakeBullet), monsterSO.delay);
    }
}
