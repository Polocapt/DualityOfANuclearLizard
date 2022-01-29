using System;
using UnityEngine;

public class CityPlayerController : MonoBehaviour
{
    [SerializeField] private AttackController _attackController = null;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 1f;
    
    private PlayerControls _playersControls = null;
    private Vector2 _moveInput = Vector2.zero;

    private bool _isDashing = false;
    private bool _isBeaming = false;

    private void Awake()
    {
        _playersControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playersControls.City.Enable();
    }

    private void OnDisable()
    {
        _playersControls.City.Disable();
    }

    private void Update()
    {
        if (_isDashing || _isBeaming)
        {
            return;
        }

        if (_playersControls.City.Beam.triggered)
        {
            _isBeaming = true;
            _attackController.FireBeam(BeamComplete);
            return;
        }

        if (_playersControls.City.FlyingKick.triggered)
        {
            _isDashing = true;
            _attackController.FlyingKick(AttackingComplete);
        }
    }

    private void BeamComplete()
    {
        _isBeaming = false;
    }
    
    private void AttackingComplete()
    {
        _isDashing = false;
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;

        _moveInput = _playersControls.City.Movement.ReadValue<Vector2>();
        
        _rigidbody.velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _speed;
        _rigidbody.transform.LookAt(_rigidbody.position +_rigidbody.velocity);
    }
}
