using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int scoreValue = 10;
    [SerializeField] float canBeHurtDelay = 1f;
    [SerializeField] float hitParticleDelay = 3f;
    [SerializeField] ParticleSystem damagedEffects;
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] HealthBar healthbar;
    GameSession gameSession;
    Scoreboard scoreboard;
    GameObject spawnAtRuntimeParent;
    AudioSource audioSource;
    public bool hasBeenHit = false;
    public int currentHealth;
    public bool isAlive = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawnAtRuntimeParent = GameObject.FindWithTag("SpawnAtRuntime");
        gameSession = FindObjectOfType<GameSession>();
        currentHealth = health;
        if (healthbar)
        {
            healthbar.SetMaxHealth(currentHealth);
        }
        scoreboard = FindObjectOfType<Scoreboard>();
    }
    void Die()
    {

        isAlive = false;
        PlayDeathAudio();
        if (tag == "Player")
        {
            ProcessPlayerDeath();
            return;
        }
        if(tag == "Boss")
        {
            HandleWin();
            ProcessEnemyDeath();
            return;
        }
        else
        {
            ProcessEnemyDeath();
        }
    }

    private void PlayDeathAudio()
    {
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
    }

    private void HandleWin()
    {
        FindObjectOfType<GameSession>().HandleWin();
    }

    void ProcessEnemyDeath()
    {
        scoreboard.IncreaseScore(scoreValue);
        GameObject vfx = Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = spawnAtRuntimeParent.transform;
        Destroy(gameObject);
    }



    public void TakeDamage()
    {
        {
            audioSource.clip = hitSFX;
            if (tag == "Player")
            { damagedEffects.GetComponent<DamagedEffects>().SetEffectsPosition(); }
            damagedEffects.Play();
            currentHealth--;
            if (healthbar)
            {
                healthbar.SetCurrentHealth(currentHealth);
            }
            audioSource.Play();
            StartCoroutine(DelayEmissionDisable());
            StartCoroutine(DelayCanBeHurt());
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }




    IEnumerator DelayCanBeHurt()
    {
        yield return new WaitForSeconds(canBeHurtDelay);
        hasBeenHit = false;
    }
    IEnumerator DelayEmissionDisable()
    {
        yield return new WaitForSeconds(hitParticleDelay);
        damagedEffects.Stop();

    }

    void ProcessPlayerDeath()
    {
        gameSession.HandleLose();
    }
}
