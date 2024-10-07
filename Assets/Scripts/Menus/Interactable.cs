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
            Debug.Log($"Highlighting {gameObject.name}");
        }
        else {
            Debug.Log($"Hiding {gameObject.name}");
        }
    }

    public void SetAsPlayerInteractable(bool isPlayerInteractable)
    {
        if (isPlayerInteractable) {
            Debug.Log($"{gameObject.name} is to be interacted with");
        }
    }

}
