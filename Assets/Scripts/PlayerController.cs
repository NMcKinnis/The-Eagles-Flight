using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast player moves based off of input")] [SerializeField] float controlSpeed = 1f;
    [SerializeField] float xRange = 4f;
    [SerializeField] float yRange = 2.5f;
    [SerializeField] float yRangeOffset = -.8f;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float controlRollFactor = -25f;
    [Header("Player Input")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [Header("Firing Particle Effects")]
    [SerializeField] GameObject[] firingEffects;

    float xInputMagnitude, yInputMagnitude;
    public bool isAlive = true;
    bool hasFired = false;

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        if (!isAlive) {return;}
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        xInputMagnitude = movement.ReadValue<Vector2>().x;
        yInputMagnitude = movement.ReadValue<Vector2>().y;

        float xOffset = xInputMagnitude * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yInputMagnitude * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange - yRangeOffset, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yInputMagnitude * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xInputMagnitude * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5 && isAlive)
        {
            hasFired = false;
            StartCoroutine(DelayEmissionDisable());
            SetLasersActive(true);
        }
        else if (hasFired)
        {
            SetLasersActive(false);
        }

    }
    IEnumerator DelayEmissionDisable()
    {
        yield return new WaitForSeconds(0.3f);

        if (fire.ReadValue<float>() < 0.5)
        { hasFired = true; }

    }


    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject effect in firingEffects)
        {
            var emissionModule = effect.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
