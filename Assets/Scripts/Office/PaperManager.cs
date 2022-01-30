using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public GameObject defaultpaper;
    public GameObject hand;
    bool paperActive = false;
    public GameObject CurrentPaper;
    GameObject LastPaper;
    public GameObject PileMarker;
    public float paperAngle;
    float frameLength = 0.02f;
    float stackHeight = 0f;
    float stackInterval = 0.04f;
    public bool stopIt = false;
    RandomSFX sfx;
    Pencil pencil;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<RandomSFX>();
        pencil = GameObject.FindGameObjectWithTag("Pencil").GetComponent<Pencil>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paperActive)
        {
            StartCoroutine(LaunchPaper());
            paperActive = true;
        }
    }

    IEnumerator LaunchPaper()
    {
        // short pause before launching paper
        yield return new WaitForSeconds(0.8f);

        sfx.TriggerRandomSound();

        GameObject newpaper = Instantiate(defaultpaper);
        newpaper.transform.position = transform.position;
        CurrentPaper = newpaper;
        Drawing drawingHandler = newpaper.GetComponentInChildren<Drawing>();

        float randomDeviation = 10f;
        float targetRotation = Random.Range(360f - randomDeviation, 360f + randomDeviation);


        float rotateRate = 10f;
        int frames = Mathf.RoundToInt(targetRotation / rotateRate);
        float travelDistance = 2f;
        float distancePerFrame = travelDistance / frames;
        
        Vector3 Rotation = new Vector3(0f, 0f, 0f);
        Vector3 Position = newpaper.transform.position;

        int frame = 0;
        while (frame < frames)
        {
            newpaper.transform.eulerAngles = Rotation;
            newpaper.transform.position = Position;

            Position.x -= distancePerFrame;
            Rotation.y += rotateRate;
            frame++;
            yield return new WaitForSeconds(frameLength);
        }

        yield break;
    }

    public void SendPaperToPile()
    {
        CurrentPaper.GetComponentInChildren<DrawingSurface>().drawingEnabled = false;
        if (CurrentPaper != null)
        {
            Drawing d = CurrentPaper.GetComponentInChildren<Drawing>();
            if (d != null)
            {
                GameObject DrawingArea = d.gameObject;
                Destroy(DrawingArea);
            }
            
        }
        
        LastPaper = CurrentPaper;
        CurrentPaper = null;
        paperActive = false;

        sfx.TriggerRandomSound();

        RestPencil();

        StartCoroutine(StackPaper(LastPaper));
    }

    void RestPencil()
    {
        pencil.gameObject.transform.position = pencil.restPosition;
        pencil.gameObject.transform.eulerAngles = pencil.restAngle;
        hand.SetActive(false);
    }

    void GrabPencil()
    {

    }

    IEnumerator StackPaper(GameObject lastpaper)
    {
        float vel = 0.2f;
        Vector3 distance = PileMarker.transform.position - lastpaper.transform.position;
        int frames = Mathf.RoundToInt(distance.magnitude / vel);

        while (distance.magnitude > 0.1f)
        {
            lastpaper.transform.position += distance.normalized * vel;
            lastpaper.transform.Translate(new Vector3(0f, stackHeight / frames, 0f));

            distance = PileMarker.transform.position - lastpaper.transform.position;
            yield return new WaitForSeconds(frameLength);
        }

        stackHeight += stackInterval;

        yield break;
    }
}
