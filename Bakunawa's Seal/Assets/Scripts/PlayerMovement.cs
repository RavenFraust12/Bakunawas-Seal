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

    /*private void Update()
    {
        // Check if the player has been spawned and components initialized
        if (!playerInitialized)
        {
            //FindPlayerComponents();
        }

        // Only proceed if player and AnimationManager are initialized and found
        if (_joystick == null || player == null || animationManager == null)
        {
            Debug.LogError("Player or AnimationManager is null. Cannot trigger walk animation.");
            return;
        }

        if (_cameraScript.charSelected == 4 /*|| player.GetComponent<CharStats>().isDead == true) return;

        //HandleMovementAndAnimation();
    }*/
    private void FixedUpdate()
    {
        if (_joystick == null) return;
        if (_cameraScript.charSelected == 4 ||
            _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().isDead == true) return;

            if (_cameraScript.playerUnits[_cameraScript.charSelected] != null)
            {
                _rigidbody = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<Rigidbody>();
                animationManager = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponentInChildren<AnimationManager>();
        }

            _moveSpeed = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().currentMovespeed;

            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0 && animationManager != null)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
                animationManager.PlayWalk();
                //_animator.SetBool("isRunning", true);
            }
            else
            {
                //_animator.SetBool("isRunning", false);
                animationManager.PlayIdle();
            }
    }
    /*public void FaceTarget()
    {
        if (currentTarget == null) return;  // Ensure there's a valid target

        Vector3 direction = (currentTarget.position - _rigidbody.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _rigidbody.transform.rotation = Quaternion.Slerp(_rigidbody.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    /*public void HandleMovementAndAnimation()
    {
        // Set the player's velocity based on joystick input
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _moveSpeed = player.GetComponent<CharStats>().currentMovespeed;

            Vector3 movement = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
            _rigidbody.velocity = movement;

            Debug.Log("Triggering Walk Animation in PlayerMovement");
            animationManager.PlayWalk();  // Trigger the Walk animation

            // If there is a target (enemy), maintain rotation towards the target during movement
            if (currentTarget != null)
            {
                Debug.Log("Player is facing the enemy during movement.");
                FaceTarget();  // Keep facing the enemy
            }
            else
            {
                // Rotate the player to face the direction of movement if no target
                player.transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
        }
        else
        {
            Debug.Log("Triggering Idle Animation in PlayerMovement");
            animationManager.PlayIdle();  // Trigger the Idle animation
        }
    }*/
    // This method can be called from PlayerAI when the player takes control
    /*public void SetTarget(Transform target)
    {
        currentTarget = target;
    }*/
    // Method to rotate and face the target (enemy)
    

    // Method to find the player object and its components after spawning
    /*private void FindPlayerComponents()
    {
        // Try to find the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Search for AnimationManager and Rigidbody in the player or its children
            animationManager = player.GetComponentInChildren<AnimationManager>();
            _rigidbody = player.GetComponentInChildren<Rigidbody>();

            if (animationManager != null && _rigidbody != null)
            {
                playerInitialized = true; // Set the flag to indicate the player is initialized
                Debug.Log("Player and components successfully initialized.");
            }
            else
            {
                Debug.LogError("Failed to initialize player components.");
            }
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }*/
}





//originalscript
//[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
//public class PlayerMovement : MonoBehaviour
//{
//    public Rigidbody _rigidbody;
//    [Header("Joystick Holder")]
//    [SerializeField] private FixedJoystick _joystick; //palitan mo nlng FixedJoystick kung di mo trip

//    //[SerializeField] private Animator _animator;
//    [SerializeField] private float _moveSpeed;

//    private CameraScript _cameraScript;

//    private void Awake()
//    {
//        _cameraScript = FindObjectOfType<CameraScript>();
//        // Find the joystick in the scene if it's not assigned in the Inspector
//        if (_joystick == null)
//        {
//            _joystick = FindObjectOfType<FixedJoystick>(); // Find the joystick by type in the scene
//            if (_joystick == null)
//            {
//                Debug.LogError("FixedJoystick not found in the scene.");
//            }
//        }
//    }
//    private void FixedUpdate()
//    {
//        if (_joystick == null) return;
//        if (_cameraScript.charSelected == 4 ||
//            _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().isDead == true) return;

//        if (_cameraScript.playerUnits[_cameraScript.charSelected] != null)
//        {
//            _rigidbody = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<Rigidbody>();
//        }

//        _moveSpeed = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().currentMovespeed;

//        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

//        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
//        {
//            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
//            //_animator.SetBool("isRunning", true);
//        }
//        else
//        {
//            //_animator.SetBool("isRunning", false);
//        }
//    }
//}

