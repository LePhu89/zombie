using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _playerSprint = 3f;
    [SerializeField] private float _jumpRange = 1f;

    [Header("Player follow camera")]
    [SerializeField] private Transform _playerCamera;

    [Header("Amination and Gravity")]
    [SerializeField] private Animator _animator;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Player Jumping and Velocity")]
    [SerializeField] private float _turnCalmTime = 0.1f;
    [SerializeField] private GroundingChecker _groundChecker;

    private Vector3 _velocity;
    private float _turnCalmVelocity;
    private bool _isJumping;
    private float _desiredSpeed;
    private float _currentSpeed;
    private float _speedVelocity;

    private void Update()
    {
        UpdateFalling();
        UpdateMoving();

        if (_isJumping)
        {
            CheckLanding();
        }
        else
        {
            CheckJumping();
        }
    }

    private void UpdateFalling()
    {
        if (_groundChecker.IsOnSurface && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateMoving()
    {
        Vector3 direction = GetInputDirection();
        if (direction.magnitude >= 0.1f)
        {
            _animator.SetBool("IsMoving", true);
            _desiredSpeed = Input.GetButton("Sprint")? _playerSprint : _playerSpeed;
            RotateToward(direction);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
            _desiredSpeed = 0;
        }
        MovePlayer();
    }

    private Vector3 GetInputDirection()
    {
        float horizontal_axis = Input.GetAxis("Horizontal");
        float vertical_axis = Input.GetAxis("Vertical");

        return new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
    }

    private void RotateToward(Vector3 direction)
    {
        float targerAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _playerCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targerAngle, ref _turnCalmVelocity, _turnCalmTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void MovePlayer()
    {
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, _desiredSpeed, ref _speedVelocity, 0.2f);
        _controller.Move(_currentSpeed * Time.deltaTime * transform.forward);
        _animator.SetFloat("Speed", _currentSpeed);
    }


    private void CheckJumping()
    {
        if (Input.GetButtonDown("Jump") && _groundChecker.IsOnSurface)
        {
            _animator.SetBool("Jumping", true);
            _velocity.y = Mathf.Sqrt(_jumpRange * _gravity * -2);
            _isJumping = true;
        }
    }
    private void CheckLanding()
    {
        if (_velocity.y <= 0 && _groundChecker.IsOnSurface)
        {
            _animator.SetBool("Jumping", false);
            _isJumping = false;
        }
    }
}
