using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        ProcessTranslation();
    }

    private void ProcessTranslation()
    {
        transform.localPosition = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
