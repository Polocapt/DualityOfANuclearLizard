using System;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Slash Parameters")]
    [SerializeField] private float _slashAttackSpeed;
    [SerializeField] private float _slashRadius;
    
    [Header("Beam Parameters")]
    [SerializeField] private float _beamRange;
    [SerializeField] private float _beamWidth;

    [Header("Dash Parameters")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    
    public void Slash(Action callback)
    {
        Debug.Log("Slash");
        callback();
    }
    
    public void FlyingKick(Action callback)
    {
        StartCoroutine(StartFlyingKick(callback));
    }
    private IEnumerator StartFlyingKick( Action callback)
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
        Debug.Log("BEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEAAAAAM");
        callback();
    }
}
