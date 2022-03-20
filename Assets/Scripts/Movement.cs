using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector2 movementInput;
    Vector3 movement;
    bool isRunning = false;

    [Range(1,10)]
    [SerializeField] float movementSpeed = 3f;
    [Range(1.5f, 5f)]
    [SerializeField]float runSpeedMultiplier = 2f;


    void Update()
    {
        transform.position +=
        movement * Time.deltaTime * (isRunning && movement.z > 0 ? movementSpeed * runSpeedMultiplier : movementSpeed);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log("OnMoveInput: " + context.phase + ": " + movementInput);
        movement.x = movementInput.x;
        movement.z = movementInput.y;
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {   
        if(context.performed){
            isRunning = !isRunning;
            Debug.Log(isRunning);
        }
    }
}
