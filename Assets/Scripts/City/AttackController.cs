using System;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Dash Parameters")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private int _dashDuration;
    
    public void FireBeam(Action callback)
    {
        Debug.Log("BEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEAAAAAM");
        callback();
    }
    
    public void Slash(Action callback)
    {
        Debug.Log("Slash");
        callback();
    }
    
    public void FlyingKick(Action callback)
    {
        StartCoroutine(Dash(callback));
    }
    private IEnumerator Dash( Action callback)
    {
        _rigidbody.velocity = transform.forward * _dashSpeed;
        
        transform.Rotate(new Vector3(-90, 0, 0));
        _rigidbody.useGravity = false;
        
        yield return new WaitForSeconds(_dashDuration);
        
        _rigidbody.useGravity = true;
        transform.Rotate(new Vector3(90, 0, 0));
        
        callback();
    }
}
