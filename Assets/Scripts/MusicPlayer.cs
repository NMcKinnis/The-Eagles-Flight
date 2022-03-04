using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip loseMusic;
    AudioSource audioSource;
    private void Awake()
    {

      int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if(numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(levelMusic, Camera.main.transform.position);
    }

    public void WinMusic()
    {
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(winMusic, Camera.main.transform.position);
    }   
    public void LoseMusic()
    {
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(loseMusic, Camera.main.transform.position);
    }
}
