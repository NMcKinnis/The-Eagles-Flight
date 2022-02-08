using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
        Debug.Log($"{name}I'm hit! by {other.gameObject.name}");
        if (health)
        {
            health.TakeDamage();
        }
    }
}
