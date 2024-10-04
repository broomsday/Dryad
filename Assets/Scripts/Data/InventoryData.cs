using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptableObjects/InventoryData", order = 1)]
public class InventoryData : ScriptableObject
{
    public string inventoryName;
    public List<ItemData> items;
}