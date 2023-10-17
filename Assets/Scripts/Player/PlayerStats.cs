using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerType
{
    Type1,
    Type2,
    Type3,
}

[Serializable]
public class PlayerStats
{
    public PlayerType playerType;
    [Range(1f, 20f)] public float speed;
}
