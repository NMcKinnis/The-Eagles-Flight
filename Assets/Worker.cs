using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] ParticleSystem scanner;
    [SerializeField] float timeBetweenScans = 1f;
    [SerializeField] float timeSpentScanning = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScanObject()
    {
        StartCoroutine(StartScanDelay());
        scanner.Play();
        StartCoroutine(ScanTime());
        scanner.Stop();
    }

    IEnumerator ScanTime()
    {
        yield return new WaitForSeconds(timeSpentScanning);
    }

    IEnumerator StartScanDelay()
    {
        yield return new WaitForSeconds(timeBetweenScans);
    }
}
