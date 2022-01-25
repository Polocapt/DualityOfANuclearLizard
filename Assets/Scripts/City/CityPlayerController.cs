using UnityEngine;

public class CityPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private float _speed = 1f;
    
    private PlayerControls _playersControls = null;
    private Vector2 _moveInput = Vector2.zero;

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

    private void FixedUpdate()
    {
        _moveInput = _playersControls.City.Movement.ReadValue<Vector2>();

        
        _rigidbody.velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _speed;
    }
}
