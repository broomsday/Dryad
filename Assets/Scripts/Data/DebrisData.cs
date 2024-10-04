using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebrisData", menuName = "ScriptableObjects/DebrisData", order = 1)]
public class DebrisData : ScriptableObject
{
    public List<GameObject> debrisStages;
    public int currentStage;
    public List<ItemData> recycleItems;
    public List<float> recycleWork;
}