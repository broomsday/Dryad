using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private ChatData chatData;
    [SerializeField] private ActionData actionData;
    [SerializeField] private Collider interactionCollider;

    // TODO: should have different classes:
    //      Harvestable
    //      Chatable
    //      Actionable
    //      Openable
    //  All the above can just inherit from Interactable
    //      Then each one can have it's own type of data it wants
    //      But things like the collider logic remain in one place

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetInPlayerRange(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SetInPlayerRange(false);
        }
    }

    private void SetInPlayerRange(bool inplayerRange)
    {
        if (inplayerRange) {
            SetHighLightLevel(1);
        }
        else {
            SetHighLightLevel(0);
        }
    }

    private void SetHighLightLevel(int level)
    {
        // TODO: change the materials highlight level
    }

    public void SetAsPlayerInteractable(bool isPlayerInteractable)
    {
        if (isPlayerInteractable) {
            SetHighLightLevel(2);
        }
    }
}
