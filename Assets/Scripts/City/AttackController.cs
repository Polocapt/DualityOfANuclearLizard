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

    [Header("Dash Parameters")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    private Coroutine _chargingCoroutine;


    private void Start()
    {
        _laserBeam.Init(_beamRange);
    }

    public void FlyingKick(Action callback)
    {
        StartCoroutine(StartFlyingKick(callback));
    }
    private IEnumerator StartFlyingKick(Action callback)
    {
        _rigidbody.velocity = transform.forward * _dashSpeed;
        
        transform.Rotate(new Vector3(-90, 0, 0));
        _rigidbody.useGravity = false;
        
        yield return new WaitForSeconds(_dashDuration);
        
        _rigidbody.useGravity = true;
        transform.Rotate(new Vector3(90, 0, 0));
        
        callback();
    }
    
    public void FireBeam(Action stopCharging, Action fire, Action stopFire)
    {
        _chargingCoroutine = StartCoroutine(StartCharging(stopCharging, fire, stopFire));
    }

    public void StopCharging()
    {
        StopCoroutine(_chargingCoroutine);
        _laserBeam.StopCharge();
    }
    
    private IEnumerator StartCharging(Action stopCharging, Action fire, Action stopFire)
    {
        _laserBeam.Charge();
        yield return new WaitForSeconds(_chargeSound.length);
        yield return StartLaserBeam(fire, stopFire);
        stopCharging();
    }
    
    private IEnumerator StartLaserBeam(Action fire, Action stopFire)
    {
        fire();
        _laserBeam.Fire();
        yield return new WaitForSeconds(_beamDuration);
        _laserBeam.Stop();
        stopFire();
    }
}
