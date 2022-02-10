using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float timeUntilDestroyed = 2f;
    float currentTime = 0f;


    private void Start()
    {
        Destroy(gameObject, timeUntilDestroyed);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (timeUntilDestroyed <= currentTime)
        {
            Destroy(gameObject);
        }
    }
}
