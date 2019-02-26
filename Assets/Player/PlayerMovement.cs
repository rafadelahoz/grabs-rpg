using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 2f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
    Vector3 clickedPoint;

    bool controlledByGamepad = false;

    Vector3 cameraForward;
    Vector3 movementVector;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // TODO: Allow player to remap later
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            controlledByGamepad = !controlledByGamepad;
            currentDestination = transform.position;
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
            clickedPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickedPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickedPoint, attackMoveStopRadius);
                    break;
                default:
                    print("Unkwon destination?");
                    break;
            }
        }

        WalkToDestination();
    }

    private void WalkToDestination()
    {
        Vector3 clickedDestination = currentDestination - transform.position;
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

    private Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        // Some lines
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickedPoint, 0.2f);

        // Sphere?
        // Gizmos.color = new Color(1, 0, 0, 0.4f);
        // Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

