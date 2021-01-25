using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Character movement written from Brackey's FPS movement tutorial.
 * https://www.youtube.com/watch?v=_QajrabyTJc
 */

public class CharacterMovement : MonoBehaviour
{
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 MoveVelocity { get { return _move; } }
    public bool IsSprinting { get; private set; }
    public float sprintSpeed;

    private float _horizontal = 0.0f;
    private float _vertical = 0.0f;
    private CharacterController _controller;

    private float _speed = 0.0f;
    private float _gravity = -9.81f;

    private Vector3 _velocity;
    private Vector3 _move;
    private bool isGrounded;

    private void OnEnable()
    {
        StatsPanelUI.onStatsChanged += UpdateSpeedValue;
    }

    private void OnDisable()
    {
        StatsPanelUI.onStatsChanged += UpdateSpeedValue;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _speed = GetComponent<CharacterStats>().speed;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // If we are on the ground set velocity to zero, otherwise when we apply gravity
        // velocity will go towards negative infinity over time
        if(isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        // Get input from WASD
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // Combine vector from input to get one vector
        _move = transform.right * _horizontal + transform.forward * _vertical;


        // Use the combined vector along with deltaTime and speed to move the character
        if (Input.GetButton("Sprint"))
        {
            IsSprinting = true;
            _controller.Move(_move * Time.deltaTime * (_speed + sprintSpeed));
        }
        else
        {
            IsSprinting = false;
            _controller.Move(_move * Time.deltaTime * _speed);
        }

        // Jump by applying velocity on the y axis
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(1.5f * -2f * _gravity);
        }

        // Apply gravity here because we do not have a rigidbody to do this for us
        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateSpeedValue(object sender, EventArgs eventArgs)
    {
        //TODO: Save a ref to CharacterStats
        _speed = GetComponent<CharacterStats>().speed;
    }
}
