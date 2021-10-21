using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMover : MonoBehaviour
{
    
    [SerializeField] private float _maxJumpHeight;
    [SerializeField] private float _maxJumpSpeed;
    [SerializeField] private float _jumpHeightAdd;
    [SerializeField] private float _jumpSpeedAdd;
    [SerializeField] private float _landYPosition;
    [SerializeField] private PointsText _pointsText;
     
    private float _currentTime;
    private bool _isGrounded;
    private float _jumpHeight;
    private float _jumpSpeed;
    private float _elapsedTime;
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    private bool _isWorking;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }
    
    private void OnEnable()
    {
        _input.StartedGainPower += OnStartedGainPower;
        _input.Jumped += OnJumped;
    }

    private void OnDisable()
    {
        _input.StartedGainPower -= OnStartedGainPower;
        _input.Jumped -= OnJumped;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Platform platform))
        {
           _pointsText.Create(platform.PointsValue);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
                OnStartedGainPower();
        if (Input.GetKeyUp(KeyCode.Space))
            OnJumped();

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_isGrounded == false)
            {
                MakeALanding();
            }
        }
    }

    private void OnStartedGainPower()
    {
        _currentTime = Time.time;
    }

    private void OnJumped()
    {
        if (_isGrounded)
        {
            _elapsedTime = Time.time - _currentTime;
            CalculateJumpVelocity();
            _rigidbody.AddForce((transform.up + transform.forward) + new Vector3(0, _jumpHeight, _jumpSpeed),
                ForceMode.VelocityChange);
        }
    }

    private void CalculateJumpVelocity()
    {
        _jumpHeight = 1;
        _jumpSpeed = 1;
        
        if (_elapsedTime < 1)
            _elapsedTime = 1;
        
        _jumpHeight = _elapsedTime * _jumpHeightAdd;
        _jumpSpeed = _elapsedTime * _jumpSpeedAdd;

        if (_jumpHeight > _maxJumpHeight)
            _jumpHeight = _maxJumpHeight;

        if (_jumpSpeed > _maxJumpSpeed)
            _jumpSpeed = _maxJumpSpeed;
    }

    private void MakeALanding()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.DOMoveY(_landYPosition,0.4f);
    }
}
