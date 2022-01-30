using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampManager : MonoBehaviour
{
    public bool StampingEnabled = false;
    public bool stamping = false;
    public PaperManager PM;
    public GameObject stampMark;
    public TaskManager TM;
    public GameObject hand;
    float frameLength = 0.02f;

    public int wiggleFrames = 100;
    public float wiggleAmplitude = 10f;
    public float wiggleRate = 10;

    [SerializeField] private float _rageStamp = 50;
    
    void Update()
    {
        if (StampingEnabled && !TM.dayIsOver)
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
        TM.StampTriggered();
        if (CurrentPage == null) yield break;

        Vector3 pathToCenter = CurrentPage.transform.position - transform.position;
        float distanceToCenter = pathToCenter.magnitude;
        float vel = 0.1f;
        int frames = Mathf.RoundToInt(distanceToCenter / vel);
        
        float deltaY = 1f / (-.5f * frames);
        Vector3 ymotion = new Vector3(0f, deltaY, 0f);
        int halfFrames = Mathf.RoundToInt(0.5f * frames);
        Vector3 initialPosition = transform.position;
        hand.SetActive(true);

        // stamp it!
        while (frames > 0)
        {
            transform.position += pathToCenter.normalized * vel;
            hand.transform.position = transform.position;
            if (frames > halfFrames) transform.Translate(-ymotion);
            if (frames < halfFrames) transform.Translate(ymotion);
            frames--;

            yield return new WaitForSeconds(frameLength);
        }

        frames = halfFrames;

        // trigger sfx
        gameObject.GetComponent<AudioSource>().Play();

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
            hand.transform.position = transform.position;
            frames--;
        }

        // done stamping
        Rage.value = Mathf.Min(Rage.value + _rageStamp, Rage.MaxRage);
        
        transform.position = initialPosition;
        hand.SetActive(false);
        stamping = false;

        // pause for a sec
        yield return new WaitForSeconds(0.2f);

        // exit paper
        PM.GetComponent<PaperManager>().SendPaperToPile();
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
