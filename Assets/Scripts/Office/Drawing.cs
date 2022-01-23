using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    Pencil pencil;


    // Start is called before the first frame update
    void Start()
    {
        pencil = GameObject.FindGameObjectWithTag("Pencil").GetComponent<Pencil>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // if mouse if over paper, display pencil over paper
                pencil.gameObject.transform.eulerAngles = pencil.drawingAngle;
                pencil.gameObject.transform.position = hit.point;

                if (Input.GetMouseButton(0))
                {
                    // mouse is pressed and over paper
                    Debug.Log("YAAAA");
                }
            }
            else
            {
                // if mouse is not over paper, put pencil back
                pencil.gameObject.transform.position = pencil.restPosition;
                pencil.gameObject.transform.eulerAngles = pencil.restAngle;
            }
        }
    }
}
