using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    
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
            if (hit.collider != null)
            {
                if (hit.collider.tag.Equals("Building"))
                {
                    FindObjectOfType<RandomSFX>().TriggerRandomSound();

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
