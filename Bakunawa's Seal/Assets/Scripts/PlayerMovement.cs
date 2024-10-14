using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
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
    private void FixedUpdate()
    {
        if (_joystick == null) return;
        if (_cameraScript.charSelected == 4 ||
            _cameraScript.playerUnits[_cameraScript.charSelected].GetComponent<CharStats>().isDead == true) return;

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

