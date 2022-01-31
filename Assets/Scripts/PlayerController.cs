using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 1f;
    [SerializeField] float xRange = 4f;
    [SerializeField] float yRange = 2.5f;
    [SerializeField] float yRangeOffset = -.8f;
    [SerializeField] float positionPitchFactor = 1f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] InputAction movement;

    float xInputMagnitude , yInputMagnitude;
    
    private void OnEnable()
    {
        movement.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
    }
      
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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
        float pitch = pitchDueToPosition + pitchDueToControl ;
        float yaw = transform.localPosition.x * positionPitchFactor;
        float roll = xInputMagnitude * controlPitchFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
