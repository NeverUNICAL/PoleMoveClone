using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _maxJumpHeight;
    [SerializeField] private float _maxJumpSpeed;
    [SerializeField] private float _jumpHeightAdd;
    [SerializeField] private float _jumpSpeedAdd;
    [SerializeField] private float _landYPosition;
    [SerializeField] private ParticleSystem _compressingParticle;
    [SerializeField] private Stickman _stickman;

    
    private float _currentTime;
    private Animator _animator;
    private bool _isGrounded;
    private float _jumpHeight;
    private float _jumpSpeed;
    private float _elapsedTime;
    private Rigidbody _rigidbody;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }
    
    private void OnEnable()
    {
        _input.StartedGainPower += OnStartedGainPower;
        _input.Jumped += OnJumped;
        _input.StartedMakeLanding += OnMakeALanding;
    }

    private void OnDisable()
    {
        _input.StartedGainPower -= OnStartedGainPower;
        _input.Jumped -= OnJumped;
        _input.StartedMakeLanding -= OnMakeALanding;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _animator.SetBool(AnimatorPlayerController.States.IsGrounded,true);
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
        _animator.SetBool(AnimatorPlayerController.States.IsGrounded,false);
        _compressingParticle.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out LoseZone loseZone))
        {
            _stickman.transform.DORotate(new Vector3(0, -90, 0),1);
        }
    }

    private void OnStartedGainPower()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Compressing);
        _currentTime = Time.time;
        _compressingParticle.Play();
    }

    private void OnJumped()
    {
        if (_isGrounded)
        {
            _animator.SetTrigger(AnimatorPlayerController.States.Jump);
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

    private void OnMakeALanding()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.DOMoveY(_landYPosition,0.4f);
    }
}
