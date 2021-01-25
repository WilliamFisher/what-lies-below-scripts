using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemPickup : MonoBehaviour
{
    public Transform pickupTransfom;

    private GameObject currentPickup;

    public void Pickup(GameObject pickup)
    {
        if (currentPickup != null)
        {
            DropCurrent();
        }
        currentPickup = pickup;
        currentPickup.layer = 2;
        currentPickup.tag = "Untagged";
        currentPickup.GetComponent<Rigidbody>().isKinematic = true;
        currentPickup.transform.SetParent(pickupTransfom);
        currentPickup.transform.position = pickupTransfom.position;
    }

    public void DropCurrent()
    {
        pickupTransfom.transform.DetachChildren();
        currentPickup.layer = 0;
        currentPickup.tag = "Interactable";
        currentPickup.GetComponent<Rigidbody>().isKinematic = false;
        currentPickup = null;
    }
}
