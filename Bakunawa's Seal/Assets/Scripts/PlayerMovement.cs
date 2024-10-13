using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
//public class PlayerMovement : MonoBehaviour
//{
//    [SerializeField] private Rigidbody _rigidbody;
//    [Header("Joystick Holder")]
//    [SerializeField] private FixedJoystick fixedJoystick;
//    [SerializeField] private FloatingJoystick floatingJoystick;
//    [SerializeField] private DynamicJoystick dynamicJoystick;
//    [SerializeField] private VariableJoystick variableJoystick;

//    [SerializeField] private Animator _animator;
//    [SerializeField] private float _moveSpeed;

//    private int activeJoystickIndex = 0; // Variable to switch between joysticks

//    private void FixedUpdate()
//    {
//        Vector3 input = GetJoystickInput(); // Get the current joystick input

//        _rigidbody.velocity = new Vector3(input.x * _moveSpeed, _rigidbody.velocity.y, input.z * _moveSpeed);

//        if (input.x != 0 || input.z != 0)
//        {
//            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
//            _animator.SetBool("isRunning", true);
//        }
//        else
//        {
//            _animator.SetBool("isRunning", false);
//        }
//    }

//    private Vector3 GetJoystickInput()
//    {
//        Vector3 input = Vector3.zero;

//        // Switch between joysticks based on activeJoystickIndex
//        switch (activeJoystickIndex)
//        {
//            case 0: // FixedJoystick
//                input = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);
//                break;
//            case 1: // FloatingJoystick
//                input = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
//                break;
//            case 2: // DynamicJoystick
//                input = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
//                break;
//            case 3: // VariableJoystick
//                input = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
//                break;
//        }

//        return input;
//    }

//    // Method to set the active joystick dynamically
//    public void SetActiveJoystick(int index)
//    {
//        if (index >= 0 && index <= 3) // Ensure index is within bounds
//        {
//            activeJoystickIndex = index;
//        }
//        else
//        {
//            Debug.LogWarning("Joystick index out of range.");
//        }
//    }
//}
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    [Header("Joystick Holder")]
    [SerializeField] private FixedJoystick _joystick; //palitan mo nlng FixedJoystick kung di mo trip

    //[SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;

    private CameraScript _cameraScript;

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
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (_joystick == null) return; // Exit if no joystick is found
        if (_cameraScript.charSelected == 4) return; // Exit if no player object is found

        if (_cameraScript.playerUnits[_cameraScript.charSelected] != null)
        {
            _rigidbody = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<Rigidbody>();
        }

        _moveSpeed = _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().currentMovespeed;

        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            //_animator.SetBool("isRunning", true);
        }
        else
        {
            //_animator.SetBool("isRunning", false);
        }
    }
}

