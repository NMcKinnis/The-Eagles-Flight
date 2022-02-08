using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
   [SerializeField] PlayerController player;
   [SerializeField] float loadDelay = 1f;

    private void Start()
    {
        if (!player)
        {
            player = FindObjectOfType<PlayerController>();
        }
         
    }
    public void HandleLose()
    {
        player.isAlive = false;
        StartCoroutine(ReloadLevel());
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(loadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
   void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex +1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
