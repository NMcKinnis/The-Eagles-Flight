using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    GameObject gameCanvas;

    private void Awake()
    {
        if (gameCanvas != null)
        {
            gameCanvas = FindObjectOfType<GameCanvas>().gameObject;
        }
    }
    public void LoadMainLevel()
    {
        if (gameCanvas)
        {
            Destroy(FindObjectOfType<GameCanvas>().gameObject);
        }

        SceneManager.LoadScene("MainLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
