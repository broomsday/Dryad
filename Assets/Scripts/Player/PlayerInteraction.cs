using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private HashSet<GameObject> interactablesInRange = new HashSet<GameObject>();
    private GameObject interactionObject = null;
    private GameObject interactionObjectPrevious = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable") {
            interactablesInRange.Add(other.gameObject);
            // TODO: subscribe to learning if this gameobject is destroyed
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable") {
            interactablesInRange.Remove(other.gameObject);
            // TODO: unsubscribe from learning if this gameobject is destroyed
        }
    }

    void Update()
    {
        AssignInteractionObject();
        CheckInteractionObjectChanged();
    }

    private void AssignInteractionObject()
    {
        interactionObjectPrevious = interactionObject;
        interactionObject = null;
        float smallestAngle = Mathf.Infinity;

        foreach (GameObject interactable in interactablesInRange)
        {
            Vector3 directionToInteractable = (interactable.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToInteractable);

            if (angle < smallestAngle)
            {
                smallestAngle = angle;
                interactionObject = interactable;
            }
        }
    }

    private void CheckInteractionObjectChanged()
    {
        if (interactionObjectPrevious != interactionObject)
            Debug.Log($"{interactionObjectPrevious} {interactionObject}");
            AssignPlayerInteractable(interactionObjectPrevious, false);
            AssignPlayerInteractable(interactionObject, true);
    }

    private void AssignPlayerInteractable(GameObject obj, bool state) {
            if (obj != null) {
                Interactable interactableScript = obj.GetComponent<Interactable>();
                interactableScript.SetAsPlayerInteractable(state);
            }
    }

    public void Interact()
    {
        if (interactionObject != null) {
            Debug.Log($"Trying to interact with {interactionObject.name}");
        }
    }
}
