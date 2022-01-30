using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _charge;
    private float _laserRange = 0;

    public void Init(float range)
    {
        Stop();
        _laserRange = range;
    }

    private void Update()
    {
        if (Physics.SphereCast(transform.position,  3,transform.forward,out RaycastHit hit, _laserRange))
        {
            var destructable = hit.collider.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.DestroyBuilding();
            }
        }
    }

    public void Fire()
    {
        _source.Play();
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }

    public void Charge()
    {
        _charge.Play();
    }
    
    public void StopCharge()
    {
        _charge.Stop();
    }
}
