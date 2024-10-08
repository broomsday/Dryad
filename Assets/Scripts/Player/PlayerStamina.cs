using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] public StaminaData staminaData;

    void Update()
    {
        staminaData.currentStamina = Mathf.Clamp(staminaData.currentStamina + staminaData.regenStamina * Time.deltaTime, 0f, staminaData.maxStamina);
    }

    public void ConsumeStamina(float staminaConsumed)
    {
        staminaData.currentStamina = Mathf.Clamp(staminaData.currentStamina - staminaConsumed, 0f, staminaData.maxStamina);
    }
}
