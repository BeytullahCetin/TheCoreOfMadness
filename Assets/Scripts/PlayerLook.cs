using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    Vector2 lookInput;
    
    [Range(0,90)]
    [SerializeField] float xClamp = 90f;
    float xRotation;
    Vector3 targetRotation;

    [SerializeField] Transform camTransform;
    [Range(1, 10)]
    [SerializeField] float lookSpeed = 1f;


    void Update()
    {
        transform.Rotate(Vector3.up, lookInput.x);

        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        camTransform.eulerAngles = targetRotation;

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>() * lookSpeed * Time.deltaTime;
    }
}
