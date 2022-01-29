using System;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Beam Parameters")]
    [SerializeField] private LaserBeam _laserBeam;
    [SerializeField] private float _beamRange;
    [SerializeField] private float _beamStartWidth;
    [SerializeField] private float _beamEndWidth;
    [SerializeField] private float _beamDuration;

    [Header("Dash Parameters")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;

    private void Start()
    {
        _laserBeam.Init(_beamRange, _beamStartWidth, _beamEndWidth);
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
    
    public void FireBeam(Action callback)
    {
        StartCoroutine(StartLaserBeam(callback));
    }

    private IEnumerator StartLaserBeam(Action callback)
    {
        _laserBeam.Fire();
        yield return new WaitForSeconds(_beamDuration);
        _laserBeam.Stop();
        callback();
    }
}
