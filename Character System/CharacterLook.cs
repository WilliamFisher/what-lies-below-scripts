using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : MonoBehaviour
{
    public float horizontalSensitvity = 100f;
    public float verticalSensitvity = 100f;

    public Transform playerTransform;
    public bool lockCamRotation = false;

    private float _xRotation = 0f;
    private float _startYPos = 0f;
    private float _timer = 0;
    [SerializeField]
    private float _headBobSpeed;
    [SerializeField]
    private float _headBobAmount;
    [SerializeField]
    private float _bobSprintMultiplier;
    private float _sprintMultiplier;
    [SerializeField]
    private CharacterMovement _movement;

    void Start()
    {
        _startYPos = transform.localPosition.y;
    }

    void Update()
    {
        if (lockCamRotation) { return; }

        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitvity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitvity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);

        if (_movement.IsSprinting)
        {
            _sprintMultiplier = _bobSprintMultiplier;
        }
        else
        {
            _sprintMultiplier = 1;
        }


        //Apply a headbob
        if (Mathf.Abs(_movement.MoveVelocity.x) > 0.1f || Mathf.Abs(_movement.MoveVelocity.z) > 0.1f)
        {
            _timer += Time.deltaTime * _headBobSpeed * _sprintMultiplier;
            transform.localPosition = new Vector3(transform.localPosition.x, _startYPos + Mathf.Sin(_timer) * _headBobAmount * _sprintMultiplier,
                transform.localPosition.z);
        }
        else
        {
            _timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y,
                _startYPos, Time.deltaTime * _headBobSpeed), transform.localPosition.z);
        }
    }
}
