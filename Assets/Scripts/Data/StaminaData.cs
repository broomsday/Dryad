using UnityEngine;

[CreateAssetMenu(fileName = "StaminaData", menuName = "ScriptableObjects/StaminaData", order = 1)]
public class StaminaData : ScriptableObject
{
    public float currentStamina;
    public float maxStamina;
    public float idleRegen;
    public float restRegen;
    public float moveCost;
    public float jumpCost;
}
