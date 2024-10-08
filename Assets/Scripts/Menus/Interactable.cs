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
