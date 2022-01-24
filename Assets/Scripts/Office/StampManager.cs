using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public bool StampingEnabled = false;
    bool stamping = false;
    public PaperManager PM;
    public GameObject stampMark;
    float frameLength = 0.02f;

    public int wiggleFrames = 100;
    public float wiggleAmplitude = 10f;
    public float wiggleRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StampingEnabled)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (!stamping)
                        {
                            stamping = true;
                            StartCoroutine(Stamp());
                        }
                    }
                }
            }
        }
        
    }

    IEnumerator Stamp()
    {
        GameObject CurrentPage = PM.CurrentPaper;

        Vector3 pathToCenter = CurrentPage.transform.position - transform.position;
        float distanceToCenter = pathToCenter.magnitude;
        float vel = 0.1f;
        int frames = Mathf.RoundToInt(distanceToCenter / vel);
        
        float deltaY = 1f / (-.5f * frames);
        Vector3 ymotion = new Vector3(0f, deltaY, 0f);
        int halfFrames = Mathf.RoundToInt(0.5f * frames);
        Vector3 initialPosition = transform.position;

        // stamp it!
        while (frames > 0)
        {
            transform.position += pathToCenter.normalized * vel;
            if (frames > halfFrames) transform.Translate(-ymotion);
            if (frames < halfFrames) transform.Translate(ymotion);
            frames--;

            yield return new WaitForSeconds(frameLength);
        }

        frames = halfFrames;

        // pause for a sec
        yield return new WaitForSeconds(0.2f);

        // leave a mark
        stampMark.SetActive(true);
        stampMark.transform.eulerAngles = transform.eulerAngles;

        // remove stamp
        while (frames > 0)
        {
            yield return new WaitForSeconds(frameLength);
            transform.position += -pathToCenter.normalized * vel * 2f;
            frames--;
        }

        // done stamping
        Debug.Log("Done Stamping!");
        transform.position = initialPosition;

        // pause for a sec
        yield return new WaitForSeconds(0.2f);

        // exit paper
        PM.GetComponent<PaperManager>().SendPaperToPile();

        yield break;
    }

    public void ReadyToStamp()
    {
        StampingEnabled = true;
        stamping = false;
        Wiggle();
    }

    void Wiggle()
    {
        StartCoroutine(WiggleMe());
    }

    IEnumerator WiggleMe() {

        
        Vector3 angle = gameObject.transform.eulerAngles;
        int frames = wiggleFrames;

        while (frames > 0)
        {
            gameObject.transform.eulerAngles = angle;
            angle.y = Mathf.Sin(frames / wiggleRate) * wiggleAmplitude;
            frames--;

            yield return new WaitForSeconds(frameLength);
        }
        yield break;
    }
}
