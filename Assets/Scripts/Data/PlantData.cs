using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "ScriptableObjects/PlantData", order = 1)]
public class PlantData : ScriptableObject
{
    public List<GameObject> plantStages;
    public int currentStage;
    public List<ItemData> harvestItems;
    public List<float> harvestTimes;
}