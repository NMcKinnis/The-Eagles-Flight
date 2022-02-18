using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health health;
  
    private void Start()
    {
        health = GetComponent<Health>();
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!health.hasBeenHit)
        {
            ProcessHit(other);
        }
    }

    private void ProcessHit(GameObject other)
    {
        Debug.Log($"{name}I'm hit! by {other.gameObject.name}");
        health.TakeDamage();
    }
}
