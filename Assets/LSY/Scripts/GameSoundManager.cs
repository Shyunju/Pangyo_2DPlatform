using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public AudioSource GameBgmPlayer;


    public void BgmSetMusicVolume(float volume)
    {
        GameBgmPlayer.volume = volume;
    }
   
}