using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject damagedEffects;
    GameSession gameSession;
    bool hasBeenHit = false;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Die()
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

    private void ProcessEnemyDeath()
    {
        Destroy(gameObject);
    }



    public void TakeDamage()
    {
        if (!hasBeenHit)
        {
            if (tag == "Player")
            {
                damagedEffects.GetComponent<DamagedEffects>().SetEffectsPosition();
            } 
            damagedEffects.SetActive(true);
            health--;
            hasBeenHit = true;
            StartCoroutine(DelayEmissionDisable());
            StartCoroutine(DelayCanBeHurt());
            if (health <= 0)
            {
                Die();
            }
        }

    }

    IEnumerator DelayCanBeHurt()
    {
        yield return new WaitForSeconds(1f);
        hasBeenHit = false;
    }
    IEnumerator DelayEmissionDisable()
    {
        yield return new WaitForSeconds(3f);
        damagedEffects.SetActive(false);

    }

    void ProcessPlayerDeath()
    {
        gameSession.HandleLose();
    }
}
