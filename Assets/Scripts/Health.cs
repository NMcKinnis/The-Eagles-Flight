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
    [SerializeField] HealthBar healthbar;
    GameSession gameSession;
    Scoreboard scoreboard;
    GameObject spawnAtRuntimeParent;
    public bool hasBeenHit = false;
    public int currentHealth;
    void Start()
    {
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
        if (tag == ("Player"))
        {
            ProcessPlayerDeath();
        }
        else
        {
            ProcessEnemyDeath();
        }
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
            if (tag == "Player")
            { damagedEffects.GetComponent<DamagedEffects>().SetEffectsPosition(); }
            damagedEffects.Play();
            currentHealth--;
            if (healthbar)
            {
                healthbar.SetCurrentHealth(currentHealth);
            }
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
