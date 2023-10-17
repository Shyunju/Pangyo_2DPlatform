using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats baseStats;
    public PlayerStats CurrentStates { get; private set; }
    public List<PlayerStats> statsModifiers = new List<PlayerStats>();

    private void Awake()
    {
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        CurrentStates = new PlayerStats();

        CurrentStates.playerType = baseStats.playerType;
        CurrentStates.speed = baseStats.speed;
    }

    public void AddStatModifier(PlayerStats statModifier)
    {
        statsModifiers.Add(statModifier);
        //UpdatePlayerStats();
        baseStats.playerType = statModifier.playerType;
        baseStats.speed = statModifier.speed;
    }
}
