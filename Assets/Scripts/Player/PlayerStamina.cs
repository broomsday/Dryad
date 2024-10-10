using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] public StaminaData staminaData;

    public void ModifyStamina(float staminaModifier)
    {
        staminaData.currentStamina = Mathf.Clamp(staminaData.currentStamina + staminaModifier, 0f, staminaData.maxStamina);
    }
}
