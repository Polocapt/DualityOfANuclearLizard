using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public GameObject defaultpaper;
    bool paperActive = false;
    GameObject CurrentPaper;
    public float paperAngle;

    // Start is called before the first frame update
    void Start()
    {
        
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

        float randomDeviation = 20f;
        float targetRotation = Random.Range(360f - randomDeviation, 360f + randomDeviation);


        float rotateRate = 10f;
        int frames = Mathf.RoundToInt(targetRotation / rotateRate);
        float travelDistance = 2f;
        float distancePerFrame = travelDistance / frames;
        float frameLength = 0.02f;
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
        StartCoroutine(StackPaper());
    }

    IEnumerator StackPaper()
    {

        yield break;
    }
}
