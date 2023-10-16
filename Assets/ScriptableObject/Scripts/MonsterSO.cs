using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterSo/MonsterSo", order = int.MaxValue)]
public class MonsterSO : ScriptableObject
{
    public float speed;
    public bool isWalking;
    public float delay;
}
