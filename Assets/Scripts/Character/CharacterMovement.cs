using UnityEngine;
using Utilities;

public class CharacterMovement : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool isGrounded;

    [SerializeField] MoveData moveData;

    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        isGrounded = characterController.isGrounded;
    }

    public void HandleMovement(Vector2 moveInputVector)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.right * moveInputVector.x, moveData.turnSpeed * Mathf.Abs(moveInputVector.x) * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        Vector3 horizontalMoveVector = transform.forward * moveData.moveSpeed * moveInputVector.y;
        playerVelocity.x = horizontalMoveVector.x;
        playerVelocity.z = horizontalMoveVector.z;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void HandleJumping(CountdownTimer jumpTimer)
    {
        if(jumpTimer.IsRunning)
        {
            playerVelocity.y = moveData.jumpPower;
        }
        else if(characterController.isGrounded)
        {
            playerVelocity.y = -moveData.groundingForce;
        }
        else if(!characterController.isGrounded) 
        {
            playerVelocity.y -= moveData.gravity * Time.deltaTime;
        }
    }

}
