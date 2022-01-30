using UnityEngine;

public class CityPlayerController : MonoBehaviour
{
    [SerializeField] private AttackController _attackController = null;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private stepsSFX _stepsSfx;

    [SerializeField] private int _kickRageCost = 50;
    [SerializeField] private int _beamRageCost = 200;
    
    private PlayerControls _playersControls = null;
    private Vector2 _moveInput = Vector2.zero;

    private bool _isDashing = false;
    private bool _isCharging = false;
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
        if (_isCharging && !_playersControls.City.Beam.IsPressed())
        {
            _attackController.StopCharging();
            DoneCharging();
        }
        
        if (_isDashing || _isCharging || _isBeaming)
        {
            return;
        }

        if (_playersControls.City.Beam.triggered)
        {
            if (_beamRageCost <= Rage.value)
            {
                _isCharging = true;
                _attackController.FireBeam(_beamRageCost, DoneCharging, Firing, Done);
                return;    
            }

            Debug.Log("Not Enough Rage for beam");
        }

        if (_playersControls.City.FlyingKick.triggered)
        {
            if (_kickRageCost <= Rage.value)
            {
                _stepsSfx.Stepping = false;
                _isDashing = true;
                _attackController.FlyingKick(AttackingComplete);
                Rage.value -= _kickRageCost;
                return;
            }
            Debug.Log("Not Enough Rage for Kick");
        }
    }

    private void DoneCharging()
    {
        _isCharging = false;
    }
    
    private void Firing()
    {
        _isBeaming = true;
    }
    
    private void Done()
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

        _stepsSfx.Stepping = _moveInput.sqrMagnitude > 0.1f;

        _rigidbody.velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _speed;
        _rigidbody.transform.LookAt(_rigidbody.position + _rigidbody.velocity);
    }
}
