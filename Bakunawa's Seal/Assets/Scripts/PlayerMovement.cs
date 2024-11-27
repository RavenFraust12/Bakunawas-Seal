using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    //public GameObject player; // Reference to the Player GameObject
    public Rigidbody _rigidbody;

    [Header("Joystick Holder")]
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;
    public Transform currentTarget; // Reference to the enemy the player was attacking

    private CameraScript _cameraScript;
    public AnimationManager animationManager; // Reference to AnimationManager
    //private bool playerInitialized = false; // Flag to check if the player has been initialized

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    public bool isMoving;
    public bool isKeyboard;

    private void Awake()
    {
        _cameraScript = FindObjectOfType<CameraScript>();

        // Find the joystick in the scene if it's not assigned in the Inspector
        if (_joystick == null)
        {
            _joystick = FindObjectOfType<FixedJoystick>(); // Find the joystick by type in the scene
            if (_joystick == null)
            {
                Debug.LogError("FixedJoystick not found in the scene.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (_joystick == null) return;
        if (_cameraScript.charSelected == 4 || _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().isDead == true) return;

        if (_cameraScript.playerUnits[_cameraScript.charSelected] != null)
        {
            _rigidbody = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<Rigidbody>();
            animationManager = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponentInChildren<AnimationManager>();
        }

        _moveSpeed = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().currentMovespeed;

        // Calculate movement velocity from the joystick input
        //Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        // Determine the move direction based on the input method
        Vector3 moveDirection;

        if (isKeyboard)
        {
            // Using WASD input for debugging
            float horizontal = 0f;
            float vertical = 0f;

            // Check for WASD inputs (assuming these are mapped to horizontal and vertical axes)
            if (Input.GetKey(KeyCode.W)) vertical = 1f; // Move forward
            if (Input.GetKey(KeyCode.S)) vertical = -1f; // Move backward
            if (Input.GetKey(KeyCode.A)) horizontal = -1f; // Move left
            if (Input.GetKey(KeyCode.D)) horizontal = 1f; // Move right

            moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        }
        else
        {
            // Using joystick input
            moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
        }

        // Apply velocity to the rigidbody
        _rigidbody.velocity = new Vector3(moveDirection.x * _moveSpeed, _rigidbody.velocity.y, moveDirection.z * _moveSpeed);

        // If the player is moving, rotate to face the movement direction
        if (moveDirection.magnitude > 0f && animationManager != null)
        {
            // Smooth rotation towards the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _cameraScript.playerUnits[_cameraScript.charSelected].transform.rotation = Quaternion.Slerp(_cameraScript.playerUnits[_cameraScript.charSelected].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Debug.Log("Player is controlled and moving");
            // Play walking animation
            animationManager.PlayWalk();
            isMoving = true;
        }
        else
        {
            // If no movement, play idle animation
            animationManager.PlayIdle();
            isMoving = false;
        }
    }
}