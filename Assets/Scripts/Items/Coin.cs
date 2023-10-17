using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickupItem
{
    [SerializeField] int Money = 10;

    protected override void OnPickedUp(GameObject receiver)
    {
        Debug.Log("Money");
    }
}
