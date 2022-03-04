using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    private void Awake()
    {
        int numGameCanvas = FindObjectsOfType<GameCanvas>().Length;
        if (numGameCanvas > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
