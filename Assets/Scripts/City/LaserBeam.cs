using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    
    [SerializeField] private LineRenderer _lineRenderer;
    private float _laserRange = 0;

    public void Init(float range, float startWidth, float endWidth)
    {
        Stop();
        _laserRange = range;
        _lineRenderer.startWidth = startWidth;
        _lineRenderer.endWidth = endWidth;
    }

    private void Update()
    {
        _lineRenderer.SetPosition(0, transform.position);
        Vector3 endPos = transform.position + transform.forward * _laserRange;
        _lineRenderer.SetPosition(1, endPos);
        
        if (Physics.SphereCast(transform.position,  3,transform.forward, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag.Equals("Building"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void Fire()
    {
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
