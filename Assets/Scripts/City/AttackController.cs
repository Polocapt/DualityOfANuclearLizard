using System;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Beam Parameters")]
    [SerializeField] private LaserBeam _laserBeam;
    [SerializeField] private AudioClip _chargeSound;
    [SerializeField] private float _beamRange;
    [SerializeField] private float _beamDuration;
    [ColorUsage(true,true)]
    [SerializeField] private Color _maxIntensity;
    [SerializeField] private Renderer _renderer;

    [Header("Dash Parameters")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _offset = Vector3.zero;

    private const int CRYSTAL_INDEX = 3;
    private Color _normalIntensity;

    private Coroutine _chargingCoroutine;
    private int _beamCost = 0;

    private void Start()
    {
        _laserBeam.Init(_beamRange);
        _normalIntensity = _renderer.materials[CRYSTAL_INDEX].color;
    }

    public void FlyingKick(Action callback)
    {
        StartCoroutine(StartFlyingKick(callback));
    }
    private IEnumerator StartFlyingKick(Action callback)
    {
        _rigidbody.velocity = transform.forward * _dashSpeed;
        
        _transform.Rotate(new Vector3(-90, 0, 0));
        _transform.position += _offset;
        _rigidbody.useGravity = true;
        yield return new WaitForSeconds(_dashDuration);
        _rigidbody.useGravity = false;
        _transform.position -= _offset;
        _transform.Rotate(new Vector3(90, 0, 0));
        
        callback();
    }
    
    public void FireBeam(int beamCost, Action stopCharging, Action fire, Action stopFire)
    {
        _beamCost = beamCost;
        _chargingCoroutine = StartCoroutine(StartCharging(stopCharging, fire, stopFire));
    }

    public void StopCharging()
    {
        StopCoroutine(_chargingCoroutine);
        _laserBeam.StopCharge();
        _renderer.materials[CRYSTAL_INDEX].color = _normalIntensity;
    }
    
    private IEnumerator StartCharging(Action stopCharging, Action fire, Action stopFire)
    {
        _laserBeam.Charge();

        float frameCount = 0;
        while (frameCount < _chargeSound.length)
        {
            frameCount += Time.deltaTime;

            _renderer.materials[CRYSTAL_INDEX].color = Color.Lerp(_normalIntensity, _maxIntensity, frameCount / _chargeSound.length);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(StartLaserBeam(fire, stopFire));
        stopCharging();
    }
    
    private IEnumerator StartLaserBeam(Action fire, Action stopFire)
    {
        fire();
        _laserBeam.Fire();

        float initValue = Rage.value;
        float frameCount = 0;
        while (frameCount < _beamDuration)
        {
            frameCount += Time.deltaTime;

            Rage.value -= _beamCost * (Time.deltaTime / _beamDuration);
            
            _renderer.materials[CRYSTAL_INDEX].color = Color.Lerp(_maxIntensity, _normalIntensity, frameCount / _beamDuration);
            yield return new WaitForEndOfFrame();
        }

        Rage.value = initValue - _beamCost;
        _laserBeam.Stop();
        stopFire();
    }
}
