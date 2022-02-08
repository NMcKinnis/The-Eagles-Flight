using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 30f;
    [SerializeField] float lifeSpan = 3f;
    Rigidbody myRigidbody;
    private void Awake()
    {
       myRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        myRigidbody.AddRelativeForce(myRigidbody.transform.forward * projectileSpeed);
        Destroy(gameObject, lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
