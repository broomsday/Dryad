using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private StaminaData staminaData;
    [SerializeField] private TMP_Text staminaText;

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        staminaText.text = $"Stamina: {staminaData.currentStamina:F0}";
    }
}
