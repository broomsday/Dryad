using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public float moveSpeed;
    public float turnSpeed;
    public float jumpPower;
    public float maxJumpDuration;
    public float gravity;
    public float groundingForce;
}