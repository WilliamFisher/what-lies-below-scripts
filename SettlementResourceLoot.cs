using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementResourceLoot : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SettlementResourceData resource;

    public string GetInteractText()
    {
        return "Press E to pick up.";
    }

    public void Interact(GameObject interactingObject)
    {
        //Make character pickup resource
        CharacterItemPickup characterPickup = interactingObject.GetComponent<CharacterItemPickup>();
        characterPickup.Pickup(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DropZone")
        {
            try
            {
                gameObject.GetComponentInParent<CharacterItemPickup>().DropCurrent();
                ResourceDropoffHandler dropHandler = other.GetComponent<ResourceDropoffHandler>();
                dropHandler.AddResource(resource);
                Destroy(gameObject);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("No component found.");
            }
        }
    }
}
