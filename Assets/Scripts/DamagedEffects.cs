using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedEffects : MonoBehaviour
{
    [SerializeField] GameObject character;
    public void SetEffectsPosition()
    {
        if (character)
        {
            transform.localPosition = new Vector3
                (character.transform.localPosition.x
                ,character.transform.localPosition.y
                ,transform.localPosition.z);
        }
    }
}
