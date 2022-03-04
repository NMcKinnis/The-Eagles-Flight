using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMusicPlayer : MonoBehaviour
{
    MusicPlayer musicPlayer;
    private void Awake()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
         Destroy(musicPlayer.gameObject);
        }

    }
}
