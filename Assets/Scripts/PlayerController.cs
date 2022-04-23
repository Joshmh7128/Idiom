using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // script handles movement of the player
    [Header("Movement")]
    Vector3 moveH, moveV, move;
    [SerializeField] Transform playerHead;
    [SerializeField] CharacterController characterController; // our character controller
    [SerializeField] float moveSpeed, gravity, jumpVelocity; // set in editor for controlling
    float playerJumpVelocity, gravityValue, verticalVelocity; // hidden because is calculated

    [Header("Camera")]
    [SerializeField] float aimSensitivity, minYAngle, maxYAngle;
    float currentSensitivity, yRotate, xRotate;

    // Update is called once per frame
    void Update()
    {
        // declare our motion
        float pAxisV = Input.GetAxis("Vertical");
        float pAxisH = Input.GetAxis("Horizontal");
        moveV = playerHead.forward * pAxisV;
        moveH = playerHead.right * pAxisH;

        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            playerJumpVelocity = Mathf.Sqrt(jumpVelocity * -3.0f * gravity);
        }

        // movement application
        // jump calculations
        gravityValue = gravity;
        playerJumpVelocity += gravityValue * Time.deltaTime;
        // verticalJumpPadVelocity = playerJumpPadVelocity += gravityValue * Time.deltaTime;
        verticalVelocity = playerJumpVelocity;
        move = new Vector3((moveH.x + moveV.x), verticalVelocity / moveSpeed, (moveH.z + moveV.z));

        // our camera control
        currentSensitivity = aimSensitivity;
        // run math to rotate the head of the player as we move the mouse
        yRotate += (Input.GetAxis("Mouse Y") * -currentSensitivity * Time.fixedDeltaTime);
        // clamp the rotation so we don't go around ourselves
        yRotate = Mathf.Clamp(yRotate, minYAngle, maxYAngle);
        // calculate our X rotation
        xRotate += (Input.GetAxis("Mouse Y") * -currentSensitivity * Time.fixedDeltaTime);
        // add in our rotate mods if we have any
        float finalxRotate = xRotate;
        float finalyRotate = yRotate;

        Mathf.SmoothStep(xRotate, finalxRotate, 5 * Time.deltaTime);
        Mathf.SmoothStep(yRotate, finalyRotate, 5 * Time.deltaTime);

        // apply it to our torso
        playerHead.eulerAngles = new Vector3(finalyRotate, finalxRotate, 0f);

    }
}
