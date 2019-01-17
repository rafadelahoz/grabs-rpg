using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool controlledByGamepad = false;

    Vector3 cameraForward;
    Vector3 movementVector;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // TODO: Allow player to remap later
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            controlledByGamepad = !controlledByGamepad;
            currentClickTarget = transform.position;
        }

        if (controlledByGamepad)
        {
            HandleDirectMovement();
        }
        else
        {
            HandleMouseMovement();
        }
    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;
                case Layer.Enemy:
                    print("Not moving to enemy!");
                    break;
                default:
                    print("Unkwon destination?");
                    break;
            }
        }

        Vector3 clickedDestination = currentClickTarget - transform.position;
        if (clickedDestination.magnitude >= walkMoveStopRadius)
        {
            m_Character.Move(clickedDestination, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    private void HandleDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        movementVector = v * cameraForward + h * Camera.main.transform.right;

        m_Character.Move(movementVector, false, false);
    }
}

