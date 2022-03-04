using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameSession : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float loadDelay = 4f;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;
    AudioSource audioSource;
    public bool hasWon = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!player)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }
    public void HandleLose()
    {
        audioSource.Stop();
        audioSource.clip = loseSound;
        audioSource.Play();
        player.isAlive = false;
        player.GetComponentInChildren<ParticleSystem>().Stop();
        StartCoroutine(LoadLoseScreen());
    }

    private IEnumerator LoadLoseScreen()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene("LoseScene");
    }

    public void HandleWin()
    {
        hasWon = true;
        audioSource.Stop();
        audioSource.clip = winSound;
        audioSource.Play();
        StartCoroutine(LoadWinScene());
    }
    public IEnumerator LoadWinScene()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene("WinScene");
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
