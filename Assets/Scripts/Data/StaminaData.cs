using UnityEngine;

[CreateAssetMenu(fileName = "StaminaData", menuName = "ScriptableObjects/StaminaData", order = 1)]
public class StaminaData : ScriptableObject
{
    public float maxStamina;
    public float regenStamina;
    public float currentStamina;
}
