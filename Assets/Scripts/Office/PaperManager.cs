using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public GameObject defaultpaper;
    bool paperActive = false;
    GameObject CurrentPaper;
    GameObject LastPaper;
    public GameObject PileMarker;
    public float paperAngle;
    float frameLength = 0.02f;
    float stackHeight = 0f;
    float stackInterval = 0.04f;
    Pencil pencil;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(1);

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
        GameObject DrawingArea = CurrentPaper.GetComponentInChildren<Drawing>().gameObject;
        Destroy(DrawingArea);
        LastPaper = CurrentPaper;
        CurrentPaper = null;
        paperActive = false;
        LastPaper.transform.Translate(new Vector3(0f, stackHeight, 0f));
        stackHeight += stackInterval;

        pencil.gameObject.transform.position = pencil.restPosition;
        pencil.gameObject.transform.eulerAngles = pencil.restAngle;

        StartCoroutine(StackPaper(LastPaper));
    }

    IEnumerator StackPaper(GameObject lastpaper)
    {
        float vel = 0.1f;
        Vector3 distance = PileMarker.transform.position - lastpaper.transform.position;
        while (distance.magnitude > 0.2f)
        {
            lastpaper.transform.position += distance.normalized * vel;
            
            distance = PileMarker.transform.position - lastpaper.transform.position;
            yield return new WaitForSeconds(frameLength);
        }

        yield break;
    }
}