using System;
using UnityEngine;

public class CityPlayerController : MonoBehaviour
{
    [SerializeField] private AttackController _attackController = null;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 1f;
    
    private PlayerControls _playersControls = null;
    private Vector2 _moveInput = Vector2.zero;

    private bool _isAttacking = false;

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
        if (_isAttacking)
        {
            return;
        }
        
        if (_playersControls.City.Slash.triggered)
        {
            _isAttacking = true;
            _attackController.Slash(AttackingComplete);
            return;
        }
        
        if (_playersControls.City.Beam.triggered)
        {
            _isAttacking = true;
            _attackController.FireBeam(AttackingComplete);
            return;
        }

        if (_playersControls.City.FlyingKick.triggered)
        {
            _isAttacking = true;
            _attackController.FlyingKick(AttackingComplete);
            return;
        }
    }

    private void AttackingComplete()
    {
        _isAttacking = false;
    }

    private void FixedUpdate()
    {
        if (_isAttacking) return;

        _moveInput = _playersControls.City.Movement.ReadValue<Vector2>();
        
        _rigidbody.velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _speed;
        _rigidbody.transform.LookAt(_rigidbody.position +_rigidbody.velocity);
    }
}
