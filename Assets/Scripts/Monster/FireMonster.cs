using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    [SerializeField] private MonsterSO monsterSO;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject bullet;

    void Start()
    {
        MakeBullet();
    }

    void MakeBullet()
    {
        Instantiate(bullet, spawner.transform.position, spawner.transform.rotation);
        Invoke(nameof(MakeBullet), monsterSO.delay);
    }
}
