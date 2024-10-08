using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private MoveData moveData;
    [SerializeField] private float currentStamina;

    void Awake()
    {
        currentStamina = 0f;
    }

    void Update()
    {
        currentStamina = Mathf.Clamp(currentStamina + moveData.regenStamina * Time.deltaTime, 0f, moveData.maxStamina);
    }

    public void ConsumeStamina(float staminaConsumed)
    {
        currentStamina = Mathf.Clamp(currentStamina - staminaConsumed, 0f, moveData.maxStamina);
    }
}
