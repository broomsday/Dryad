using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "ScriptableObjects/ActionData", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;
    public float staminaCost;
}