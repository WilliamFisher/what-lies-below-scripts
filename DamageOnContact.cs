using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.GetComponent<CharacterStats>().TakeDamage(15);
        }
        catch (Exception)
        {
            Debug.LogWarning("Could not get the character data.");
        }
    }
}
