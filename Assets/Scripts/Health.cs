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
    [SerializeField] Transform spawnAtRuntimeParent;
    GameSession gameSession;
    public bool hasBeenHit = false;
    Scoreboard scoreboard;
    [SerializeField] HealthBar healthbar;
    public int currentHealth;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        currentHealth = health;
        if (healthbar)
        {
            healthbar.SetMaxHealth(currentHealth);
        }

        {
            scoreboard = FindObjectOfType<Scoreboard>();
        }
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
        vfx.transform.parent = spawnAtRuntimeParent;
        Destroy(gameObject);
    }



    public void TakeDamage()
    {
        if (!hasBeenHit)
        {
            damagedEffects.GetComponent<DamagedEffects>().SetEffectsPosition();
            damagedEffects.Play();
            currentHealth--;
            if (healthbar)
            {
            healthbar.SetCurrentHealth(currentHealth);
            }
            hasBeenHit = true;
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
