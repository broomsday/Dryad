using UnityEngine;

[CreateAssetMenu(fileName = "MoveData", menuName = "ScriptableObjects/MoveData", order = 1)]
public class MoveData : ScriptableObject
{
    public float moveSpeed;
    public float turnSpeed;
    public float jumpPower;
    public float maxJumpDuration;
    public float gravity;
    public float groundingForce;
}