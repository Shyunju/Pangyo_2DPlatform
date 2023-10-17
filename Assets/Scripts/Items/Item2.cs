using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : PickupItem
{
    [SerializeField] private List<PlayerStats> statsModifier;
    protected override void OnPickedUp(GameObject receiver)
    {
        PlayerStatsHandler statsHandler = receiver.GetComponent<PlayerStatsHandler>();
        foreach (PlayerStats stat in statsModifier)
        {
            statsHandler.AddStatModifierType(stat);
        }
    }
}
